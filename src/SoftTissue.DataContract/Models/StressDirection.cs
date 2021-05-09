namespace SoftTissue.DataContract.Models
{
    /// <summary>
    /// It represents the direction of stress, indicating if it is increasing or decreasing.
    /// </summary>
    public enum StressDirection
    {
        /// <summary>
        /// The stress increase during the ramp time.
        /// </summary>
        Increase = 1,

        /// <summary>
        /// The stress decrease after the ramp time.
        /// </summary>
        Decrease = 2
    }
}
