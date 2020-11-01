using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using System.IO;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel
{
    /// <summary>
    /// It is responsible to calculate the stress to Fung model.
    /// </summary>
    public class CalculateFungModelStress : CalculateQuasiLinearViscoelasticityStress<FungModelInput, FungModelResult>, ICalculateFungModelStress
    {
        private readonly IFungModel _viscoelasticModel;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateFungModelStress(IFungModel viscoelasticModel) : base(viscoelasticModel)
        {
            this._viscoelasticModel = viscoelasticModel;
        }

        /// <summary>
        /// The base path to files.
        /// </summary>
        protected override string TemplateBasePath => Constants.FungModelBasePath;

        /// <summary>
        /// The header to solution file.
        /// </summary>
        protected override string SolutionFileHeader => "Time;Strain;Reduced Relaxation Function;Elastic Response;Stress with dSigma;Stress with dG;Stress with integral derivative";

        /// <summary>
        /// This method writes the input data into a file.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="streamWriter"></param>
        public override void WriteInput(FungModelInput input, StreamWriter streamWriter)
        {
            base.WriteInput(input, streamWriter);

            if (input.UseSimplifiedReducedRelaxationFunction == true)
            {
                streamWriter.WriteLine($"Variables E: {string.Join(";", input.SimplifiedReducedRelaxationFunctionInput.VariableEList)}");
                streamWriter.WriteLine($"Relaxation Times: {string.Join(";", input.SimplifiedReducedRelaxationFunctionInput.RelaxationTimeList)}");
            }
            else
            {
                streamWriter.WriteLine($"Relaxation Index (C): {input.ReducedRelaxationFunctionInput.RelaxationIndex}");
                streamWriter.WriteLine($"Fast Relaxation Time (Tau1): {input.ReducedRelaxationFunctionInput.FastRelaxationTime} s");
                streamWriter.WriteLine($"Slow Relaxation Time (Tau2): {input.ReducedRelaxationFunctionInput.SlowRelaxationTime} s");
            }
        }

        /// <summary>
        /// This method calculates the results and writes them to a file.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <param name="streamWriter"></param>
        public override FungModelResult CalculateAndWriteResults(FungModelInput input, double time, StreamWriter streamWriter)
        {
            double strain = this._viscoelasticModel.CalculateStrain(input, time);

            double reducedRelaxationFunction;
            if (input.UseSimplifiedReducedRelaxationFunction == true) reducedRelaxationFunction = this._viscoelasticModel.CalculateReducedRelaxationFunctionSimplified(input, time);
            else reducedRelaxationFunction = this._viscoelasticModel.CalculateReducedRelaxationFunction(input, time);

            double elasticResponse = this._viscoelasticModel.CalculateElasticResponse(input, time);

            double stressWithElasticResponseDerivative = this._viscoelasticModel.CalculateStress(input, time);

            double stressWithReducedRelaxationFunctionDerivative = this._viscoelasticModel.CalculateStressByReducedRelaxationFunctionDerivative(input, time);

            double stressWithIntegrationDerivative = this._viscoelasticModel.CalculateStressByIntegrationDerivative(input, time);

            streamWriter.WriteLine($"{time};{strain};{reducedRelaxationFunction};{elasticResponse};{stressWithElasticResponseDerivative};{stressWithReducedRelaxationFunctionDerivative};{stressWithIntegrationDerivative}");

            return new FungModelResult
            {
                Strain = strain,
                ReducedRelaxationFunction = reducedRelaxationFunction,
                ElasticResponse = elasticResponse,
                Stress = stressWithElasticResponseDerivative,
                StressWithReducedRelaxationFunctionDerivative = stressWithReducedRelaxationFunctionDerivative,
                StressWithIntegralDerivative = stressWithIntegrationDerivative
            };
        }
    }
}
