using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.FungModel
{
    public class CalculateFungModelStressSensitivityAnalysisRequest : OperationRequestBase
    {
        public bool UseSimplifiedReducedRelaxationFunction { get; set; }

        public IEnumerable<double> StrainRateList { get; set; }

        public IEnumerable<double> MaximumStrainList { get; set; }

        public IEnumerable<double> ElasticStressConstantList { get; set; }

        public IEnumerable<double> ElasticPowerConstantList { get; set; }

        public IEnumerable<double> RelaxationIndexList { get; set; }

        public IEnumerable<double> FastRelaxationTimeList { get; set; }

        public IEnumerable<double> SlowRelaxationTimeList { get; set; }
    }
}
