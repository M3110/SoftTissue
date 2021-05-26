using SoftTissue.Core.Models.Viscoelasticity.Linear.Maxwell;
using System;

namespace SoftTissue.Core.ConstitutiveEquations.LinearModel.Maxwell
{
    /// <summary>
    /// It represents the Maxwell Model.
    /// </summary>
    public class MaxwellModel : LinearViscoelasticityModel<MaxwellModelInput, MaxwellModelResult>, IMaxwellModel
    {
        /// <summary>
        /// This method calculates the relaxation function.
        /// Equation used: G(t) = mi * e^(-t/tau)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateRelaxationFunction(MaxwellModelInput input, double time)
        {
            return input.Stiffness * Math.Exp(-time / input.RelaxationTime);
        }

        /// <summary>
        /// This method calculates the stress for a specific time.
        /// Equation used: Sigma(t) = G(t) * Epsilon0
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateStress(MaxwellModelInput input, double time)
        {
            return input.InitialStrain * this.CalculateRelaxationFunction(input, time);
        }

        /// <summary>
        /// This method calculates the Creep Compliance J(t).
        /// Equation used: J(t) = (1 / mi) + (time / ni)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateCreepCompliance(MaxwellModelInput input, double time)
        {
            return 1 / input.Stiffness + time / input.Viscosity;
        }

        /// <summary>
        /// This method calculates the strain.
        /// Equation used: Epsilon(t) = (Sigma0 / mi) * J(t)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override double CalculateStrain(MaxwellModelInput input, double time)
        {
            return (input.InitialStress / input.Stiffness) * this.CalculateCreepCompliance(input, time);
        }
    }
}
