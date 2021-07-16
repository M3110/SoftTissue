using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSensitivityAnalysis;

namespace SoftTissue.Core.Operations.Base.CalculateResultSensitivityAnalysis
{
    /// <summary>
    /// It is responsible to calculate the results with sensitivity analysis for a generic viscoelastic model.
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