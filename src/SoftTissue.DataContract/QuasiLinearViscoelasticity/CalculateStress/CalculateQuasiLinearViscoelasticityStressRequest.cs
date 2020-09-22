using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress
{
    public class CalculateQuasiLinearViscoelasticityStressRequest : OperationRequestBase
    {
        /// <summary>
        /// A list with the experiment strain ratio.
        /// </summary>
        public IEnumerable<double> StrainRateList { get; set; }

        public IEnumerable<double> MaximumStrainList { get; set; }

        public IEnumerable<double> ElasticStressConstantList { get; set; }

        public IEnumerable<double> ElasticPowerConstantList { get; set; }

        public IEnumerable<double> RelaxationIndexList { get; set; }

        public IEnumerable<double> FastRelaxationTimeList { get; set; }
        
        public IEnumerable<double> SlowRelaxationTimeList { get; set; }
    }
}
