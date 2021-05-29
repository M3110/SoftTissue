using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSentivityAnalysis;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It is responsible to calculate the stress to a quasi-linear viscoelastic model.
    /// </summary>
    public interface ICalculateQuasiLinearViscoelasticityStress<TRequest, TResponse, TResponseData, TInput, TReducedRelaxation, TResult> : ICalculateResult<TRequest, TResponse, TResponseData, TInput>
        where TRequest : CalculateResultsRequest
        where TResponse : CalculateResultsResponse<TResponseData>, new()
        where TResponseData : CalculateResultsResponseData, new()
        where TInput : QuasiLinearViscoelasticityModelInput<TReducedRelaxation>, new()
        where TResult : QuasiLinearViscoelasticityModelResult, new()
    {
        /// <summary>
        /// This method writes the input data into a file.
        /// </summary>
        /// <param name="input"></param>
        void WriteInput(TInput input);
    }
}
