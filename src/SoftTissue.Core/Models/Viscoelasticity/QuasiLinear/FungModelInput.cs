using SoftTissue.DataContract.Models;
using System.Collections.Generic;

namespace SoftTissue.Core.Models.Viscoelasticity.QuasiLinear
{
    public class FungModelInput : QuasiLinearViscoelasticityModelInput
    {
        public bool UseSimplifiedReducedRelaxationFunction { get; set; }

        public ReducedRelaxationFunctionInput ReducedRelaxationFunctionInput { get; set; }

        public List<SimplifiedRelaxationFunctionRequestData> SimplifiedReducedRelaxationFunctionInputList { get; set; }
    }

    public class ReducedRelaxationFunctionInput
    {
        /// <summary>
        /// Constant C.
        /// </summary>
        public double RelaxationIndex { get; set; }

        /// <summary>
        /// Tau 1.
        /// </summary>
        public double FastRelaxationTime { get; set; }

        /// <summary>
        /// Tau 2.
        /// </summary>
        public double SlowRelaxationTime { get; set; }
    }
}
