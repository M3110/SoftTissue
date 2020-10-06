using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel;
using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.Core.Operations.Base.CalculateResult;
using SoftTissue.DataContract.OperationBase;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.FungModel;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It is responsible to calculate the stress to a quasi-linear viscoelastic model.
    /// </summary>
    public abstract class CalculateQuasiLinearViscoelasticityStress<TRequest, TInput, TResult> : CalculateResult<TRequest, CalculateFungModelStressResponse, CalculateFungModelStressResponseData, TInput>, ICalculateQuasiLinearViscoelasticityStress<TRequest, TInput>
        where TRequest : OperationRequestBase
        where TInput : QuasiLinearViscoelasticityModelInput, new()
        where TResult : QuasiLinearViscoelasticityModelResult, new()
    {
        private readonly IQuasiLinearViscoelasticityModel<TInput, TResult> _viscoelasticModel;

        /// <summary>
        /// The header to solution file.
        /// This property depends on what is calculated on method <see cref="CalculateAndWriteResults"/>.
        /// </summary>
        public override string SolutionFileHeader => $"Time;Strain;Reduced Relaxation Function;Elastic Response;Stress";

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateQuasiLinearViscoelasticityStress(IQuasiLinearViscoelasticityModel<TInput, TResult> viscoelasticModel)
        {
            this._viscoelasticModel = viscoelasticModel;
        }

        /// <summary>
        /// This method writes the input data into a file.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="streamWriter"></param>
        public virtual void WriteInputDataInFile(TInput input, StreamWriter streamWriter)
        {
            streamWriter.WriteLine($"Analysis type: {input.SoftTissueType}");
            streamWriter.WriteLine($"Initial Time: {input.InitialTime} s");
            streamWriter.WriteLine($"Time Step: {input.TimeStep} s");
            streamWriter.WriteLine($"Final Time: {input.FinalTime} s");
            streamWriter.WriteLine($"Strain Rate: {input.StrainRate} /s");
            streamWriter.WriteLine($"Maximum Strain: {input.MaximumStrain}");
            streamWriter.WriteLine($"Elastic Stress Constant (A): {input.ElasticStressConstant} MPa");
            streamWriter.WriteLine($"Elastic Power Constant (B): {input.ElasticPowerConstant}");
        }

        /// <summary>
        /// This method calculates the results and writes them to a file.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <param name="streamWriter"></param>
        public virtual TResult CalculateAndWriteResults(TInput input, double time, StreamWriter streamWriter)
        {
            // TODO: Tornar assíncrono o cálculo dos resultados
            double strain = this._viscoelasticModel.CalculateStrain(input, time);
            double reducedRelaxationFunction = this._viscoelasticModel.CalculateReducedRelaxationFunction(input, time);
            double elasticResponse = this._viscoelasticModel.CalculateElasticResponse(input, time);
            double stress = this._viscoelasticModel.CalculateStress(input, time);

            streamWriter.WriteLine($"{time};{strain};{reducedRelaxationFunction};{elasticResponse};{stress}");

            return new TResult
            {
                Strain = strain,
                ReducedRelaxationFunction = reducedRelaxationFunction,
                ElasticResponse = elasticResponse,
                Stress = stress
            };
        }

        /// <summary>
        /// This method executes an analysis to calculate the stress for a quasi-linear viscoelasticity model.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<CalculateFungModelStressResponse> ProcessOperation(TRequest request)
        {
            var response = new CalculateFungModelStressResponse { Data = new CalculateFungModelStressResponseData() };
            response.SetSuccessCreated();

            List<TInput> inputList = this.BuildInputList(request);

            Parallel.ForEach(inputList, input =>
            {
                string solutionFileName = this.CreateSolutionFile(input);
                string inputDataFileName = this.CreateInputDataFile(input);

                using (StreamWriter streamWriter = new StreamWriter(inputDataFileName))
                {
                    this.WriteInputDataInFile(input, streamWriter);
                }

                double time = input.InitialTime;
                var previousResult = this._viscoelasticModel.CalculateInitialConditions(input);
                using (StreamWriter streamWriter = new StreamWriter(solutionFileName))
                {
                    streamWriter.WriteLine(this.SolutionFileHeader);

                    while (time <= input.FinalTime)
                    {
                        TResult result = this.CalculateAndWriteResults(input, time, streamWriter);

                        time += input.TimeStep;

                        previousResult = result;
                    }
                }
            });

            return Task.FromResult(response);
        }

        /// <summary>
        /// This method validates the request to be used on process.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<CalculateFungModelStressResponse> ValidateOperation(TRequest request)
        {
            return base.ValidateOperation(request);
        }
    }
}