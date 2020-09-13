using SoftTissue.Core.Models;
using System.Threading.Tasks;

namespace SoftTissue.Core.ConstitutiveEquations.LinearModel
{
    public abstract class LinearViscoelasticityModel : ViscoelasticModel<LinearViscoelasticityModelInput>, ILinearViscoelasticityModel
    {
        /// <summary>
        /// This method calculates the reduced relaxation function for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract Task<double> CalculateReducedRelaxationFunction(LinearViscoelasticityModelInput input, double time);

        /// <summary>
        /// This method calculates the strain for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract Task<double> CalculateStrain(LinearViscoelasticityModelInput input, double time);

        /// <summary>
        /// This method calculates the Creep Compliance J(t) for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract Task<double> CalculateCreepCompliance(LinearViscoelasticityModelInput input, double time);
    }
}
