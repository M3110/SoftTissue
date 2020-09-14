using SoftTissue.DataContract.Models;
using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.SimplifiedG
{
    public class CalculateQuasiLinearViscoelasticityStressToSimplifiedGRequest
    {
        public IEnumerable<RelaxationFunctionSimplifiedInput> RelaxationFunctionSimplifiedInputList { get; set; }
    }
}
