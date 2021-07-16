namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime
{
    /// <summary>
    /// It represents the request content to CalculateResultsConsiderRampTime operation.
    /// </summary>
    /// <typeparam name="TRequestData"></typeparam>
    /// <typeparam name="TRelaxationFunction"></typeparam>
    public abstract class CalculateQuasiLinearModelResultsConsiderRampTimeRequest<TRequestData, TRelaxationFunction> : CalculateResultsRequest<TRequestData> 
        where TRequestData : CalculateQuasiLinearModelResultsConsiderRampTimeRequestData<TRelaxationFunction>
        where TRelaxationFunction : class
    { }
}
