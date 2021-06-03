using SoftTissue.DataContract.OperationBase;
using System;
using Range = SoftTissue.DataContract.Models.Range;

namespace SoftTissue.Core.ExtensionMethods
{
    /// <summary>
    /// It contains the extension methods to OperationResponseBase.
    /// </summary>
    public static class OperationResponseBaseExtensions
    {
        /// <summary>
        /// This method adds error if the value is negative or equal to zero.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="response"></param>
        /// <param name="parameter"></param>
        /// <param name="parameterName"></param>
        /// <param name="aditionalMessage"></param>
        /// <param name="operationErrorCode"></param>
        /// <returns></returns>
        public static TResponse AddErrorIfNegativeOrZero<TResponse>(
            this TResponse response,
            double parameter,
            string parameterName,
            string aditionalMessage = null,
            string operationErrorCode = OperationErrorCode.RequestValidationError)
            where TResponse : OperationResponseBase
        {
            string errorMessage = $"The '{parameterName}' cannot be negative or equal to zero.";

            if (aditionalMessage != null)
                errorMessage = $"{aditionalMessage} {errorMessage}";

            return response.AddErrorIf(() => parameter <= 0, errorMessage, operationErrorCode);
        }

        /// <summary>
        /// This method adds error if the value is equal to zero.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="response"></param>
        /// <param name="parameter"></param>
        /// <param name="parameterName"></param>
        /// <param name="aditionalMessage"></param>
        /// <param name="operationErrorCode"></param>
        /// <returns></returns>
        public static TResponse AddErrorIfZero<TResponse>(
            this TResponse response,
            double parameter,
            string parameterName,
            string aditionalMessage = null,
            string operationErrorCode = OperationErrorCode.RequestValidationError)
            where TResponse : OperationResponseBase
        {
            string errorMessage = $"The '{parameterName}' cannot be equal to zero.";

            if (aditionalMessage != null)
                errorMessage = $"{aditionalMessage} {errorMessage}";

            return response.AddErrorIf(() => parameter == 0, errorMessage, operationErrorCode);
        }

        /// <summary>
        /// This method adds error by a condition expression.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="expression"></param>
        /// <param name="errorMessage"></param>
        /// <param name="operationErrorCode"></param>
        public static TResponse AddErrorIf<TResponse>(
            this TResponse response,
            Func<bool> expression,
            string errorMessage,
            string operationErrorCode = OperationErrorCode.RequestValidationError)
            where TResponse : OperationResponseBase
        {
            if (expression())
            {
                response.AddError(operationErrorCode, errorMessage);
            }

            return response;
        }

        /// <summary>
        /// This method adds error if <see cref="Range"/> is invalid.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="response"></param>
        /// <param name="range"></param>
        /// <param name="name"></param>
        public static TResponse AddErrorIfInvalidRange<TResponse>(this TResponse response, Range range, string name)
            where TResponse : OperationResponseBase
        {
            response.AddErrorIfNegativeOrZero(range.InitialPoint, $"{name} initial point");

            if (range.Step.HasValue)
                response.AddErrorIfNegativeOrZero(range.Step.Value, $"{name} step");

            if (range.FinalPoint.HasValue)
            {
                response
                    .AddErrorIfNegativeOrZero(range.FinalPoint.Value, $"{name} final point")
                    .AddErrorIf(() => range.InitialPoint > range.FinalPoint.Value, "The initial point must be less than final point.");
            }

            return response;
        }
    }
}
