using SoftTissue.DataContract.OperationBase;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It represents the request content to CalculateStress operation of Quasi-Linear Viscoelasticity Model for a soft tissue experimental model.
    /// </summary>
    public class CalculateStressToExperimentalModelRequest : OperationRequestBase
    {
        /// <summary>
        /// The experimental model.
        /// </summary>
        public ExperimentalModel ExperimentalModel { get; set; }

        /// <summary>
        /// Strain rate.
        /// Unity: Dimensionless.
        /// </summary>
        public double StrainRate { get; set; }

        /// <summary>
        /// Maximum strain.
        /// Unity: Dimensionless.
        /// </summary>
        public double MaximumStrain { get; set; }
    }
}
