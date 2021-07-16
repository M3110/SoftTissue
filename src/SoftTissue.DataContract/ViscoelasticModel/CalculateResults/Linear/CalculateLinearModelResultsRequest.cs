namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear
{
    /// <summary>
    /// It represents the request content to CalculateLinearModelResults operation for .
    /// </summary>
    public abstract class CalculateLinearModelResultsRequest<TRequestData> : CalculateResultsRequest<TRequestData>
        where TRequestData : CalculateLinearModelResultsRequestData
    { }
}
