using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain
{
    /// <summary>
    /// It represents the 'data' content to CalculateStrain operation request of Linear Viscoelasticity Model.
    /// </summary>
    public class CalculateStrainRequestData : OperationRequestData
    {
        /// <summary>
        /// Stiffness.
        /// Unity: Pa (Pascal).
        /// </summary>
        public double Stiffness { get; set; }

        /// <summary>
        /// Inital stress.
        /// Unity: Pa (Pascal).
        /// </summary>
        public double InitialStress { get; set; }

        /// <summary>
        /// Viscosity.
        /// Unity: N.s/m (Newton-second per meter).
        /// </summary>
        public double Viscosity { get; set; }
    }
}
