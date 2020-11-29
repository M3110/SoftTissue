using SoftTissue.Infrastructure.Models;

namespace SoftTissue.Core.Models.Viscoelasticity
{
    /// <summary>
    /// It contains the input data to a generic Viscoelastic Model.
    /// </summary>
    public class ViscoelasticModelInput
    {
        /// <summary>
        /// The viscoelasctic consideration.
        /// </summary>
        public ViscoelasticConsideration ViscoelasticConsideration { get; set; }

        /// <summary>
        /// The soft tissue type.
        /// </summary>
        public string SoftTissueType { get; set; }

        /// <summary>
        /// Initial time.
        /// Unity: s (second).
        /// </summary>
        public double InitialTime { get; set; }

        /// <summary>
        /// Time step.
        /// Unity: s (second).
        /// </summary>
        public double TimeStep { get; set; }

        /// <summary>
        /// Final time.
        /// Unity: s (second).
        /// </summary>
        public double FinalTime { get; set; }
    }
}
