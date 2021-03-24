﻿using SoftTissue.DataContract.OperationBase;
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
        /// Asynchronously, this method processes the operation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected abstract Task<TResponse> ProcessOperationAsync(TRequest request);

        /// <summary>
        /// Asynchronously, this method validates the request sent to operation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual Task<TResponse> ValidateOperationAsync(TRequest request)
        {
            var response = new TResponse();
            response.SetSuccessCreated();

            if (request == null)
            {
                response.SetBadRequestError(OperationErrorCode.RequestValidationError, "Request cannot be null.");
            }

            return Task.FromResult(response);
        }

        /// <summary>
        /// The main method of all operations.
        /// Asynchronously, this method orchestrates the operations.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TResponse> ProcessAsync(TRequest request)
        {
            // Sets the current culture like invariant.
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            var response = new TResponse();

            try
            {
                response = await this.ValidateOperationAsync(request).ConfigureAwait(false);
                if (response.Success == false)
                {
                    response.SetBadRequestError();

                    return response;
                }

                response = await this.ProcessOperationAsync(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                response.SetInternalServerError(OperationErrorCode.InternalServerError, $"{ex.Message}");
            }

            return response;
        }
    }
}
