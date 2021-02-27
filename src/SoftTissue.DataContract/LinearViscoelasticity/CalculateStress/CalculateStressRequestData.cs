using Newtonsoft.Json;
using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It represents the 'data' content to CalculateStress operation request of Linear Viscoelasticity Model.
    /// </summary>
    public sealed class CalculateStressRequestData : CalculateResultRequestData
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="softTissueType"></param>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="stiffness"></param>
        /// <param name="initialStrain"></param>
        /// <param name="viscosity"></param>
        [JsonConstructor]
        public CalculateStressRequestData(
            string softTissueType,
            double? timeStep,
            double? finalTime,
            double stiffness,
            double initialStrain,
            double viscosity) : base(softTissueType, timeStep, finalTime)
        {
            this.Stiffness = stiffness;
            this.InitialStrain = initialStrain;
            this.Viscosity = viscosity;
        }

        /// <summary>
        /// Stiffness.
        /// Unit: Pa (Pascal).
        /// </summary>
        public double Stiffness { get; private set; }

        /// <summary>
        /// Inital strain.
        /// Unit: Dimensionless.
        /// </summary>
        public double InitialStrain { get; private set; }

        /// <summary>
        /// Viscosity.
        /// Unit: N.s/m (Newton-second per meter).
        /// </summary>
        public double Viscosity { get; private set; }
    }
}
