using SoftTissue.Core.ConstitutiveEquations.LinearModel;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.Linear
{
    /// <summary>
    /// It is responsible to calculate the results to a linear viscoelastic model.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TRequestData"></typeparam>
    public abstract class CalculateLinearModelResults<TRequest, TRequestData, TInput, TResult> : CalculateResults<TRequest, TRequestData, TInput, TResult>, ICalculateLinearModelResults<TRequest, TRequestData, TInput, TResult>
        where TRequest : CalculateLinearModelResultsRequest<TRequestData>
        where TRequestData : CalculateLinearModelResultsRequestData
        where TInput : LinearModelInput
        where TResult : LinearModelResult, new()
    {
        /// <inheritdoc/>
        protected override string TemplateBasePath => BasePaths.LinearModel;

        /// <inheritdoc/>
        protected override string SolutionFileHeader => LinearModelResult.ValueSequence;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        protected CalculateLinearModelResults(ILinearModel<TInput, TResult> viscoelasticModel) : base(viscoelasticModel) { }
    }
}
