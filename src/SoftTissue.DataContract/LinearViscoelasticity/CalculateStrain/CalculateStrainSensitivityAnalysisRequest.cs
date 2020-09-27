using System.Collections.Generic;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain
{
    public class CalculateStrainSensitivityAnalysisRequest : OperationRequestBase
    {
        public List<double> StiffnessList { get; set; }

        public List<double> InitialStressList { get; set; }

        public List<double> ViscosityList { get; set; }
    }
}
