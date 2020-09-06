using System.Collections.Generic;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It represents the essencial request content to CalculateStress operation.
    /// </summary>
    public class CalculateStressRequest : OperationRequestBase
    {
        public List<double> StiffnessList { get; set; }

        public List<double> InitialStrainList { get; set; }

        public List<double> ViscosityList { get; set; }
    }
}
