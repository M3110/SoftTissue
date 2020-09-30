using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain
{
    public class CalculateStrainRequestData : OperationRequestData
    {
        public double Stiffness { get; set; }

        public double InitialStress { get; set; }

        public double Viscosity { get; set; }
    }
}
