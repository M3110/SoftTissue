using Newtonsoft.Json;
using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.FileManager.SkipPoints
{
    /// <summary>
    /// It represents the request content to SkipPoints operation.
    /// </summary>
    public sealed class SkipPointsRequest : OperationRequestBase
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="fileUri"></param>
        /// <param name="fileName"></param>
        /// <param name="pointsToSkip"></param>
        [JsonConstructor]
        public SkipPointsRequest(string fileUri, string fileName, int pointsToSkip)
        {
            this.FileUri = fileUri;
            this.FileName = fileName;
            this.PointsToSkip = pointsToSkip;
        }

        /// <summary>
        /// The file URI.
        /// </summary>
        public string FileUri { get; private set; }

        /// <summary>
        /// The file name to be used in the operation.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// The number of points to be skipped in the file.
        /// </summary>
        public int PointsToSkip { get; private set; }
    }
}
