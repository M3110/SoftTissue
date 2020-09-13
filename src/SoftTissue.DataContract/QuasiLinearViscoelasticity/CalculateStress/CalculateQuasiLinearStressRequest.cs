using SoftTissue.DataContract.Models;
using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress
{
    public class CalculateQuasiLinearStressRequest : OperationRequestBase
    {
        /// <summary>
        /// The initial strain.
        /// If the initial strain is equals to zero, it does not need to be informed.
        /// </summary>
        public double InitialStain { get; set; }

        public double MaximumStrain { get; set; }

        /// <summary>
        /// The time when strain is zero.
        /// </summary>
        public double StrainFinalTime { get; set; }

        /// <summary>
        /// The experiment strain ratio.
        /// </summary>
        public double StrainRatio { get; set; }

        public double VariableA { get; set; }

        public double VariableB { get; set; }

        public IEnumerable<RelaxationFunctionSimplifiedInput> RelaxationFunctionSimplifiedInputList { get; set; }
    }
}
