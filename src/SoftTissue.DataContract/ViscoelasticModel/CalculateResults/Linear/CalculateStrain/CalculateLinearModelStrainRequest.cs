namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.CalculateStrain
{
    /// <summary>
    /// It represents the request content to CalculateLinearModelStrain operation.
    /// </summary>
    public abstract class CalculateLinearModelStrainRequest<TRequestData> : CalculateResultsRequest<TRequestData>
        where TRequestData : CalculateLinearModelStrainRequestData
    { }
}
