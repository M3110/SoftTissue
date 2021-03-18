namespace SoftTissue.DataContract.Models
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
        /// Viscoelastic effect considered after the ramp time.
        /// </summary>
        ViscoelasticEffectAfterRampTime = 2,

        /// <summary>
        /// The ramp time is disregarded. In time equals to zero, the stress is maximum and equals to the maximum elastic stress.
        /// </summary>
        DisregardRampTime = 3,
    }
}
