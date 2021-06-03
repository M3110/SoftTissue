using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.Core.NumericalMethods.Derivative;
using SoftTissue.Core.NumericalMethods.Integral.Simpson;
using SoftTissue.DataContract.Models;
using System;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.SimplifiedFung
{
    /// <summary>
    /// It represents the Simplified Fung Model.
    /// The simplified Fung Model is characterized by using the Simplified Reduced Relaxation Function. 
    /// </summary>
    public class SimplifiedFungModel : QuasiLinearModel<SimplifiedFungModelInput, SimplifiedFungModelResult, SimplifiedReducedRelaxationFunctionData>, ISimplifiedFungModel
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
            // Here is applied the boundary conditions for Reduced Relaxation Function for time equals to zero.
            // The comparison with Constants.Precision is used because the operations with double have an error and, when that function
            // is called in another methods, that error must be considered to times near to zero.
            if (time <= Constants.Precision)
                return 1;

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
            double result = 0;

            foreach (var iteratorValues in input.ReducedRelaxationFunctionInput.IteratorValues)
            {
                result += -(iteratorValues.ViscoelasticStiffness / iteratorValues.RelaxationTime) * Math.Exp(-time / iteratorValues.RelaxationTime);
            }

            return result;
        }
    }
}
