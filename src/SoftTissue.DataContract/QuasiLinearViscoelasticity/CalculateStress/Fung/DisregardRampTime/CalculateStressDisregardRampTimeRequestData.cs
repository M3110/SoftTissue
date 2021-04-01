using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.DisregardRampTime
{
    /// <summary>
    /// It represents the 'data' content to CalculateStressDisregardRampTime operation request of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public abstract class CalculateStressDisregardRampTimeRequestData : CalculateResultRequestData
    {
        /// <summary>
        /// The maximum strain.
        /// Unit: Dimensionless.
        /// </summary>
        public double Strain { get; set; }

        /// <summary>
        /// The initial stress.
        /// Unit: Pa (Pascal).
        /// </summary>
        public double InitialStress { get; set; }
    }
}
