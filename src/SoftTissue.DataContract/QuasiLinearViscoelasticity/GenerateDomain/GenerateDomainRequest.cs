using SoftTissue.DataContract.OperationBase;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.GenerateDomain
{
    /// <summary>
    /// It represents the request content to GenerateDomain operation of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public class GenerateDomainRequest : OperationRequestBase
    {
        /// <summary>
        /// Tau 1.
        /// List of fast relaxation time.
        /// </summary>
        public Value FastRelaxationTimeList { get; set; }

        /// <summary>
        /// Tau 2.
        /// List of slow relaxation time.
        /// </summary>
        public Value SlowRelaxationTimeList { get; set; }
    }
}
