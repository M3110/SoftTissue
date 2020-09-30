using SoftTissue.Core.Models.Viscoelasticity.Linear;

namespace SoftTissue.Core.ConstitutiveEquations.LinearModel
{
    public abstract class LinearViscoelasticityModel<TInput> : ViscoelasticModel<TInput>, ILinearViscoelasticityModel<TInput>
        where TInput : LinearViscoelasticityModelInput, new()
    {
        /// <summary>
        /// This method calculates the reduced relaxation function for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract double CalculateReducedRelaxationFunction(TInput input, double time);

        /// <summary>
        /// This method calculates the Creep Compliance J(t) for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract double CalculateCreepCompliance(TInput input, double time);
    }
}
