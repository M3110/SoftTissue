using Newtonsoft.Json;

namespace SoftTissue.DataContract.OperationBase
{
    /// <summary>
    /// It represents the request content to all operations.
    /// </summary>
    public abstract class OperationRequestBase { }

    /// <summary>
    /// It represents the essencial request content to operations.
    /// </summary>
    /// <typeparam name="T">The type of request data.</typeparam>
    public abstract class OperationRequestBase<T> : OperationRequestBase
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="data"></param>
        [JsonConstructor]
        protected OperationRequestBase(T data)
        {
            this.Data = data;
        }

        /// <summary>
        /// Request data.
        /// </summary>
        public T Data { get; protected set; }
    }
}
