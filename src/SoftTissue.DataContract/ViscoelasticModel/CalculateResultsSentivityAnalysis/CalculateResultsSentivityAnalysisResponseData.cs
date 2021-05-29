using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSentivityAnalysis
{
    /// <summary>
    /// It represents the 'data' content of CalculateResultsSentivityAnalysis operations response.
    /// </summary>
    public class CalculateResultsSentivityAnalysisResponseData : OperationResponseData
    {
        /// <summary>
        /// The list of result file path.
        /// </summary>
        public string FilePath { get; set; }
    }
}
