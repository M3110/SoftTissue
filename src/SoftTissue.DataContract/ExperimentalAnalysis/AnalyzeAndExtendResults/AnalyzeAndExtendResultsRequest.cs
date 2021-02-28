using Newtonsoft.Json;
using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.ExperimentalAnalysis.AnalyzeAndExtendResults
{
    /// <summary>
    /// It represents the request content to AnalyzeAndPredictResults operation.
    /// </summary>
    public sealed class AnalyzeAndExtendResultsRequest : OperationRequestBase
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileUri"></param>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        [JsonConstructor]
        public AnalyzeAndExtendResultsRequest(
            string fileName, 
            string fileUri, 
            double timeStep, 
            double finalTime)
        {
            this.FileName = fileName;
            this.FileUri = fileUri;
            this.TimeStep = timeStep;
            this.FinalTime = finalTime;
        }

        /// <summary>
        /// The file name with the experimental results.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// The file URI.
        /// </summary>
        public string FileUri { get; private set; }

        /// <summary>
        /// Time step.
        /// Unit: s (second).
        /// </summary>
        public double TimeStep { get; private set; }

        /// <summary>
        /// Final time.
        /// Unit: s (second).
        /// </summary>
        public double FinalTime { get; private set; }
    }
}
