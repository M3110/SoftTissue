namespace SoftTissue.Infrastructure.Models
{
    /// <summary>
    /// It contains the viscoelasticy considerations to analysis.
    /// </summary>
    public enum ViscoelasticConsideration
    {
        /// <summary>
        /// Viscoelastic effect considered to all time domain.
        /// </summary>
        GeneralViscoelasctiEffect = 1,

        /// <summary>
        /// Viscoelastic effect considered after the ramp time.
        /// </summary>
        ViscoelasticEffectAfterRampTime = 2,

        /// <summary>
        /// The ramp time is not considered. In time equals to zero, the 
        /// stress is maximum and equals to the maximum elastic stress.
        /// </summary>
        RampTimeNotConsidered = 3,
    }
}
