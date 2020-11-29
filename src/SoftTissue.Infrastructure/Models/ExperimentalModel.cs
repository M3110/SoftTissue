namespace SoftTissue.Infrastructure.Models
{
    /// <summary>
    /// It contains the soft tissue models tested in the laboratory. 
    /// </summary>
    public enum ExperimentalModel
    {
        /// <summary>
        /// First relaxation for Posterior Cruciate Ligament (PCL).
        /// </summary>
        PosteriorCruciateLigamentFirstRelaxation = 1,

        /// <summary>
        /// Second relaxation for Posterior Cruciate Ligament (PCL).
        /// </summary>
        PosteriorCruciateLigamentSecondRelaxation = 2,

        /// <summary>
        /// First relaxation for Medial collateral ligament (MCL).
        /// </summary>
        MedialCollateralLigamentFirstRelaxation = 3,

        /// <summary>
        /// Second relaxation for Medial collateral ligament (MCL).
        /// </summary>
        MedialCollateralLigamentSecondRelaxation = 4,

        /// <summary>
        /// First relaxation Lateral Collateral Ligament (LCL).
        /// </summary>
        LateralCollateralLigamentFirstRelaxation = 5,

        /// <summary>
        /// Second relaxation Lateral Collateral Ligament (LCL).
        /// </summary>
        LateralCollateralLigamentSecondRelaxation = 6,

        /// <summary>
        /// First relaxation Anterior Cruciate Ligament (ACL).
        /// </summary>
        AnteriorCruciateLigamentFirstRelaxation = 7,

        /// <summary>
        /// Second relaxation Anterior Cruciate Ligament (ACL).
        /// </summary>
        AnteriorCruciateLigamentSecondRelaxation = 8
    }
}