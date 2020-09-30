namespace SoftTissue.Core.Models.Viscoelasticity
{
    public class QuasiLinearViscoelasticityModelInput : ViscoelasticModelInput
    {
        public double StrainRate { get; set; }

        public double MaximumStrain { get; set; }

        /// <summary>
        /// Constant A.
        /// </summary>
        public double ElasticStressConstant { get; set; }

        /// <summary>
        /// Constant B.
        /// </summary>
        public double ElasticPowerConstant { get; set; }
    }
}