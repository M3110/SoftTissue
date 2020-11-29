using SoftTissue.DataContract.OperationBase;
using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Request
{
    public class CalculateQuasiLinearViscoelasticityStressRequest : OperationRequestBase
    {
        public List<CalculateQuasiLinearViscoelasticityStressRequestData> RequestDataList { get; set; }
    }
}
