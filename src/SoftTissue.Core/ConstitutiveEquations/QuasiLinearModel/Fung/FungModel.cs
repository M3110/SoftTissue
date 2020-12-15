using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.Core.NumericalMethods.Derivative;
using SoftTissue.Core.NumericalMethods.Integral.Simpson;
using SoftTissue.Infrastructure.Models;
using System;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung
{
    public class FungModel : QuasiLinearViscoelasticityModel<FungModelInput, FungModelResult, ReducedRelaxationFunctionData>, IFungModel
    {
        private readonly ISimpsonRuleIntegration _simpsonRuleIntegration;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="simpsonRuleIntegration"></param>
        public FungModel(ISimpsonRuleIntegration simpsonRuleIntegration, IDerivative derivative) : base(simpsonRuleIntegration, derivative)
        {
            this._simpsonRuleIntegration = simpsonRuleIntegration;
        }

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
            // When considering that the viscoelastic effect ocurrer just after the ramp time, the reduced relaxation function must not
            // be considered in calculations and, after the rampa time, the time that will be used, must be adjusted to the initial time
            // be the ramp time.
            if (input.ViscoelasticConsideration == ViscoelasticConsideration.ViscoelasticEffectAfterRampTime)
            {
                if (time <= input.RampTime)
                    return 1;
                else
                    time -= input.RampTime;
            }

            // Here is applied the boundary conditions for Reduced Relaxation Function for time equals to zero.
            // The comparison with Constants.Precision is used because the operations with double have an error and, when that function
            // is called in another methods, that error must be considered to times near to zero.
            if (time <= Constants.Precision)
            {
                return 1;
            }

            // This step is necessary to find the time when the original and alternative reduced relaxation function converge.
            //if (this.ConvergenceTime.HasValue == false)
            //{
            //    this.ConvergenceTime = this.CalculateConvergenceTimeToReducedRelaxationFunction(input);
            //}

            var reducedRelaxationFunctionInput = input.ReducedRelaxationFunctionInput;

            // When the time is in order of 1s, can be used a simplification of reduced relaxation function, exposed by Fung.
            // In that case, is used an approximation to equation E1, because to short values ​​it tends to infinite.
            //if (time <= this.ConvergenceTime.Value)
            //{
            //    return (1 - reducedRelaxationFunctionInput.RelaxationIndex * Constants.EulerMascheroniConstant - reducedRelaxationFunctionInput.RelaxationIndex * Math.Log(time / reducedRelaxationFunctionInput.SlowRelaxationTime)) / (1 + reducedRelaxationFunctionInput.RelaxationIndex * Math.Log(reducedRelaxationFunctionInput.SlowRelaxationTime / reducedRelaxationFunctionInput.FastRelaxationTime));
            //}

            // The original equation was rewritten to simplify the E1 equation.
            // Instead of calculate E1 twice, was unified the dominion of both integration. See the explanation on Equation.docx.
            return (1 + reducedRelaxationFunctionInput.RelaxationIndex * this.CalculateI(reducedRelaxationFunctionInput.SlowRelaxationTime, reducedRelaxationFunctionInput.SlowRelaxationTime, input.TimeStep, time)) / (1 + reducedRelaxationFunctionInput.RelaxationIndex * Math.Log(reducedRelaxationFunctionInput.SlowRelaxationTime / reducedRelaxationFunctionInput.FastRelaxationTime));
        }

        /// <summary>
        /// This method calculates the derivative of reduced relaxation function.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateReducedRelaxationFunctionDerivative(FungModelInput input, double time)
        {
            // When considering that the viscoelastic effect ocurrer just after the ramp time, the reduced relaxation function must not
            // be considered in calculations and, after the rampa time, the time that will be used, must be adjusted to the initial time
            // be the ramp time.
            if (input.ViscoelasticConsideration == ViscoelasticConsideration.ViscoelasticEffectAfterRampTime)
            {
                if (time <= input.RampTime)
                    return 0;
                else
                    time -= input.RampTime;
            }

            // Here is applied the boundary conditions for Reduced Relaxation Function for time equals to zero.
            // The comparison with Constants.Precision is used because the operations with double have an error and, when that function
            // is called in another methods, that error must be considered to times near to zero.
            if (time <= Constants.Precision)
            {
                return 0;
            }

            var reducedRelaxationFunctionInput = input.ReducedRelaxationFunctionInput;

            return (reducedRelaxationFunctionInput.RelaxationIndex * (Math.Exp(-time / reducedRelaxationFunctionInput.FastRelaxationTime) - Math.Exp(-time / reducedRelaxationFunctionInput.SlowRelaxationTime))) / (time * (1 + reducedRelaxationFunctionInput.RelaxationIndex * Math.Log(reducedRelaxationFunctionInput.SlowRelaxationTime / reducedRelaxationFunctionInput.FastRelaxationTime)));
        }

        /// <summary>
        /// This method calculates the equation I(t) where t is the time.
        /// I(t) = E1(t/tau 2) - E1(t/tau 1)
        /// E1(x) = integral(e^-x/x) from x to infinite.
        /// </summary>
        /// <param name="slowRelaxationTime"></param>
        /// <param name="fastRelaxationTime"></param>
        /// <param name="stepTime"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public double CalculateI(double slowRelaxationTime, double fastRelaxationTime, double stepTime, double time)
        {
            double initialPoint = time / slowRelaxationTime;
            double step;

            if (initialPoint <= 0.5) step = 1e-4;
            else if (initialPoint > 0.5 && initialPoint <= 1) step = 1e-3;
            else if (initialPoint > 1 && initialPoint <= 5) step = 1e-2;
            else step = 1e-1;

            var integralInput = new IntegralInput
            {
                InitialPoint = initialPoint,
                FinalPoint = time / fastRelaxationTime,
                Step = step
            };

            return this._simpsonRuleIntegration.Calculate((parameter) => Math.Exp(-parameter) / parameter, integralInput);
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
