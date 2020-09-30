using SoftTissue.DataContract.OperationBase;
using System.Collections.Generic;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain
{
    /// <summary>
    /// It represents the essencial request content to CalculateStrain operation.
    /// </summary>
    public class CalculateStrainRequest : OperationRequestBase
    {
        public List<CalculateStrainRequestData> RequestDataList { get; set; }
    }
}
