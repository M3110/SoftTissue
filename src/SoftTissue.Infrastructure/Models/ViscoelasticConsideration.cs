namespace SoftTissue.Infrastructure.Models
{
    /// <summary>
    /// It contains the viscoelasticy considerations to analysis.
    /// At all quasi-linear analysis, is considered that the strain can just have one increase and one decrease.
    /// </summary>
    public enum ViscoelasticConsideration
    {
        /// <summary>
        /// Viscoelastic effect considered to all time domain considering a constant strain after the ramp time.
        /// </summary>
        GeneralViscoelasctiEffect = 1,

        /// <summary>
        /// Viscoelastic effect considered to all time domain considering that the strain decrease after a specific time.
        /// </summary>
        GeneralViscoelasticEffectWithStrainDecrease = 2,

        /// <summary>
        /// Viscoelastic effect considered after the ramp time.
        /// </summary>
        ViscoelasticEffectAfterRampTime = 3,

        /// <summary>
        /// Viscoelastic effect considered after the ramp time and the strain decrease after a specific time.
        /// </summary>
        ViscoelasticEffectAfterRampTimeWithStrainDecrease = 4,

        /// <summary>
        /// The ramp time is disregarded. In time equals to zero, the stress is maximum and equals to the maximum elastic stress.
        /// </summary>
        DisregardRampTime = 5,
    }
}
