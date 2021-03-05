namespace SoftTissue.Core.Models.Viscoelasticity
{
    /// <summary>
    /// It contains the results to a generic Viscoelastic Model.
    /// </summary>
    public class ViscoelasticModelResult
    {
        /// <summary>
        /// The time.
        /// Unit: s (second).
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// The soft tissue strain.
        /// Unit: dimensionless.
        /// </summary>
        public double Strain { get; set; }

        /// <summary>
        /// The value of reduced relaxation function to a specific time.
        /// Unit: dimensionless.
        /// </summary>
        public double ReducedRelaxationFunction { get; set; }

        /// <summary>
        /// The value of stress to a specific time.
        /// Unit: Pa (Pascal).
        /// </summary>
        public double Stress { get; set; }

        /// <summary>
        /// This method converts the result to a string separating the values by a specified separator.
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public virtual string ToString(string separator)
            => $"{this.Time}" +
            $"{separator}{this.Strain}" +
            $"{separator}{this.ReducedRelaxationFunction}" +
            $"{separator}{this.Stress}";

        /// <summary>
        /// This method converts the result to a string separating the values by comma.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{this.Strain},{this.ReducedRelaxationFunction},{this.Stress}";
    }
}
