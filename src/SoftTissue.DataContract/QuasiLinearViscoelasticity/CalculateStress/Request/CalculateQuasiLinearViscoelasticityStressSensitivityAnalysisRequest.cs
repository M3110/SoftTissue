using SoftTissue.DataContract.OperationBase;
using SoftTissue.Infrastructure.Models;
using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Request
{
    public class CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisRequest : OperationRequestBase
    {
        public bool UseSimplifiedReducedRelaxationFunction { get; set; }

        public IEnumerable<double> StrainRateList { get; set; }

        public IEnumerable<double> MaximumStrainList { get; set; }

        public IEnumerable<double> ElasticStressConstantList { get; set; }

        public IEnumerable<double> ElasticPowerConstantList { get; set; }

        /// <summary>
        /// Constant C.
        /// </summary>
        public IEnumerable<double> RelaxationIndexList { get; set; }

        /// <summary>
        /// Tau 1.
        /// </summary>
        public IEnumerable<double> FastRelaxationTimeList { get; set; }

        /// <summary>
        /// Tau 2.
        /// </summary>
        public IEnumerable<double> SlowRelaxationTimeList { get; set; }

        public IEnumerable<SimplifiedReducedRelaxationFunctionData> SimplifiedReducedRelaxationFunctionDataList { get; set; }
    }
}
