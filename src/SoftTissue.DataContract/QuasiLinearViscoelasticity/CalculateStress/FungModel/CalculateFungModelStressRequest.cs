using SoftTissue.DataContract.OperationBase;
using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.FungModel
{
    public class CalculateFungModelStressRequest : OperationRequestBase
    {
        public IEnumerable<CalculateFungModelStressRequestData> RequestDataList { get; set; }
    }
}
