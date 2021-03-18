using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.CalculateResult
{
    /// <summary>
    /// It represents the 'data' content of CalculateResult operations response.
    /// </summary>
    public abstract class CalculateResultResponseData : OperationResponseData
    {
        /// <summary>
        /// The result file name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The file URI.
        /// </summary>
        public string FileUri { get; set; }
    }
}
