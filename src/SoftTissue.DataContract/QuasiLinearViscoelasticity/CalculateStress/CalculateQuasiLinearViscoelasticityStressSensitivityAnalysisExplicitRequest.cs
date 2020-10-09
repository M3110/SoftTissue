using SoftTissue.Infrastructure.Models;
using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress
{
    public class CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisExplicitRequest
    {
        public bool UseSimplifiedReducedRelaxationFunction { get; set; }

        public Value StrainRateList { get; set; }

        public Value MaximumStrainList { get; set; }

        public Value ElasticStressConstantList { get; set; }

        public Value ElasticPowerConstantList { get; set; }

        public ReducedRelaxationFunctionDataList ReducedRelaxationFunctionDataList { get; set; }

        public IEnumerable<SimplifiedReducedRelaxationFunctionData> SimplifiedReducedRelaxationFunctionDataList { get; set; }
    }

    public class ReducedRelaxationFunctionDataList
    {
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
    }
}
