using Newtonsoft.Json;
using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain
{
    /// <summary>
    /// It represents the 'data' content to CalculateStrain operation request of Linear Viscoelasticity Model.
    /// </summary>
    public sealed class CalculateStrainRequestData : CalculateResultRequestData
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="softTissueType"></param>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="stiffness"></param>
        /// <param name="initialStress"></param>
        /// <param name="viscosity"></param>
        [JsonConstructor]
        public CalculateStrainRequestData(
            string softTissueType, 
            double? timeStep, 
            double? finalTime,
            double stiffness, 
            double initialStress, 
            double viscosity) : base(softTissueType, timeStep, finalTime)
        {
            this.Stiffness = stiffness;
            this.InitialStress = initialStress;
            this.Viscosity = viscosity;
        }

        /// <summary>
        /// Stiffness.
        /// Unit: Pa (Pascal).
        /// </summary>
        public double Stiffness { get; private set; }

        /// <summary>
        /// Inital stress.
        /// Unit: Pa (Pascal).
        /// </summary>
        public double InitialStress { get; private set; }

        /// <summary>
        /// Viscosity.
        /// Unit: N.s/m (Newton-second per meter).
        /// </summary>
        public double Viscosity { get; private set; }
    }
}
