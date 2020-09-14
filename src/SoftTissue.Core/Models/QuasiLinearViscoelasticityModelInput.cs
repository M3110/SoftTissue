using SoftTissue.DataContract.Models;
using System.Collections.Generic;

namespace SoftTissue.Core.Models
{
    public class QuasiLinearViscoelasticityModelInput : ViscoelasticModelInput
    {
        public double InitialStrain { get; set; }

        public double StrainRate { get; set; }

        public double MaximumStrain { get; set; }

        public double StrainFinalTime { get; set; }

        public double VariableA { get; set; }

        public double VariableB { get; set; }
        
        public double VariableC { get; set; }

        public double RelaxationTime1 { get; set; }

        public double RelaxationTime2 { get; set; }

        public List<RelaxationFunctionSimplifiedInput> RelaxationFunctionSimplifiedInputList { get; set; }
    }
}