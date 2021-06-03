using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.Core.Operations.Base.CalculateResultSensitivityAnalysis;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSensitivityAnalysis.Linear;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResultsSensitivityAnalysis.Linear
{
    /// <summary>
    /// It is responsible to calculate the results with sensitivity analysis for a linear viscoelastic model.
    /// </summary>
    public interface ICalculateLinearModelResultsSensitivityAnalysis<TRequest, TInput, TResult> : ICalculateResultsSensitivityAnalysis<TRequest, TInput, TResult>
        where TRequest : CalculateLinearModelResultsSensitivityAnalysisRequest
        where TInput : LinearModelInput
        where TResult : LinearModelResult, new()
    { }
}