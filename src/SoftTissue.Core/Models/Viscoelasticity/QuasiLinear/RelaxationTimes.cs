namespace SoftTissue.Core.Models.Viscoelasticity.QuasiLinear
{
    /// <summary>
    /// It contains the important relaxation times.
    /// </summary>
    public struct RelaxationTimes
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="strainDecreaseStartTime"></param>
        /// <param name="strainDecreaseFinalTime"></param>
        /// <param name="strainIncreaseStartTime"></param>
        /// <param name="strainIncreaseFinalTime"></param>
        public RelaxationTimes(
            double strainDecreaseStartTime, 
            double strainDecreaseFinalTime, 
            double strainIncreaseStartTime, 
            double strainIncreaseFinalTime)
        {
            this.StrainDecreaseStartTime = strainDecreaseStartTime;
            this.StrainDecreaseFinalTime = strainDecreaseFinalTime;
            this.StrainIncreaseStartTime = strainIncreaseStartTime;
            this.StrainIncreaseFinalTime = strainIncreaseFinalTime;
        }

        /// <summary>
        /// The time when the strain starts to decrease.
        /// </summary>
        public double StrainDecreaseStartTime { get; }

        /// <summary>
        /// The time when the strain ends to decrease.
        /// </summary>
        public double StrainDecreaseFinalTime { get; }

        /// <summary>
        /// The time when the strain starts to increase.
        /// </summary>
        public double StrainIncreaseStartTime { get; }

        /// <summary>
        /// The time when the strain ends to increase.
        /// </summary>
        public double StrainIncreaseFinalTime { get; }

        public bool IsEmpty()
        {
            if (this.StrainDecreaseStartTime == 0 && this.StrainDecreaseFinalTime == 0 &&
                this.StrainIncreaseStartTime == 0 && this.StrainIncreaseFinalTime == 0)
                return true;

            return false;
        }
    }
}
