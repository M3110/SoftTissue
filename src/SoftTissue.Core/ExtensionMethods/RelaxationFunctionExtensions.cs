using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.Core.ExtensionMethods
{
    /// <summary>
    /// It contains extension methods for relaxation functions.
    /// </summary>
    public static class RelaxationFunctionExtensions
    {
        /// <summary>
        /// This method validates if the parameters for Reduced Relaxation Function is valid and writes the errors in response.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="reducedRelaxationFunctionData"></param>
        /// <param name="response"></param>
        public static void Validate<TResponse>(this ReducedRelaxationFunctionData reducedRelaxationFunctionData, TResponse response)
            where TResponse : OperationResponseBase
        {
            response
                .AddErrorIfNegativeOrZero(reducedRelaxationFunctionData.FastRelaxationTime, nameof(reducedRelaxationFunctionData.FastRelaxationTime))
                .AddErrorIfNegativeOrZero(reducedRelaxationFunctionData.SlowRelaxationTime, nameof(reducedRelaxationFunctionData.SlowRelaxationTime))
                .AddErrorIfNegativeOrZero(reducedRelaxationFunctionData.RelaxationStiffness, nameof(reducedRelaxationFunctionData.RelaxationStiffness));
        }

        /// <summary>
        /// This method validates if the parameters for Simplified Reduced Relaxation Function is valid and writes the errors in response.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="simplifiedReducedRelaxationFunctionData"></param>
        /// <param name="response"></param>
        public static void Validate<TResponse>(this SimplifiedReducedRelaxationFunctionData simplifiedReducedRelaxationFunctionData, TResponse response)
            where TResponse : OperationResponseBase
        {
            response
                .AddErrorIfNegativeOrZero(simplifiedReducedRelaxationFunctionData.FirstViscoelasticStiffness, nameof(simplifiedReducedRelaxationFunctionData.FirstViscoelasticStiffness));

            foreach (var iteratorValues in simplifiedReducedRelaxationFunctionData.IteratorValues)
            {
                response
                    .AddErrorIfNegativeOrZero(iteratorValues.RelaxationTime, nameof(iteratorValues.RelaxationTime))
                    .AddErrorIfNegativeOrZero(iteratorValues.ViscoelasticStiffness, nameof(iteratorValues.ViscoelasticStiffness));
            }
        }
    }
}
