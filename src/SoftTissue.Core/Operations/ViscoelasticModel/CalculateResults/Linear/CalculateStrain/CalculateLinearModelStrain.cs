using SoftTissue.Core.ConstitutiveEquations.LinearModel;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.CalculateStrain;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.Linear.CalculateStrain
{
    /// <summary>
    /// It is responsible to calculate the strain to a linear viscoelastic model.
    /// </summary>
    public abstract class CalculateLinearModelStrain<TRequest, TRequestData, TInput, TResult> : CalculateResults<TRequest, TRequestData, TInput, TResult>, ICalculateLinearModelStrain<TRequest, TRequestData, TInput, TResult>
        where TRequest : CalculateLinearModelStrainRequest<TRequestData>
        where TRequestData : CalculateLinearModelStrainRequestData
        where TInput : LinearModelInput
        where TResult : LinearModelResult, new()
    {
        /// <inheritdoc/>
        protected override string TemplateBasePath => Path.Combine(BasePaths.LinearModel, "Strain");

        /// <inheritdoc/>
        protected override string SolutionFileHeader => "Time;Creep Compliance;Strain";

        private readonly ILinearModel<TInput, TResult> _viscoelasticModel;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateLinearModelStrain(ILinearModel<TInput, TResult> viscoelasticModel) : base(viscoelasticModel)
        {
            this._viscoelasticModel = viscoelasticModel;
        }

        /// <summary>
        /// Asynchronously, this method calculates the result and writes its into file.
        /// </summary>
        /// <param name="streamWriter"></param>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        protected override async Task CalculateAndWriteResultAsync(StreamWriter streamWriter, TInput input, double time)
        {
            double creepCompliance = this._viscoelasticModel.CalculateCreepCompliance(input, time);

            double strain = this._viscoelasticModel.CalculateStrain(input, time);

            await streamWriter.WriteLineAsync($"{time},{creepCompliance},{strain}").ConfigureAwait(false);
        }
    }
}
