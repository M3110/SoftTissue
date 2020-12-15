using System.Collections.Generic;

namespace SoftTissue.Infrastructure.Models
{
    /// <summary>
    /// It contains the input data to Simplified Reduced Relaxation Function.
    /// </summary>
    public class SimplifiedReducedRelaxationFunctionData
    {
        /// <summary>
        /// The first viscoelastic stiffness. This variable is independent.
        /// </summary>
        public double FirstViscoelasticStiffness { get; set; }

        /// <summary>
        /// The values for each iteration to Simplified Reduced Relaxation Function.
        /// </summary>
        public IEnumerable<SimplifiedReducedRelaxationFunctionIteratorValues> IteratorValues { get; set; }
    }
}
