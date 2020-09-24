using System.Collections.Generic;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStress
{
    public class CalculateStressSensitivityTestRequest : OperationRequestBase
    {
        public List<double> StiffnessList { get; set; }

        public List<double> InitialStrainList { get; set; }

        public List<double> ViscosityList { get; set; }
    }
}
