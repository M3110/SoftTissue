using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.Core.Operations.Base.CalculateResult;
using SoftTissue.DataContract.CalculateResult;
using SoftTissue.DataContract.OperationBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It is responsible to calculate the stress to a quasi-linear viscoelastic model.
    /// </summary>
    public abstract class CalculateQuasiLinearViscoelasticityStress<TRequest, TResponse, TResponseData, TInput, TReducedRelaxation, TResult> : CalculateResult<TRequest, TResponse, TResponseData, TInput>, ICalculateQuasiLinearViscoelasticityStress<TRequest, TResponse, TResponseData, TInput, TReducedRelaxation, TResult>
        where TRequest : CalculateResultRequest
        where TResponse : CalculateResultResponse<TResponseData>, new()
        where TResponseData : CalculateResultResponseData, new()
        where TInput : QuasiLinearViscoelasticityModelInput<TReducedRelaxation>, new()
        where TResult : QuasiLinearViscoelasticityModelResult, new()
    {
        private readonly IQuasiLinearViscoelasticityModel<TInput, TResult, TReducedRelaxation> _viscoelasticModel;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateQuasiLinearViscoelasticityStress(IQuasiLinearViscoelasticityModel<TInput, TResult, TReducedRelaxation> viscoelasticModel) : base(viscoelasticModel)
        {
            this._viscoelasticModel = viscoelasticModel;
        }

        /// <summary>
        /// The header to solution file.
        /// </summary>
        protected override string SolutionFileHeader => $"Time,Strain,Reduced Relaxation Function,Elastic Response,Stress,Stress by dG,Stress by Integral Derivative";

        /// <summary>
        /// This method writes the input data into a file.
        /// </summary>
        /// <param name="input"></param>
        public abstract void WriteInput(TInput input);

        /// <summary>
        /// Asynchronously, this method executes an analysis to calculate the stress for a quasi-linear viscoelasticity model.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override async Task<TResponse> ProcessOperationAsync(TRequest request)
        {
            var response = new TResponse { Data = new TResponseData() };
            response.SetSuccessCreated();

            // Step 1 - Builds the input data list based on request.
            List<TInput> inputList = this.BuildInputList(request);

            var tasks = new List<Task>();

            foreach (var input in inputList)
            {
                tasks.Add(Task.Run(async () =>
                {
                    // Step 2 - Saves the model's parameters in a file.
                    this.WriteInput(input);

                    try
                    {
                        // Step 3 - Creates the solution file.
                        // Criar propriedade FileName dentro do Input? Para já ter o nome do arquivo e poder verificar se ele existe ou não. NÃO QUERO SOBRESCREVER ARQUIVOS
                        // Construir lista de input no validate e verificar se os arquivos existem.
                        using (var streamWriter = new StreamWriter(this.CreateSolutionFile(input)))
                        {
                            // Step 4 - Writes the header in the file.
                            streamWriter.WriteLine(this.SolutionFileHeader);

                            var previousResult = new TResult();

                            double time = input.InitialTime;
                            while (time <= input.FinalTime)
                            {
                                // Step 5 - Calculate the results.
                                TResult result = await this._viscoelasticModel.CalculateResultsAsync(input, time).ConfigureAwait(false);

                                streamWriter.WriteLine($"{time},{result}");

                                // It increases the time step when the stress is converging to its asymptote.
                                //if (Math.Abs((result.Stress - previousResult.Stress) / previousResult.Stress) < Constants.RelativePrecision)
                                //    time += 10 * input.TimeStep;
                                //else
                                //    time += input.TimeStep;
                                time += input.TimeStep;

                                // Step 6  - Saves the current result.
                                previousResult = result;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        response.SetInternalServerError(OperationErrorCode.InternalServerError, $"Error trying to calculate and write the solutions in file. {ex.Message}.");
                    }
                }));
            }

            // TODO: preencher o fileUri e fileName do response.

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return response;
        }

        // TODO: adicionar validação para não permitir mais de 10 modelos no request.
        // requestData.Cont <= 10
        // Criar arquivo de configurações contendo a quantidade máxima de request.Data para cada modelo e com as pastas de cada modelo e operação.
    }
}