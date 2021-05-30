using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel;
using SoftTissue.Core.ExtensionMethods;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime
{
    /// <summary>
    /// It is responsible to calculate the results considering ramp time to a quasi-linear viscoelastic model.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TRequestData"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TRelaxationFunction"></typeparam>
    public abstract class CalculateQuasiLinearModelResultsConsiderRampTime<TRequest, TRequestData, TInput, TResult, TRelaxationFunction> :
        CalculateQuasiLinearModelResults<TRequest, TRequestData, TInput, TResult, TRelaxationFunction>,
        ICalculateQuasiLinearModelResultsConsiderRampTime<TRequest, TRequestData, TInput, TResult, TRelaxationFunction>
        where TRequest : CalculateQuasiLinearModelResultsConsiderRampTimeRequest<TRequestData, TRelaxationFunction>
        where TRequestData : CalculateQuasiLinearModelResultsConsiderRampTimeRequestData<TRelaxationFunction>
        where TInput : QuasiLinearModelInput<TRelaxationFunction>, new()
        where TResult : QuasiLinearModelResult, new()
        where TRelaxationFunction : class
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        protected CalculateQuasiLinearModelResultsConsiderRampTime(IQuasiLinearModel<TInput, TResult, TRelaxationFunction> viscoelasticModel) : base(viscoelasticModel) { }

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
                    ViscoelasticConsideration = requestData.ViscoelasticConsideration,
                    NumerOfRelaxations = requestData.NumerOfRelaxations,
                    // Strain parameters
                    StrainRate = requestData.StrainRate,
                    StrainDecreaseRate = requestData.StrainDecreaseRate,
                    MaximumStrain = requestData.MaximumStrain,
                    MinimumStrain = requestData.MinimumStrain,
                    TimeWithConstantMaximumStrain = requestData.TimeWithConstantMaximumStrain,
                    TimeWithConstantMinimumStrain = requestData.TimeWithConstantMinimumStrain,
                    // Elastic parameters
                    ElasticStressConstant = requestData.ElasticStressConstant,
                    ElasticPowerConstant = requestData.ElasticPowerConstant,
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
                    .AddErrorIf(() => requestData.ViscoelasticConsideration == ViscoelasticConsideration.DisregardRampTime, $"{aditionalErrorMessage} The '{nameof(requestData.ViscoelasticConsideration)}' cannot be {requestData.ViscoelasticConsideration} to that operation because the ramp time must be considered.")
                    .AddErrorIfNegativeOrZero(requestData.NumerOfRelaxations, nameof(requestData.NumerOfRelaxations), aditionalErrorMessage)
                    .AddErrorIfNegativeOrZero(requestData.StrainRate, nameof(requestData.StrainRate), aditionalErrorMessage)
                    .AddErrorIfNegativeOrZero(requestData.StrainDecreaseRate, nameof(requestData.StrainDecreaseRate), aditionalErrorMessage)
                    .AddErrorIfNegativeOrZero(requestData.MaximumStrain, nameof(requestData.MaximumStrain), aditionalErrorMessage)
                    .AddErrorIfNegativeOrZero(requestData.MinimumStrain, nameof(requestData.MinimumStrain), aditionalErrorMessage)
                    .AddErrorIfNegativeOrZero(requestData.TimeWithConstantMaximumStrain, nameof(requestData.TimeWithConstantMaximumStrain), aditionalErrorMessage)
                    .AddErrorIfNegativeOrZero(requestData.TimeWithConstantMinimumStrain, nameof(requestData.TimeWithConstantMinimumStrain), aditionalErrorMessage)
                    .AddErrorIfNegativeOrZero(requestData.ElasticStressConstant, nameof(requestData.ElasticStressConstant), aditionalErrorMessage)
                    .AddErrorIfNegativeOrZero(requestData.ElasticPowerConstant, nameof(requestData.ElasticPowerConstant), aditionalErrorMessage);

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
