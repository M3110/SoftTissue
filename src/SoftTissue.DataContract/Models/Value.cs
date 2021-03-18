namespace SoftTissue.DataContract.Models
{
    /// <summary>
    /// It contains the parameters to build a list of values.
    /// </summary>
    public class Value
    {
        /// <summary>
        /// The initial point.
        /// </summary>
        public double InitialPoint { get; set; }

        /// <summary>
        /// The step used to iterate from initial point to final point.
        /// </summary>
        public double? Step { get; set; }

        /// <summary>
        /// The final point.
        /// </summary>
        public double? FinalPoint { get; set; }
    }
}
