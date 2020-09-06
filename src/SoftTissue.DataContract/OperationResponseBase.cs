using System.Collections.Generic;
using System.Net;

namespace SoftTissue.DataContract
{
    /// <summary>
    /// It contains the content of response for all operations.
    /// </summary>
    /// <typeparam name="TResponseData"></typeparam>
    public class OperationResponseBase<TResponseData>
        where TResponseData : OperationResponseData
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        public OperationResponseBase()
        {
            Errors = new List<OperationError>();
        }

        /// <summary>
        /// The success status of operation.
        /// </summary>
        public bool Success { get; protected set; }

        /// <summary>
        /// The HTTP status code.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; }

        /// <summary>
        /// The list of errors.
        /// </summary>
        public List<OperationError> Errors { get; protected set; }

        /// <summary>
        /// It represents the 'data' content of all operation response.
        /// </summary>
        public TResponseData Data { get; set; }


        /// <summary>
        /// This method adds error on list of errors.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="httpStatusCode"></param>
        public void AddError(string code, string message, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            Errors.Add(new OperationError(code, message));

            HttpStatusCode = httpStatusCode;
            Success = false;
        }

        /// <summary>
        /// Set success to true. The HttpStatusCode will be set to 201 (Created).
        /// </summary>
        public void SetSuccessCreated()
        {
            HttpStatusCode = HttpStatusCode.Created;
            Success = true;
        }

        /// <summary>
        /// Set success to false. The HttpStatusCode will be set to 400 (BadRequest).
        /// </summary>
        public void SetBadRequestError()
        {
            HttpStatusCode = HttpStatusCode.BadRequest;
            Success = false;
        }

        /// <summary>
        /// Set success to false. The HttpStatusCode will be set to 401 (Unauthorized).
        /// </summary>
        public void SetUnauthorizedError()
        {
            HttpStatusCode = HttpStatusCode.Unauthorized;
            Success = false;
        }

        /// <summary>
        /// Set Success to false. The HttpStatusCode will be set to 500 (InternalServerError).
        /// </summary>
        public void SetInternalServerError()
        {
            HttpStatusCode = HttpStatusCode.InternalServerError;
            Success = false;
        }

        /// <summary>
        /// Set Success to false. The HttpStatusCode will be set to 501 (NotImplemented).
        /// </summary>
        public void SetNotImplementedError()
        {
            HttpStatusCode = HttpStatusCode.NotImplemented;
            Success = false;
        }
    }
}
