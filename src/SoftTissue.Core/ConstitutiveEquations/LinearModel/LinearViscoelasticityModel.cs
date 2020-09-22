using SoftTissue.Core.Models;

namespace SoftTissue.Core.ConstitutiveEquations.LinearModel
{
    public abstract class LinearViscoelasticityModel : ViscoelasticModel<LinearViscoelasticityModelInput>, ILinearViscoelasticityModel
    {
        /// <summary>
        /// This method calculates the reduced relaxation function for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract double CalculateReducedRelaxationFunction(LinearViscoelasticityModelInput input, double time);

        /// <summary>
        /// This method calculates the Creep Compliance J(t) for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract double CalculateCreepCompliance(LinearViscoelasticityModelInput input, double time);
    }
}
