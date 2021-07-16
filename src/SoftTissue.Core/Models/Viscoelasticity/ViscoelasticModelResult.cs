namespace SoftTissue.Core.Models.Viscoelasticity
{
    /// <summary>
    /// It contains the results for a generic Viscoelastic Model.
    /// </summary>
    public class ViscoelasticModelResult
    {
        /// <summary>
        /// Unit: s (second).
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// Dimensioless.
        /// </summary>
        public double Strain { get; set; }

        /// <summary>
        /// Unit: Pa (Pascal).
        /// </summary>
        public double Stress { get; set; }

        /// <summary>
        /// This method returns a string that represents the current object with each element separated by comma.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{this.Time},{this.Strain},{this.Stress}";
    }
}
