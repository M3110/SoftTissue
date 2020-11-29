using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It represents the 'data' content to CalculateStress operation request of Linear Viscoelasticity Model.
    /// </summary>
    public class CalculateStressRequestData : OperationRequestData
    {
        /// <summary>
        /// Stiffness.
        /// Unity: Pa (Pascal).
        /// </summary>
        public double Stiffness { get; set; }

        /// <summary>
        /// Inital strain.
        /// Unity: Dimensionless.
        /// </summary>
        public double InitialStrain { get; set; }

        /// <summary>
        /// Viscosity.
        /// Unity: N.s/m (Newton-second per meter).
        /// </summary>
        public double Viscosity { get; set; }
    }
}
