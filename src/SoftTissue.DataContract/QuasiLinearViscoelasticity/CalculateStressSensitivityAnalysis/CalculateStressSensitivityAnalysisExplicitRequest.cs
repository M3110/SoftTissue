using SoftTissue.DataContract.CalculateResult;
using SoftTissue.Infrastructure.Models;
using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStressSensitivityAnalysis
{
    /// <summary>
    /// It represents the request content to CalculateStressSensitivityAnalysisExplicit operation of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public class CalculateStressSensitivityAnalysisExplicitRequest : CalculateResultRequest
    {
        /// <summary>
        /// True, if have to use the simplified Reduced Relaxation Function.
        /// False, otherwise.
        /// </summary>
        public bool UseSimplifiedReducedRelaxationFunction { get; set; }

        /// <summary>
        /// The viscoelasctic consideration.
        /// </summary>
        public ViscoelasticConsideration ViscoelasticConsideration { get; set; }

        public Value StrainRateList { get; set; }

        public Value MaximumStrainList { get; set; }

        public Value ElasticStressConstantList { get; set; }

        public Value ElasticPowerConstantList { get; set; }

        /// <summary>
        /// Constant C.
        /// </summary>
        public Value RelaxationIndexList { get; set; }

        /// <summary>
        /// Tau 1.
        /// </summary>
        public Value FastRelaxationTimeList { get; set; }

        /// <summary>
        /// Tau 2.
        /// </summary>
        public Value SlowRelaxationTimeList { get; set; }

        public IEnumerable<SimplifiedReducedRelaxationFunctionData> SimplifiedReducedRelaxationFunctionDataList { get; set; }
    }
}
