using System.Collections.Generic;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain
{
    /// <summary>
    /// It represents the essencial request content to CalculateStrain operation.
    /// </summary>
    public class CalculateStrainRequest : OperationRequestBase
    {
        public List<double> StiffnessList { get; set; }

        public List<double> InitialStressList { get; set; }

        public List<double> ViscosityList { get; set; }
    }
}
