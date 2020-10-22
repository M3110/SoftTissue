using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.Core.NumericalMethods.Derivative;
using SoftTissue.Core.NumericalMethods.Integral.Simpson;
using System;
using System.Linq;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung
{
    public class FungModel : QuasiLinearViscoelasticityModel<FungModelInput, FungModelResult>, IFungModel
    {
        private readonly ISimpsonRuleIntegration _simpsonRuleIntegration;
        private readonly IDerivative _derivative;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="simpsonRuleIntegration"></param>
        /// <param name="derivative"></param>
        public FungModel(
            ISimpsonRuleIntegration simpsonRuleIntegration,
            IDerivative derivative)
        {
            this._simpsonRuleIntegration = simpsonRuleIntegration;
            this._derivative = derivative;
        }

        public delegate double FungModelEquation(FungModelInput input, double time);

        /// <summary>
        /// The time when the alternative and original reduced relaxation function converge.
        /// </summary>
        public double? ConvergenceTime { get; set; }

        /// <summary>
        /// This method calculates the initial conditions for Fung model analysis.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override FungModelResult CalculateInitialConditions(FungModelInput input)
        {
            return new FungModelResult
            {
                ReducedRelaxationFunction = 1,
                ElasticResponse = 0,
                Strain = 0,
                Stress = 0,
                StressWithIntegralDerivative = 0,
                StressWithReducedRelaxationFunctionDerivative = 0
            };
        }

        /// <summary>
        /// This method calculates the strain.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateStrain(FungModelInput input, double time)
        {
            return time < input.RampTime ? input.StrainRate * time : input.MaximumStrain;
        }

        /// <summary>
        /// This method calculates the elastic response.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateElasticResponse(FungModelInput input, double time)
        {
            double strain = this.CalculateStrain(input, time);

            return input.ElasticStressConstant * (Math.Exp(input.ElasticPowerConstant * strain) - 1);
        }

        /// <summary>
        /// This method calculates the derivtive of elastic response.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateElasticResponseDerivative(FungModelInput input, double time)
        {
            double strain = this.CalculateStrain(input, time);

            // After the ramp time, the strain is constant, so the its derivative is zero.
            double strainDerivative = time < input.RampTime ? input.StrainRate : 0;

            return input.ElasticStressConstant * input.ElasticPowerConstant * strainDerivative * Math.Exp(input.ElasticPowerConstant * strain);
        }

        /// <summary>
        /// This method calculates the simplified reduced relaxation funtion.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateReducedRelaxationFunctionSimplified(FungModelInput input, double time)
        {
            if (time <= Constants.Precision)
            {
                return 1;
            }

            double result = 0;

            for (int i = 0; i < input.SimplifiedReducedRelaxationFunctionInput.VariableEList.Count(); i++)
            {
                result += input.SimplifiedReducedRelaxationFunctionInput.VariableEList.ElementAt(i) * Math.Exp(-time / input.SimplifiedReducedRelaxationFunctionInput.RelaxationTimeList.ElementAt(i));
            }

            return result;
        }

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
            {
                return 1;
            }

            // This step is necessary to find the time when the original and alternative reduced relaxation function converge.
            if (this.ConvergenceTime.HasValue == false)
            {
                this.ConvergenceTime = this.CalculateConvergenceTimeToReducedRelaxationFunction(input);
            }

            var reducedRelaxationFunctionInput = input.ReducedRelaxationFunctionInput;

            // When the time is in order of 1s, can be used a simplification of reduced relaxation function, exposed by Fung.
            // In that case, is used an approximation to equation E1, because to short values ​​it tends to infinite.
            if (time <= this.ConvergenceTime.Value)
            {
                return (1 - reducedRelaxationFunctionInput.RelaxationIndex * Constants.EulerMascheroniConstant - reducedRelaxationFunctionInput.RelaxationIndex * Math.Log(time / reducedRelaxationFunctionInput.SlowRelaxationTime)) / (1 + reducedRelaxationFunctionInput.RelaxationIndex * Math.Log(reducedRelaxationFunctionInput.SlowRelaxationTime / reducedRelaxationFunctionInput.FastRelaxationTime));
            }

            // The original equation was rewritten to simplify the E1 equation.
            // Instead of calculate E1 twice, was unified the dominion of both integration. See the explanation on Equation.docx.
            return (1 + reducedRelaxationFunctionInput.RelaxationIndex * this.CalculateI(reducedRelaxationFunctionInput.SlowRelaxationTime, reducedRelaxationFunctionInput.SlowRelaxationTime, input.TimeStep, time)) / (1 + reducedRelaxationFunctionInput.RelaxationIndex * Math.Log(reducedRelaxationFunctionInput.SlowRelaxationTime / reducedRelaxationFunctionInput.FastRelaxationTime));
        }

        /// <summary>
        /// This method calculates the stress using the equation 7 from Fung, at page 279.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateStress(FungModelInput input, double time)
        {
            FungModelEquation calculatedReducedRelaxationFunction = this.SetReducedRelaxationFunction(input);

            var integralInput = new IntegralInput
            {
                InitialPoint = 0,
                FinalPoint = time < input.RampTime ? time : input.RampTime,
                Step = input.TimeStep
            };

            return this._simpsonRuleIntegration.Calculate(
                (equationTime) => calculatedReducedRelaxationFunction(input, time - equationTime) * this.CalculateElasticResponseDerivative(input, equationTime),
                integralInput);
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
            var integralInput = new IntegralInput
            {
                InitialPoint = time / slowRelaxationTime,
                FinalPoint = time / fastRelaxationTime,
                Step = stepTime
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

            while (original - simplified <= Constants.Precision)
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

        /// <summary>
        /// This method calculates the stress using the equation 8.a from Fung, at page 279.
        /// That equation do not returns a satisfactory result.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public double CalculateStressByReducedRelaxationFunctionDerivative(FungModelInput input, double time)
        {
            FungModelEquation calculatedReducedRelaxationFunction = this.SetReducedRelaxationFunction(input);

            var integralInput = new IntegralInput
            {
                InitialPoint = 0,
                FinalPoint = time,
                Step = input.TimeStep
            };

            return calculatedReducedRelaxationFunction(input, time: 0) * this.CalculateElasticResponse(input, time) +
                this._simpsonRuleIntegration.Calculate((integrationTime) =>
                {
                    return this.CalculateElasticResponse(input, integrationTime) * this._derivative.Calculate(
                        (derivativeTime) => calculatedReducedRelaxationFunction(input, derivativeTime),
                        input.TimeStep,
                        time);
                },
                integralInput);
        }

        /// <summary>
        /// This method calculates the stress using the equation 8.b from Fung, at page 279.
        /// That equation do not returns a satisfactory result.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public double CalculateStressByIntegrationDerivative(FungModelInput input, double time)
        {
            FungModelEquation calculatedReducedRelaxationFunction = this.SetReducedRelaxationFunction(input);

            return this._derivative.Calculate((derivativeTime) =>
            {
                var integralInput = new IntegralInput
                {
                    InitialPoint = 0,
                    FinalPoint = derivativeTime,
                    Step = input.TimeStep
                };

                return this._simpsonRuleIntegration.Calculate(
                    (integrationTime) => this.CalculateElasticResponse(input, time - integrationTime) * calculatedReducedRelaxationFunction(input, integrationTime),
                    integralInput);
            },
            input.TimeStep, time);
        }

        /// <summary>
        /// This method sets the correct Reduced Relaxation Function to be used.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public FungModelEquation SetReducedRelaxationFunction(FungModelInput input)
        {
            if (input.UseSimplifiedReducedRelaxationFunction == false)
                return this.CalculateReducedRelaxationFunction;
            else
                return this.CalculateReducedRelaxationFunctionSimplified;
        }
    }
}
