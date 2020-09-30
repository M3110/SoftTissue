using SoftTissue.Core.Models.Viscoelasticity.Linear;

namespace SoftTissue.Core.ConstitutiveEquations.LinearModel
{
    /// <summary>
    /// It represents a linear viscoelastic model.
    /// </summary>
    public interface ILinearViscoelasticityModel<TInput> : IViscoelasticModel<TInput>
        where TInput : LinearViscoelasticityModelInput, new()
    {
        /// <summary>
        /// This method calculates the reduced relaxation function for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        double CalculateReducedRelaxationFunction(TInput inpu, double time);

        /// <summary>
        /// This method calculates the Creep Compliance J(t) for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        double CalculateCreepCompliance(TInput input, double time);
    }
}
