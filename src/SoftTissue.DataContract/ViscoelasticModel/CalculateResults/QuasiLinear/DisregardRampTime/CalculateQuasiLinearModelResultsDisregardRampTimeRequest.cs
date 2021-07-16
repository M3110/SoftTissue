namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime
{
    /// <summary>
    /// It represents the request content to CalculateResultsDisregardRampTime operation.
    /// </summary>
    /// <typeparam name="TRequestData"></typeparam>
    /// <typeparam name="TRelaxationFunction"></typeparam>
    public abstract class CalculateQuasiLinearModelResultsDisregardRampTimeRequest<TRequestData, TRelaxationFunction> : CalculateResultsRequest<TRequestData>
        where TRequestData : CalculateQuasiLinearModelResultsDisregardRampTimeRequestData<TRelaxationFunction>
        where TRelaxationFunction : class
    { }
}
