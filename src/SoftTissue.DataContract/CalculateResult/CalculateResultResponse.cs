using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.CalculateResult
{
    /// <summary>
    /// It represents the response content for CalculateResult operations.
    /// </summary>
    /// <typeparam name="TResponseData"></typeparam>
    public abstract class CalculateResultResponse<TResponseData> : OperationResponseBase<TResponseData>
        where TResponseData : CalculateResultResponseData, new()
    { }
}
