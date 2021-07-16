namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults
{
    /// <summary>
    /// It represents the 'data' content to CalculateResults operations request.
    /// </summary>
    public abstract class CalculateResultsRequestData
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
