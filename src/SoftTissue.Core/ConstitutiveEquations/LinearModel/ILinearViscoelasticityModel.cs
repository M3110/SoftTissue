using SoftTissue.Core.Models;
using System.Threading.Tasks;

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
        Task<double> CalculateReducedRelaxationFunction(LinearViscoelasticityModelInput inpu, double timet);

        /// <summary>
        /// This method calculates the strain for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<double> CalculateStrain(LinearViscoelasticityModelInput input, double time);

        /// <summary>
        /// This method calculates the Creep Compliance J(t) for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<double> CalculateCreepCompliance(LinearViscoelasticityModelInput input, double time);
    }
}
