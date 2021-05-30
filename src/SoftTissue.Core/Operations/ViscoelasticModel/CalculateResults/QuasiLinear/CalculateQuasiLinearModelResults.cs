using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear
{
    /// <summary>
    /// It is responsible to calculate the results to a quasi-linear viscoelastic model.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TRequestData"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TRelaxationFunction"></typeparam>
    public abstract class CalculateQuasiLinearModelResults<TRequest, TRequestData, TInput, TResult, TRelaxationFunction> :
        CalculateResults<TRequest, TRequestData, TInput, TResult>,
        ICalculateQuasiLinearModelResults<TRequest, TRequestData, TInput, TResult, TRelaxationFunction>
        where TRequest : CalculateResultsRequest<TRequestData>
        where TRequestData : CalculateResultsRequestData
        where TInput : QuasiLinearModelInput<TRelaxationFunction>
        where TResult : QuasiLinearModelResult, new()
        where TRelaxationFunction : class
    {
        /// <inheritdoc/>
        protected override string TemplateBasePath => BasePaths.QuasiLinearModel;

        /// <inheritdoc/>
        protected override string SolutionFileHeader => QuasiLinearModelResult.ValueSequence;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        protected CalculateQuasiLinearModelResults(IQuasiLinearModel<TInput, TResult, TRelaxationFunction> viscoelasticModel) : base(viscoelasticModel) { }
    }
}
