namespace SoftTissue.Core.Models.Viscoelasticity.QuasiLinear
{
    public class GenerateFungModelDomainInput : ViscoelasticModelInput
    {
        /// <summary>
        /// The fast relaxation time. Tau 1.
        /// Unit: s (second).
        /// </summary>
        public double FastRelaxationTime { get; set; }

        /// <summary>
        /// The slow relaxation time. Tau 2.
        /// Unit: s (second).
        /// </summary>
        public double SlowRelaxationTime { get; set; }
    }
}
