﻿using SoftTissue.DataContract.Models;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime
{
    /// <summary>
    /// It represents the 'data' content to CalculateResultsConsiderRampTime operation request.
    /// </summary>
    /// <typeparam name="TRelaxationFunction"></typeparam>
    public abstract class CalculateQuasiLinearModelResultsConsiderRampTimeRequestData<TRelaxationFunction> : CalculateResultsRequestData
        where TRelaxationFunction : class
    {
        #region Relaxation parameters.

        /// <summary>
        /// The viscoelasctic consideration.
        /// </summary>
        public ViscoelasticConsideration ViscoelasticConsideration { get; set; }

        /// <summary>
        /// The number of relaxations considered in the analysis.
        /// Unit: Dimensionless.
        /// </summary>
        public int NumerOfRelaxations { get; set; }

        #endregion

        #region Strain parameters

        /// <summary>
        /// The analysis strain rate.
        /// Unit: Dimensionless.
        /// </summary>
        public double StrainRate { get; set; }

        /// <summary>
        /// The absolut strain decrease rate.
        /// Unit: Dimensionless.
        /// </summary>
        public double StrainDecreaseRate { get; set; }

        /// <summary>
        /// The maximum strain, this is obtained after the ramp time.
        /// Unit: Dimensionless.
        /// </summary>
        public double MaximumStrain { get; set; }

        /// <summary>
        /// The minimum strain, this is obtained after the decrease time.
        /// Unit: Dimensionless.
        /// </summary>
        public double MinimumStrain { get; set; }

        /// <summary>
        /// The time when the maximum strain is kept constant before strain decreases.
        /// Unit: s (second).
        /// </summary>
        public double TimeWithConstantMaximumStrain { get; set; }

        /// <summary>
        /// The time when the minimum strain is kept constant after the strain decreases.
        /// Unit: s (second).
        /// </summary>
        public double TimeWithConstantMinimumStrain { get; set; }

        #endregion

        #region Elastic Response parameters

        /// <summary>
        /// The elastic stress constant. Constant A.
        /// Unit: Pa (Pascal). In some cases, can be N (Newton).
        /// </summary>
        public double ElasticStressConstant { get; set; }

        /// <summary>
        /// The elastic power constant. Constant B.
        /// Unit: Dimensionless.
        /// </summary>
        public double ElasticPowerConstant { get; set; }

        #endregion

        #region Reduced Relaxation Function parameters

        /// <summary>
        /// The input data to Reduced Relaxation Function.
        /// </summary>
        public TRelaxationFunction ReducedRelaxationFunctionData { get; set; }

        #endregion
    }
}
