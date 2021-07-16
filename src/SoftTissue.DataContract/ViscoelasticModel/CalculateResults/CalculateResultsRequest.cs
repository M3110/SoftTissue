using SoftTissue.DataContract.OperationBase;
using System.Collections.Generic;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults
{
    /// <summary>
    /// It represents the request content to CalculateResults operations.
    /// </summary>
    public abstract class CalculateResultsRequest : OperationRequestBase
    {
        /// <summary>
        /// Time step.
        /// Unit: s (second).
        /// </summary>
        public double TimeStep { get; set; }

        /// <summary>
        /// Final time.
        /// Unit: s (second).
        /// </summary>
        public double FinalTime { get; set; }
    }

    /// <summary>
    /// It represents the request content to CalculateResults operations.
    /// </summary>
    /// <typeparam name="T">The type of request body.</typeparam>
    public class CalculateResultsRequest<T> : CalculateResultsRequest
        where T : CalculateResultsRequestData
    {
        /// <summary>
        /// Request data.
        /// </summary>
        public List<T> DataList { get; set; }
    }
}
