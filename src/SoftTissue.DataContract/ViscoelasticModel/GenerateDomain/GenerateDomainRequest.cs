using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.GenerateDomain
{
    /// <summary>
    /// It represents the request content to GenerateDomain operation of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public sealed class GenerateDomainRequest : OperationRequestBase
    {
        /// <summary>
        /// Time step.
        /// Unit: s (second).
        /// </summary>
        public double TimeStep { get; set; }

        /// <summary>
        /// Final time.
        /// Unit: s (second).
        /// </summary>
        public double FinalTime { get; set; }

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
