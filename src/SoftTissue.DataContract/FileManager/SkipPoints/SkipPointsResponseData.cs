using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.FileManager.SkipPoints
{
    /// <summary>
    /// It represents the 'data' content of SkipPoints operation response.
    /// </summary>
    public class SkipPointsResponseData : OperationResponseData
    {
        /// <summary>
        /// The file URI.
        /// </summary>
        public string FileUri { get; set; }

        /// <summary>
        /// The file name with the new data.
        /// </summary>
        public string FileName { get; set; }
    }
}
