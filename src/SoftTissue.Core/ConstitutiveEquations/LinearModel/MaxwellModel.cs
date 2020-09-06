using SoftTissue.Core.Models;
using System;
using System.Threading.Tasks;

namespace SoftTissue.Core.ConstitutiveEquations.LinearModel
{
    /// <summary>
    /// It represents the Maxwell Model to Linear Viscoelastic.
    /// </summary>
    public class MaxwellModel : ViscoelasticModel<LinearModelInput>, IMaxwellModel
    {
        /// <summary>
        /// This method calculates the reduced relaxation function for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override Task<double> CalculateReducedRelaxationFunction(LinearModelInput input)
        {
            // The equation to be used to calculate the reduced relaxation function.
            // G(t) = mi * e^(-t/tau)
            double value
                = input.Stiffness * Math.Exp(-input.Time / input.RelaxationTime);

            return Task.FromResult(value);
        }

        /// <summary>
        /// This method calculates the stress for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async override Task<double> CalculateStress(LinearModelInput input)
        {
            double reducedRelaxationFunction = await this.CalculateReducedRelaxationFunction(input).ConfigureAwait(false);

            // The equation to be used to calculate the stress.
            // Sigma(epsilon, t) = G(t) * epsilon(t)
            return (input.InitialStrain * reducedRelaxationFunction);
        }

        /// <summary>
        /// This method calculates the Creep Compliance J(t) for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override Task<double> CalculateCreepCompliance(LinearModelInput input)
        {
            // The equation to be used to calculate the Creep Compliance.
            // J(t) = (1 / mi) + (time / ni)
            double value = (1 / input.Stiffness) + (input.Time / input.Viscosity);

            return Task.FromResult(value);
        }

        /// <summary>
        /// This method calculates the strain for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async override Task<double> CalculateStrain(LinearModelInput input)
        {
            // The equation to be used to calculate the strain.
            // epsilon(t) = (sigma0 / mi) * J(t)
            // J(t) = (1 / mi) + (time / ni)
            double value
                = (input.InitialStress / input.Stiffness) * (await this.CalculateCreepCompliance(input).ConfigureAwait(false));

            return value;
        }
    }
}
