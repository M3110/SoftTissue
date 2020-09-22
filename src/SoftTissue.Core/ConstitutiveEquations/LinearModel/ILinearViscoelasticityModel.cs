using SoftTissue.Core.Models;

namespace SoftTissue.Core.ConstitutiveEquations.LinearModel
{
    /// <summary>
    /// It represents a linear viscoelastic model.
    /// </summary>
    public interface ILinearViscoelasticityModel : IViscoelasticModel<LinearViscoelasticityModelInput>
    {
        /// <summary>
        /// This method calculates the reduced relaxation function for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        double CalculateReducedRelaxationFunction(LinearViscoelasticityModelInput inpu, double time);

        /// <summary>
        /// This method calculates the Creep Compliance J(t) for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        double CalculateCreepCompliance(LinearViscoelasticityModelInput input, double time);
    }
}
