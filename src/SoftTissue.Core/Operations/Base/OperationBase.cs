using SoftTissue.DataContract.OperationBase;
using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.Base
{
    /// <summary>
    /// It represents the base for all operations in the application.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <typeparam name="TResponseData"></typeparam>
    public abstract class OperationBase<TRequest, TResponse, TResponseData> : IOperationBase<TRequest, TResponse, TResponseData>
        where TRequest : OperationRequestBase
        where TResponse : OperationResponseBase<TResponseData>, new()
        where TResponseData : OperationResponseData, new()
    {
        /// <summary>
        /// This method processes the operation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected abstract Task<TResponse> ProcessOperation(TRequest request);

        /// <summary>
        /// This method validates the request sent to operation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual Task<TResponse> ValidateOperation(TRequest request)
        {
            var response = new TResponse();
            response.SetSuccessCreated();

            if (request == null)
            {
                response.AddError(OperationErrorCode.RequestValidationError, "Request cannot be null");
                response.SetBadRequestError();
            }

            return Task.FromResult(response);
        }

        /// <summary>
        /// The main method of all operations.
        /// This method orchestrates the operations.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TResponse> Process(TRequest request)
        {
            // Sets the current culture like invariant.
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            var response = new TResponse();

            try
            {
                response = await ValidateOperation(request).ConfigureAwait(false);
                if (response.Success == false)
                {
                    response.SetBadRequestError();

                    return response;
                }

                response = await ProcessOperation(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                response.AddError(OperationErrorCode.InternalServerError, $"{ex.Message}", HttpStatusCode.InternalServerError);
            }

            return response;
        }
    }
}
