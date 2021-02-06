using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain
{
    /// <summary>
    /// It represents the 'data' content to CalculateStrain operation request of Linear Viscoelasticity Model.
    /// </summary>
    public class CalculateStrainRequestData : CalculateResultRequestData
    {
        /// <summary>
        /// Stiffness.
        /// Unit: Pa (Pascal).
        /// </summary>
        public double Stiffness { get; set; }

        /// <summary>
        /// Inital stress.
        /// Unit: Pa (Pascal).
        /// </summary>
        public double InitialStress { get; set; }

        /// <summary>
        /// Viscosity.
        /// Unit: N.s/m (Newton-second per meter).
        /// </summary>
        public double Viscosity { get; set; }
    }
}
