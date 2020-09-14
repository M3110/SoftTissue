using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress
{
    public class CalculateQuasiLinearViscoelasticityStressRequest : OperationRequestBase
    {
        /// <summary>
        /// A list with the initial strain.
        /// If the initial strain is equals to zero, it does not need to be informed.
        /// </summary>
        public IEnumerable<double> InitialStainList { get; set; }

        /// <summary>
        /// A list with the experiment strain ratio.
        /// </summary>
        public IEnumerable<double> StrainRateList { get; set; }

        public IEnumerable<double> MaximumStrainList { get; set; }

        /// <summary>
        /// A list with the time when strain is zero.
        /// </summary>
        public IEnumerable<double> StrainFinalTimeList { get; set; }

        public IEnumerable<double> VariableAList { get; set; }

        public IEnumerable<double> VariableBList { get; set; }

        public IEnumerable<double> VariableCList { get; set; }

        public IEnumerable<double> RelaxationTime1List { get; set; }
        
        public IEnumerable<double> RelaxationTime2List { get; set; }
    }
}
