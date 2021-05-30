using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSensitivityAnalysis;

namespace SoftTissue.Core.Operations.Base.CalculateResultSensitivityAnalysis
{
    /// <summary>
    /// It contains methods and parameters shared between operations to calculate a result.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface ICalculateResultsSensitivityAnalysis<TRequest, TInput, TResult> : IOperationBase<TRequest, CalculateResultsSensitivityAnalysisResponse, CalculateResultsSensitivityAnalysisResponseData>
        where TRequest : CalculateResultsSensitivityAnalysisRequest
        where TInput : ViscoelasticModelInput
        where TResult : ViscoelasticModelResult, new()
    { }
}