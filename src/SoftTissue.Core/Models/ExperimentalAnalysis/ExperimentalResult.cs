namespace SoftTissue.Core.Models.ExperimentalAnalysis
{
    /// <summary>
    /// It contains the data of experimental result.
    /// </summary>
    public class ExperimentalResult
    {
        /// <summary>
        /// The time.
        /// Unit: s (second).
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// The stress.
        /// OBS.: Only here the stress is at Mega-Pascal.
        /// Unit: MPa (Mega-Pascal).
        /// </summary>
        public double Stress { get; set; }
    }
}
