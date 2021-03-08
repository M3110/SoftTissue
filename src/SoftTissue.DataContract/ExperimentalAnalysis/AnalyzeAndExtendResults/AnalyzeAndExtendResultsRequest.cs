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
        /// Basic class constructor.
        /// </summary>
        public AnalyzeAndExtendResultsRequest() { }

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileUri"></param>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="useFileTimeStep"></param>
        [JsonConstructor]
        public AnalyzeAndExtendResultsRequest(
            string fileName,
            string fileUri,
            double timeStep,
            double finalTime,
            bool useFileTimeStep)
        {
            this.FileName = fileName;
            this.FileUri = fileUri;
            this.TimeStep = timeStep;
            this.FinalTime = finalTime;
            this.UseFileTimeStep = useFileTimeStep;
        }

        /// <summary>
        /// The file name with the experimental results.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The file URI.
        /// </summary>
        public string FileUri { get; set; }

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

        /// <summary>
        /// True, if should be used the time step at the file. False, if must be used the time step passed on request to extend the results.
        /// </summary>
        public bool UseFileTimeStep { get; set; }
    }
}
