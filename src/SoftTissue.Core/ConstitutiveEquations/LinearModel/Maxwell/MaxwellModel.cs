using SoftTissue.Core.Models.Viscoelasticity.Linear;
using System;

namespace SoftTissue.Core.ConstitutiveEquations.LinearModel.Maxwell
{
    /// <summary>
    /// It represents the Maxwell Model to Linear Viscoelastic.
    /// </summary>
    public class MaxwellModel : LinearViscoelasticityModel<MaxwellModelInput>, IMaxwellModel
    {
        /// <summary>
        /// This method calculates the reduced relaxation function for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override double CalculateReducedRelaxationFunction(MaxwellModelInput input, double time)
        {
            // The equation to be used to calculate the reduced relaxation function.
            // G(t) = mi * e^(-t/tau)
            double value = input.Stiffness * Math.Exp(-time / input.RelaxationTime);

            return value;
        }

        /// <summary>
        /// This method calculates the stress for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override double CalculateStress(MaxwellModelInput input, double time)
        {
            double reducedRelaxationFunction = this.CalculateReducedRelaxationFunction(input, time);

            // The equation to be used to calculate the stress.
            // Sigma(epsilon, t) = G(t) * epsilon(t)
            return input.InitialStrain * reducedRelaxationFunction;
        }

        /// <summary>
        /// This method calculates the Creep Compliance J(t) for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override double CalculateCreepCompliance(MaxwellModelInput input, double time)
        {
            // The equation to be used to calculate the Creep Compliance.
            // J(t) = (1 / mi) + (time / ni)
            double result = 1 / input.Stiffness + time / input.Viscosity;

            return result;
        }

        /// <summary>
        /// This method calculates the strain for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override double CalculateStrain(MaxwellModelInput input, double time)
        {
            // The equation to be used to calculate the strain.
            // epsilon(t) = (sigma0 / mi) * J(t)
            // J(t) = (1 / mi) + (time / ni)
            double result = (input.InitialStress / input.Stiffness) * this.CalculateCreepCompliance(input, time);

            return result;
        }
    }
}
