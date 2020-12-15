using SoftTissue.Infrastructure.Models;

namespace SoftTissue.Core.Models.Viscoelasticity
{
    /// <summary>
    /// It contains the input data to a generic Viscoelastic Model.
    /// </summary>
    public class ViscoelasticModelInput
    {
        /// <summary>
        /// The soft tissue type.
        /// </summary>
        public string SoftTissueType { get; set; }

        /// <summary>
        /// Initial time.
        /// Unit: s (second).
        /// </summary>
        public double InitialTime { get; set; }

        /// <summary>
        /// Time step.
        /// Unit: s (second).
        /// </summary>
        public double TimeStep { get; set; }

        /// <summary>
        /// Final time.
        /// Unit: s (second).
        /// </summary>
        public double FinalTime { get; set; }
    }
}
