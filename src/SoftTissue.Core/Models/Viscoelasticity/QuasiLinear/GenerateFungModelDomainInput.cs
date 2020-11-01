namespace SoftTissue.Core.Models.Viscoelasticity.QuasiLinear
{
    public class GenerateFungModelDomainInput : ViscoelasticModelInput
    {
        /// <summary>
        /// Tau 1.
        /// </summary>
        public double FastRelaxationTime { get; set; }

        /// <summary>
        /// Tau 2.
        /// </summary>
        public double SlowRelaxationTime { get; set; }
    }
}
