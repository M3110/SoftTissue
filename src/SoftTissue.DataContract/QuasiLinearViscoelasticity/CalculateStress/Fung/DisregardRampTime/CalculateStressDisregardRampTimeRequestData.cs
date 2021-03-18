using Newtonsoft.Json;
using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.DisregardRampTime
{
    /// <summary>
    /// It represents the 'data' content to CalculateStressDisregardRampTime operation request of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public abstract class CalculateStressDisregardRampTimeRequestData : CalculateResultRequestData
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="softTissueType"></param>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="strain"></param>
        /// <param name="initialStress"></param>
        [JsonConstructor]
        public CalculateStressDisregardRampTimeRequestData(
            string softTissueType, 
            double? timeStep, 
            double? finalTime,
            double strain, 
            double initialStress) 
            : base(softTissueType, timeStep, finalTime)
        {
            this.Strain = strain;
            this.InitialStress = initialStress;
        }

        /// <summary>
        /// The maximum strain.
        /// Unit: Dimensionless.
        /// </summary>
        public double Strain { get; protected set; }

        /// <summary>
        /// The initial stress.
        /// Unit: Pa (Pascal).
        /// </summary>
        public double InitialStress { get; protected set; }
    }
}
