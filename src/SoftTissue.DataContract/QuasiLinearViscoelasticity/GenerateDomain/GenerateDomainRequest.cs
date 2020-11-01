using SoftTissue.DataContract.OperationBase;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.GenerateDomain
{
    public class GenerateDomainRequest : OperationRequestBase
    {
        /// <summary>
        /// Tau 1.
        /// </summary>
        public Value FastRelaxationTimeList { get; set; }

        /// <summary>
        /// Tau 2.
        /// </summary>
        public Value SlowRelaxationTimeList { get; set; }
    }
}
