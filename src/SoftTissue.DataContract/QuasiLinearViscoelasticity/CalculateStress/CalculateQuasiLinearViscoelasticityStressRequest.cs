using SoftTissue.DataContract.OperationBase;
using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress
{
    public class CalculateQuasiLinearViscoelasticityStressRequest : OperationRequestBase
    {
        public IEnumerable<CalculateQuasiLinearViscoelasticityStressRequestData> RequestDataList { get; set; }
    }
}
