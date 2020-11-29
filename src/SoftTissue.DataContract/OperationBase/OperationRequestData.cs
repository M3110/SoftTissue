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
        /// Initial time.
        /// Unity: s (second).
        /// </summary>
        public double? InitialTime { get; set; }

        /// <summary>
        /// Time step.
        /// Unity: s (second).
        /// </summary>
        public double? TimeStep { get; set; }

        /// <summary>
        /// Final time.
        /// Unity: s (second).
        /// </summary>
        public double? FinalTime { get; set; }
    }
}
