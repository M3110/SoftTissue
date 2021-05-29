namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.CalculateStress
{
    /// <summary>
    /// It represents the request content to CalculateLinearModelStress operation.
    /// </summary>
    public abstract class CalculateLinearModelStressRequest<TRequestData> : CalculateResultsRequest<TRequestData>
        where TRequestData : CalculateLinearModelStressRequestData
    { }
}
