using SoftTissue.DataContract.OperationBase;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Request
{
    public class CalculateQuasiLinearViscoelasticityStressRequestData : OperationRequestData
    {
        public bool UseSimplifiedReducedRelaxationFunction { get; set; }

        /// <summary>
        /// The analysis strain rate.
        /// </summary>
        public double StrainRate { get; set; }

        /// <summary>
        /// The meximum strain imposed to analysis.
        /// </summary>
        public double MaximumStrain { get; set; }

        /// <summary>
        /// Constant A.
        /// </summary>
        public double ElasticStressConstant { get; set; }

        /// <summary>
        /// Constant B.
        /// </summary>
        public double ElasticPowerConstant { get; set; }

        public ReducedRelaxationFunctionData ReducedRelaxationFunctionData { get; set; }

        public SimplifiedReducedRelaxationFunctionData SimplifiedReducedRelaxationFunctionData { get; set; }
    }
}
