using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSensitivityAnalysis
{
    /// <summary>
    /// It represents the 'data' content of CalculateResultsSensitivityAnalysis operations response.
    /// </summary>
    public class CalculateResultsSensitivityAnalysisResponseData : OperationResponseData
    {
        /// <summary>
        /// The list of result file path.
        /// </summary>
        public string FilePath { get; set; }
    }
}
