using SoftTissue.DataContract.OperationBase;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Request
{
    /// <summary>
    /// It represents the request content to CalculateQuasiLinearViscoelasticity operation for a soft tissue experimental model.
    /// </summary>
    public class CalculateQuasiLinearViscoelasticityStressToExperimentalModelRequest : OperationRequestBase
    {
        public ExperimentalModel ExperimentalModel { get; set; }

        public double StrainRate { get; set; }

        public double MaximumStrain { get; set; }
    }
}
