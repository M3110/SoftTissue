using SoftTissue.DataContract.OperationBase;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.DisregardRampTime
{
    /// <summary>
    /// It represents the request content to CalculateStress operation of Quasi-Linear Viscoelasticity Model for a soft tissue experimental model.
    /// </summary>
    public class CalculateSimplifiedFungModelStressDisregardRampTimeToExperimentalModelRequest : OperationRequestBase
    {
        /// <summary>
        /// The experimental model.
        /// </summary>
        public ExperimentalModel ExperimentalModel { get; set; }

        /// <summary>
        /// The strain.
        /// Unit: Dimensionless.
        /// </summary>
        public double Strain { get; set; }
    }
}
