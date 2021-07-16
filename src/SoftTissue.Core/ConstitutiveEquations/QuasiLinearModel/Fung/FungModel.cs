using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.Core.NumericalMethods.Derivative;
using SoftTissue.Core.NumericalMethods.Integral.Simpson;
using SoftTissue.DataContract.Models;
using System;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung
{
    /// <summary>
    /// It represents the Fung Model.
    /// </summary>
    public class FungModel : QuasiLinearModel<FungModelInput, FungModelResult, ReducedRelaxationFunctionData>, IFungModel
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="simpsonRuleIntegration"></param>
        public FungModel(ISimpsonRuleIntegration simpsonRuleIntegration, IDerivative derivative) : base(simpsonRuleIntegration, derivative) { }

        /// <summary>
        /// The time when the alternative and original reduced relaxation function converge.
        /// </summary>
        public double? ConvergenceTime { get; set; }

        /// <summary>
        /// This method calculates the reduced relaxation function.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateReducedRelaxationFunction(FungModelInput input, double time)
        {
            // Here is applied the boundary conditions for Reduced Relaxation Function for time equals to zero.
            // The comparison with Constants.Precision is used because the operations with double have an error and, when that function
            // is called in another methods, that error must be considered to times near to zero.
            if (time <= Constants.Precision)
                return 1;

            var reducedRelaxationFunctionInput = input.ReducedRelaxationFunctionInput;

            // The original equation was rewritten to simplify the E1 equation.
            // Instead of calculate E1 twice, was unified the dominion of both integration. See the explanation on Equation.docx.
            return
                (1 + reducedRelaxationFunctionInput.RelaxationStiffness * this.CalculateI(reducedRelaxationFunctionInput.SlowRelaxationTime, reducedRelaxationFunctionInput.FastRelaxationTime, input.TimeStep, time))
                / (1 + reducedRelaxationFunctionInput.RelaxationStiffness * Math.Log(reducedRelaxationFunctionInput.SlowRelaxationTime / reducedRelaxationFunctionInput.FastRelaxationTime));
        }

        /// <summary>
        /// This method calculates the derivative of reduced relaxation function.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateReducedRelaxationFunctionDerivative(FungModelInput input, double time)
        {
            var reducedRelaxationFunctionInput = input.ReducedRelaxationFunctionInput;

            // Here is applied the boundary conditions for Reduced Relaxation Function for time equals to zero.
            // The comparison with Constants.Precision is used because the operations with double have an error and, when that function
            // is called in another methods, that error must be considered to times near to zero.
            if (time <= Constants.Precision)
                return reducedRelaxationFunctionInput.RelaxationStiffness
                    * (-1 / reducedRelaxationFunctionInput.FastRelaxationTime + 1 / reducedRelaxationFunctionInput.SlowRelaxationTime)
                    / (1 + reducedRelaxationFunctionInput.RelaxationStiffness * Math.Log(reducedRelaxationFunctionInput.SlowRelaxationTime / reducedRelaxationFunctionInput.FastRelaxationTime));

            return reducedRelaxationFunctionInput.RelaxationStiffness
                * (Math.Exp(-time / reducedRelaxationFunctionInput.FastRelaxationTime) - Math.Exp(-time / reducedRelaxationFunctionInput.SlowRelaxationTime))
                / (time * (1 + reducedRelaxationFunctionInput.RelaxationStiffness * Math.Log(reducedRelaxationFunctionInput.SlowRelaxationTime / reducedRelaxationFunctionInput.FastRelaxationTime)));
        }

        /// <summary>
        /// This method calculates the equation I(t) where t is the time.
        /// I(t) = E1(t/tau 2) - E1(t/tau 1)
        /// E1(x) = integral(e^-x/x) from x to infinite.
        /// </summary>
        /// <param name="slowRelaxationTime"></param>
        /// <param name="fastRelaxationTime"></param>
        /// <param name="timeStep"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public double CalculateI(double slowRelaxationTime, double fastRelaxationTime, double timeStep, double time)
        {
            if (time < 0)
                throw new ArgumentOutOfRangeException(nameof(time), "The time cannot be negative.");

            if (time == 0)
                return Math.Log(fastRelaxationTime / slowRelaxationTime);

            double initialTime = time / slowRelaxationTime;
            double step = this.SetIntegrationStep(initialTime, timeStep);
            double finalTime = time / fastRelaxationTime > Constants.EquationE1MaximumFinalTime ? Constants.EquationE1MaximumFinalTime : time / fastRelaxationTime;

            double result = 0;
            double integralTime = initialTime;

            static double Equation(double time) => Math.Exp(-time) / time;

            int index = 0;

            // Here is used a Simpson Integration with some changes to addapt for this case.
            while (finalTime - integralTime >= Constants.Precision)
            {
                if (index == 0 || index == finalTime)
                {
                    result += Equation(integralTime) * step / 3;
                }
                else if (index % 2 != 0)
                {
                    result += 4 * Equation(integralTime) * step / 3;
                }
                else if (index % 2 == 0)
                {
                    result += 2 * Equation(integralTime) * step / 3;
                }

                if (result <= Constants.Precision)
                    break;

                integralTime += step;

                step = this.SetIntegrationStep(integralTime, timeStep);

                if (integralTime > finalTime)
                {
                    result += Equation(finalTime) * step / 3;
                    break;
                }

                index++;
            }

            return result;
        }

        private double SetIntegrationStep(double time, double timeStep)
        {
            if (time <= 0.5)
                return 1e-3 < timeStep ? 1e-3 : timeStep;
            else if (time > 0.5 && time <= 1)
                return 1e-2 < timeStep ? 1e-2 : timeStep;
            else
                return 1e-1 < timeStep ? 1e-1 : timeStep;
        }

        /// <summary>
        /// This method calculates time when the alternative and original reduced relaxation function converge.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public double CalculateConvergenceTimeToReducedRelaxationFunction(FungModelInput input)
        {
            double original = this.CalculateI(input.ReducedRelaxationFunctionInput.SlowRelaxationTime, input.ReducedRelaxationFunctionInput.SlowRelaxationTime, input.TimeStep, input.InitialTime);
            double simplified = -Constants.EulerMascheroniConstant - Math.Log(input.InitialTime / input.ReducedRelaxationFunctionInput.SlowRelaxationTime);
            double time = input.InitialTime;

            while (original - simplified <= 1e-3)
            {
                time += input.TimeStep;

                if (time >= input.FinalTime)
                {
                    throw new IndexOutOfRangeException($"The original and alternative reduced relaxation function do not converge in the range of time: {input.InitialTime} - {input.FinalTime}, with slow relaxation time: {input.ReducedRelaxationFunctionInput.SlowRelaxationTime} and fast relaxation time: {input.ReducedRelaxationFunctionInput.FastRelaxationTime}.");
                }

                original = this.CalculateI(input.ReducedRelaxationFunctionInput.SlowRelaxationTime, input.ReducedRelaxationFunctionInput.SlowRelaxationTime, input.TimeStep, time);
                simplified = -Constants.EulerMascheroniConstant - Math.Log(time / input.ReducedRelaxationFunctionInput.SlowRelaxationTime);
            }

            return time;
        }
    }
}
