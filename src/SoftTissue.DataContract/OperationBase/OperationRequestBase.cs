namespace SoftTissue.DataContract.OperationBase
{
    /// <summary>
    /// It represents the request content to all operations.
    /// </summary>
    public class OperationRequestBase { }

    /// <summary>
    /// It represents the essencial request content to operations.
    /// </summary>
    /// <typeparam name="T">The type of request data.</typeparam>
    public class OperationRequestBase<T> : OperationRequestBase
    {
        /// <summary>
        /// Request data.
        /// </summary>
        public T Data { get; set; }
    }
}
