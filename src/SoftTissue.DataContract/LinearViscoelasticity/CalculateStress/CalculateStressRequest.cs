using System.Collections.Generic;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It represents the essencial request content to CalculateStress operation.
    /// </summary>
    public class CalculateStressRequest : OperationRequestBase
    {
        public List<CalculateStressRequestData> RequestDataList { get; set; }
    }
}
