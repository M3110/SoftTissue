using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.Core.NumericalMethods.Derivative;
using SoftTissue.Core.NumericalMethods.Integral.Simpson;
using SoftTissue.Infrastructure.Models;
using System;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.SimplifiedFung
{
    /// <summary>
    /// It represents the viscoelastic Fung Model considering the Simplified Relaxation Function.
    /// </summary>
    public class SimplifiedFungModel : QuasiLinearViscoelasticityModel<SimplifiedFungModelInput, SimplifiedFungModelResult, SimplifiedReducedRelaxationFunctionData>, ISimplifiedFungModel
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="simpsonRuleIntegration"></param>
        /// <param name="derivative"></param>
        public SimplifiedFungModel(ISimpsonRuleIntegration simpsonRuleIntegration, IDerivative derivative) : base(simpsonRuleIntegration, derivative) { }

        /// <summary>
        /// This method calculates the simplified reduced relaxation funtion.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateReducedRelaxationFunction(SimplifiedFungModelInput input, double time)
        {
            // When considering that the viscoelastic effect ocurrer just after the ramp time, the reduced relaxation function must not
            // be considered in calculations and, after the rampa time, the time that will be used, must be adjusted to the initial time
            // be the ramp time.
            if (input.ViscoelasticConsideration == ViscoelasticConsideration.ViscoelasticEffectAfterRampTime)
            {
                if (time <= input.FirstRampTime)
                    return 1;

                return time -= input.FirstRampTime;
            }

            // Here is applied the boundary conditions for Reduced Relaxation Function for time equals to zero.
            // The comparison with Constants.Precision is used because the operations with double have an error and, when that function
            // is called in another methods, that error must be considered to times near to zero.
            if (time <= Constants.Precision)
            {
                return 1;
            }

            double result = input.ReducedRelaxationFunctionInput.FirstViscoelasticStiffness;

            foreach (var iteratorValues in input.ReducedRelaxationFunctionInput.IteratorValues)
            {
                result += iteratorValues.ViscoelasticStiffness * Math.Exp(-time / iteratorValues.RelaxationTime);
            }

            return result;
        }

        /// <summary>
        /// This method calculates the derivative of reduced relaxation function.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateReducedRelaxationFunctionDerivative(SimplifiedFungModelInput input, double time)
        {
            // When considering that the viscoelastic effect ocurrer just after the ramp time, the reduced relaxation function must not
            // be considered in calculations and, after the rampa time, the time that will be used, must be adjusted to the initial time
            // be the ramp time.
            if (input.ViscoelasticConsideration == ViscoelasticConsideration.ViscoelasticEffectAfterRampTime)
            {
                if (time <= input.FirstRampTime)
                    return 0;
                else
                    time -= input.FirstRampTime;
            }

            // Here is applied the boundary conditions for Reduced Relaxation Function for time equals to zero.
            // The comparison with Constants.Precision is used because the operations with double have an error and, when that function
            // is called in another methods, that error must be considered to times near to zero.
            if (time <= Constants.Precision)
            {
                return 0;
            }

            double result = 0;

            foreach (var iteratorValues in input.ReducedRelaxationFunctionInput.IteratorValues)
            {
                result += -(iteratorValues.ViscoelasticStiffness / iteratorValues.RelaxationTime) * Math.Exp(-time / iteratorValues.RelaxationTime);
            }

            return result;
        }
    }
}
