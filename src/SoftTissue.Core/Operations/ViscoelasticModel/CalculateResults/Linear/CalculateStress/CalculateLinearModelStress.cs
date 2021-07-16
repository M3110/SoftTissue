using SoftTissue.Core.ConstitutiveEquations.LinearModel;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.CalculateStress;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.Linear.CalculateStress
{
    /// <summary>
    /// It is responsible to calculate the stress to a linear viscoelastic model.
    /// </summary>
    public abstract class CalculateLinearModelStress<TRequest, TRequestData, TInput, TResult> : CalculateResults<TRequest, TRequestData, TInput, TResult>, ICalculateLinearModelStress<TRequest, TRequestData, TInput, TResult>
        where TRequest : CalculateLinearModelStressRequest<TRequestData>
        where TRequestData : CalculateLinearModelStressRequestData
        where TInput : LinearModelInput
        where TResult : LinearModelResult, new()
    {
        /// <inheritdoc/>
        protected override string TemplateBasePath => Path.Combine(BasePaths.LinearModel, "Stress");

        /// <inheritdoc/>
        protected override string SolutionFileHeader => "Time;Relaxation Function;Stress";
        
        private readonly ILinearModel<TInput, TResult> _viscoelasticModel;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateLinearModelStress(ILinearModel<TInput, TResult> viscoelasticModel) : base(viscoelasticModel)
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
            double relaxationFunction = this._viscoelasticModel.CalculateRelaxationFunction(input, time);

            double stress = await this._viscoelasticModel.CalculateStressAsync(input, time).ConfigureAwait(false);

            await streamWriter.WriteLineAsync($"{time},{relaxationFunction},{stress}").ConfigureAwait(false);
        }
    }
}
