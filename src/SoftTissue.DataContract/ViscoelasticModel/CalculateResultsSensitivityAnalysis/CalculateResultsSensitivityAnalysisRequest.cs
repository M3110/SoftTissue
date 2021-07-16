using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSensitivityAnalysis
{
    /// <summary>
    /// It represents the request content to CalculateResultsSensitivityAnalysis operations.
    /// </summary>
    public abstract class CalculateResultsSensitivityAnalysisRequest : OperationRequestBase
    {
        /// <summary>
        /// Time step.
        /// Unit: s (second).
        /// </summary>
        public double TimeStep { get; set; }

        /// <summary>
        /// Final time.
        /// Unit: s (second).
        /// </summary>
        public double FinalTime { get; set; }
    }
}
