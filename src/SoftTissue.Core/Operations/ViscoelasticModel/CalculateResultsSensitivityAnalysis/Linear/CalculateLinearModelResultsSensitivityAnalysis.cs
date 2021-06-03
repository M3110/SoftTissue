using SoftTissue.Core.ConstitutiveEquations.LinearModel;
using SoftTissue.Core.ExtensionMethods;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.Core.Operations.Base.CalculateResultSensitivityAnalysis;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSensitivityAnalysis;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSensitivityAnalysis.Linear;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResultsSensitivityAnalysis.Linear
{
    /// <summary>
    /// It is responsible to calculate the results with sensitivity analysis for a linear viscoelastic model.
    /// </summary>
    public abstract class CalculateLinearModelResultsSensitivityAnalysis<TRequest, TInput, TResult> : CalculateResultsSensitivityAnalysis<TRequest, TInput, TResult>, ICalculateLinearModelResultsSensitivityAnalysis<TRequest, TInput, TResult>
        where TRequest : CalculateLinearModelResultsSensitivityAnalysisRequest
        where TInput : LinearModelInput
        where TResult : LinearModelResult, new()
    {
        /// <inheritdoc/>
        protected override string TemplateBasePath => BasePaths.LinearModel;

        /// <inheritdoc/>
        protected override string SolutionFileHeader => LinearModelResult.ValueSequence;

        private readonly ILinearModel<TInput, TResult> _viscoelasticModel;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateLinearModelResultsSensitivityAnalysis(ILinearModel<TInput, TResult> viscoelasticModel) : base(viscoelasticModel)
        {
            this._viscoelasticModel = viscoelasticModel;
        }

        /// <summary>
        /// This method builds the tasks that have to be executed.
        /// The aim of this method is run asynchronously the sensitivity analysis.
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <returns></returns>
        protected override List<Task> BuildTasks(List<TInput> inputList, double timeStep, double finalTime)
        {
            List<Task> tasks = base.BuildTasks(inputList, timeStep, finalTime);

            tasks.Add(Task.Run(() => this.RunSensitivityAnalysis(inputList, timeStep, finalTime, "Creep Compliance", this._viscoelasticModel.CalculateCreepCompliance)));
            tasks.Add(Task.Run(() => this.RunSensitivityAnalysis(inputList, timeStep, finalTime, "Relaxation Function", this._viscoelasticModel.CalculateRelaxationFunction)));

            return tasks;
        }

        /// <summary>
        /// Asynchronously, this method validates the request sent to operation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override async Task<CalculateResultsSensitivityAnalysisResponse> ValidateOperationAsync(TRequest request)
        {
            var response = await base.ValidateOperationAsync(request).ConfigureAwait(false);
            if (response.Success == false)
            {
                return response;
            }

            response
                .AddErrorIfInvalidRange(request.InitialStrainRange, nameof(request.InitialStrainRange))
                .AddErrorIfInvalidRange(request.InitialStressRange, nameof(request.InitialStressRange));

            return response;
        }
    }
}
