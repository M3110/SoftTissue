namespace SoftTissue.DataContract.Models
{
    /// <summary>
    /// It contains the values for each iteration to Simplified Reduced Relaxation Function.
    /// </summary>
    public class SimplifiedReducedRelaxationFunctionIteratorValues
    {
        /// <summary>
        /// The viscoelastic stiffness. The constante that multiplies the exponencial.
        /// </summary>
        public double ViscoelasticStiffness { get; set; }

        /// <summary>
        /// The relaxation time.
        /// Unit: s (second).
        /// </summary>
        public double RelaxationTime { get; set; }
    }
}
