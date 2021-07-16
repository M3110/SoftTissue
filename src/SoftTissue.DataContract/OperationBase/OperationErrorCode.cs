using System.Net;

namespace SoftTissue.DataContract.OperationBase
{
    /// <summary>
    /// It represents the operation error codes that the application can return.
    /// </summary>
    public class OperationErrorCode
    {
        /// <summary>
        /// This error means that the request do not passed in the validation.
        /// </summary>
        public const string RequestValidationError = "400";

        /// <summary>
        /// This error means that the client is not authorized to access the endpoint or the resource.
        /// </summary>
        public const string UnauthorizedError = "401";

        /// <summary>
        /// This error means that some error ocurred while processing the request.
        /// </summary>
        public const string InternalServerError = "500";

        /// <summary>
        /// This error means that some resource or endpoint was not implemented.
        /// </summary>
        public const string NotImplementedError = "501";
    }
}
