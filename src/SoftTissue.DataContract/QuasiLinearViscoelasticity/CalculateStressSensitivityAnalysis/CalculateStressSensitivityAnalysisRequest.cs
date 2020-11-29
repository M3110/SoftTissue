using SoftTissue.DataContract.OperationBase;
using SoftTissue.Infrastructure.Models;
using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStressSensitivityAnalysis
{
    /// <summary>
    /// It represents the request content to CalculateStressSensitivityAnalysis operation of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public class CalculateStressSensitivityAnalysisRequest : OperationRequestBase
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
