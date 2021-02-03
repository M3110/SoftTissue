namespace SoftTissue.DataContract.OperationBase
{
    /// <summary>
    /// It represents the 'data' content to operation request.
    /// </summary>
    public class OperationRequestData
    {
        /// <summary>
        /// The type of soft tissue.
        /// </summary>
        public string SoftTissueType { get; set; }

        /// <summary>
        /// Time step.
        /// Unit: s (second).
        /// </summary>
        public double? TimeStep { get; set; }

        /// <summary>
        /// Final time.
        /// Unit: s (second).
        /// </summary>
        public double? FinalTime { get; set; }
    }
}
