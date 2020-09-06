namespace SoftTissue.DataContract
{
    /// <summary>
    /// It contains the operation error codes that the application can return.
    /// </summary>
    public static class OperationErrorCode
    {
        /// <summary>
        /// This error means that the request do not passed in the validation.
        /// </summary>
        public static string RequestValidationError { get => "400"; }

        /// <summary>
        /// This error means that the client is not authorized to access the endpoint or the resource.
        /// </summary>
        public static string UnauthorizedError { get => "401"; }

        /// <summary>
        /// This error means that some error ocurred while processing the request.
        /// </summary>
        public static string InternalServerError { get => "500"; }

        /// <summary>
        /// This error means that some resource or endpoint was not implemented.
        /// </summary>
        public static string NotImplementedError { get => "501"; }
    }
}
