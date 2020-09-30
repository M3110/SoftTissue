using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStress
{
    public class CalculateStressRequestData : OperationRequestData
    {
        public double Stiffness { get; set; }

        public double InitialStrain { get; set; }

        public double Viscosity { get; set; }
    }
}
