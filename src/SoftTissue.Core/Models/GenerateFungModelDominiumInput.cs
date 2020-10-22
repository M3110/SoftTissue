namespace SoftTissue.Core.Models
{
    public class GenerateFungModelDominiumInput
    {
        /// <summary>
        /// Tau 1.
        /// </summary>
        public double FastRelaxationTimeList { get; set; }

        /// <summary>
        /// Tau 2.
        /// </summary>
        public double SlowRelaxationTimeList { get; set; }
    }
}
