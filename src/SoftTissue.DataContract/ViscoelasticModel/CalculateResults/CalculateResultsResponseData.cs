using SoftTissue.DataContract.OperationBase;
using System.Collections.Generic;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults
{
    /// <summary>
    /// It represents the 'data' content of CalculateResults operations response.
    /// </summary>
    public class CalculateResultsResponseData : OperationResponseData
    {
        /// <summary>
        /// The list of result file path.
        /// </summary>
        public List<string> FilePaths { get; set; }
    }
}
