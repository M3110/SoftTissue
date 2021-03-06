﻿namespace SoftTissue.Core.Models.ExperimentalAnalysis
{
    /// <summary>
    /// It contains the data of experimental result and its variation rates.
    /// </summary>
    public class AnalyzedExperimentalResult
    {
        /// <summary>
        /// Basic class constructor.
        /// </summary>
        public AnalyzedExperimentalResult() { }

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="experimentalResult"></param>
        public AnalyzedExperimentalResult(ExperimentalResult experimentalResult)
        {
            this.Time = experimentalResult.Time;
            this.Stress = experimentalResult.Stress;
        }

        /// <summary>
        /// The time.
        /// Unit: s (second).
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// The stress.
        /// OBS.: Only here the stress is at Mega-Pascal.
        /// Unit: MPa (Mega-Pascal).
        /// </summary>
        public double Stress { get; set; }

        /// <summary>
        /// The stress derivative.
        /// Unit: MPa/s (Mega-Pascal per second).
        /// </summary>
        public double? Derivative { get; set; }

        /// <summary>
        /// The stress second derivative.
        /// Unit: MPa/s² (Mega-Pascal per second squared).
        /// </summary>
        public double? SecondDerivative { get; set; }

        /// <summary>
        /// True, if the result is valid. Otherwise, false.
        /// </summary>
        public bool IsValid { get; set; }
    }
}
