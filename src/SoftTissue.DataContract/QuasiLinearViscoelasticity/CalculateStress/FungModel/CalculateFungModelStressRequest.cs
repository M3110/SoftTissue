using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.FungModel
{
    public class CalculateFungModelStressRequest : OperationRequestBase
    {
        public bool UseSimplifiedReducedRelaxationFunction { get; set; }

        public IEnumerable<CalculateFungModelStressRequestData> RequestDataList { get; set; }
    }
}
