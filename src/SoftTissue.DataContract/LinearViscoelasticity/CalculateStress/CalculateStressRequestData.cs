using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It represents the 'data' content to CalculateStress operation request of Linear Viscoelasticity Model.
    /// </summary>
    public sealed class CalculateStressRequestData : CalculateResultRequestData
    {
        /// <summary>
        /// Stiffness.
        /// Unit: Pa (Pascal).
        /// </summary>
        public double Stiffness { get; set; }

        /// <summary>
        /// Inital strain.
        /// Unit: Dimensionless.
        /// </summary>
        public double InitialStrain { get; set; }

        /// <summary>
        /// Viscosity.
        /// Unit: N.s/m (Newton-second per meter).
        /// </summary>
        public double Viscosity { get; set; }
    }
}
