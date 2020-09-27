using SoftTissue.DataContract.Models;
using System.Collections.Generic;

namespace SoftTissue.Core.Models
{
    public class QuasiLinearViscoelasticityModelInput : ViscoelasticModelInput
    {
        public double StrainRate { get; set; }

        public double MaximumStrain { get; set; }

        /// <summary>
        /// Constant A.
        /// </summary>
        public double ElasticStressConstant { get; set; }
        
        /// <summary>
        /// Constant B.
        /// </summary>
        public double ElasticPowerConstant { get; set; }
        
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

        //public double StrainFinalTime { get; set; }
    }
}