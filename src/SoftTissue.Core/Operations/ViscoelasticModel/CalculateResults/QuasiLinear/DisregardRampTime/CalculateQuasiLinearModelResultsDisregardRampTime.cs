using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel;
using SoftTissue.Core.ExtensionMethods;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime
{
    /// <summary>
    /// It is responsible to calculate the results disregarding ramp time for a quasi-linear viscoelastic model.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TRequestData"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TRelaxationFunction"></typeparam>
    public abstract class CalculateQuasiLinearModelResultsDisregardRampTime<TRequest, TRequestData, TInput, TResult, TRelaxationFunction> :
        CalculateQuasiLinearModelResults<TRequest, TRequestData, TInput, TResult, TRelaxationFunction>,
        ICalculateQuasiLinearModelResultsDisregardRampTime<TRequest, TRequestData, TInput, TResult, TRelaxationFunction>
        where TRequest : CalculateQuasiLinearModelResultsDisregardRampTimeRequest<TRequestData, TRelaxationFunction>
        where TRequestData : CalculateQuasiLinearModelResultsDisregardRampTimeRequestData<TRelaxationFunction>
        where TInput : QuasiLinearModelInput<TRelaxationFunction>, new()
        where TResult : QuasiLinearModelResult, new()
        where TRelaxationFunction : class
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        protected CalculateQuasiLinearModelResultsDisregardRampTime(IQuasiLinearModel<TInput, TResult, TRelaxationFunction> viscoelasticModel) : base(viscoelasticModel) { }

        /// <summary>
        /// This method builds a list with <see cref="TInput"/> based on <see cref="TRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override List<TInput> BuildInputList(TRequest request)
        {
            var inputs = new List<TInput>();

            foreach (var requestData in request.DataList)
            {
                inputs.Add(new TInput
                {
                    // Relaxation parameters
                    ViscoelasticConsideration = ViscoelasticConsideration.DisregardRampTime,
                    // Strain parameters
                    MaximumStrain = requestData.Strain,
                    // Elastic parameters
                    InitialStress = requestData.InitialStress,
                    // Reduced Relaxation Function parameters
                    ReducedRelaxationFunctionInput = requestData.ReducedRelaxationFunctionData,
                    // General parameters
                    TimeStep = requestData.TimeStep ?? request.TimeStep,
                    FinalTime = requestData.FinalTime ?? request.FinalTime,
                    SoftTissueType = requestData.SoftTissueType,
                });
            }

            return inputs;
        }

        /// <summary>
        /// Asynchronously, this method validates the request sent to operation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override async Task<CalculateResultsResponse> ValidateOperationAsync(TRequest request)
        {
            CalculateResultsResponse response = await base.ValidateOperationAsync(request).ConfigureAwait(false);
            if (response.Success == false)
            {
                return response;
            }

            foreach (var requestData in request.DataList)
            {
                string aditionalErrorMessage = $"Error on request Data index {request.DataList.IndexOf(requestData)}.";

                response
                    .AddErrorIfNegativeOrZero(requestData.Strain, nameof(requestData.Strain), aditionalErrorMessage)
                    .AddErrorIfNegativeOrZero(requestData.InitialStress, nameof(requestData.InitialStress), aditionalErrorMessage);

                this.ValidateReducedRelaxationFunctionData(requestData.ReducedRelaxationFunctionData, response);
            }

            return response;
        }

        /// <summary>
        /// This method validates the parameters for Reduced Relaxation Function.
        /// </summary>
        /// <param name="reducedRelaxationFunctionData"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        protected abstract void ValidateReducedRelaxationFunctionData(TRelaxationFunction reducedRelaxationFunctionData, CalculateResultsResponse response);
    }
}
