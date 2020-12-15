using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel
{
    /// <summary>
    /// It is responsible to calculate the stress to Fung model.
    /// </summary>
    public abstract class CalculateFungModelStress<TRequest, TResponse, TResponseData, TInput, TReducedRelaxation, TResult> : CalculateQuasiLinearViscoelasticityStress<TRequest, TResponse, TResponseData, TInput, TReducedRelaxation, TResult>, ICalculateFungModelStress<TRequest, TResponse, TResponseData, TInput, TReducedRelaxation, TResult>
        where TRequest : OperationRequestBase
        where TResponse : OperationResponseBase<TResponseData>, new()
        where TResponseData : OperationResponseData, new()
        where TInput : QuasiLinearViscoelasticityModelInput<TReducedRelaxation>, new()
        where TResult : QuasiLinearViscoelasticityModelResult, new()
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateFungModelStress(IQuasiLinearViscoelasticityModel<TInput, TResult, TReducedRelaxation> viscoelasticModel) : base(viscoelasticModel) { }

        /// <summary>
        /// The base path to files.
        /// </summary>
        protected override string TemplateBasePath => Constants.FungModelBasePath;
    }
}
