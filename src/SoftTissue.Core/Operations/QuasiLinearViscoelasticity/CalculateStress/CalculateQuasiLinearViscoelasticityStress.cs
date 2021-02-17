using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.Core.Operations.Base.CalculateResult;
using SoftTissue.DataContract.CalculateResult;
using SoftTissue.DataContract.OperationBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
        /// This property depends on what is calculated on method <see cref="CalculateAndWriteResults"/>.
        /// </summary>
        protected override string SolutionFileHeader => $"Time;Strain;Reduced Relaxation Function;Elastic Response;Stress;Stress by dG;Stress by Integral Derivative";

        /// <summary>
        /// This method writes the input data into a file.
        /// </summary>
        /// <param name="input"></param>
        public abstract void WriteInput(TInput input);

        /// <summary>
        /// This method calculates the results and writes them to a file.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <param name="streamWriter"></param>
        public virtual async Task<TResult> CalculateAndWriteResults(TInput input, double time, StreamWriter streamWriter)
        {
            var tasks = new List<Task>();

            double strain = 0;
            tasks.Add(Task.Run(() =>
            {
                strain = this._viscoelasticModel.CalculateStrain(input, time);
            }));

            double reducedRelaxationFunction = 0;
            tasks.Add(Task.Run(() =>
            {
                reducedRelaxationFunction = this._viscoelasticModel.CalculateReducedRelaxationFunction(input, time);
            }));

            double elasticResponse = 0;
            tasks.Add(Task.Run(() =>
            {
                elasticResponse = this._viscoelasticModel.CalculateElasticResponse(input, time);
            }));

            double stress = 0;
            tasks.Add(Task.Run(() =>
            {
                stress = this._viscoelasticModel.CalculateStress(input, time);
            }));

            double stressByReducedRelaxationFunctionDerivative = 0;
            tasks.Add(Task.Run(() =>
            {
                stressByReducedRelaxationFunctionDerivative = this._viscoelasticModel.CalculateStressByReducedRelaxationFunctionDerivative(input, time);
            }));

            double stressByIntegralDerivative = 0;
            tasks.Add(Task.Run(() =>
            {
                stressByIntegralDerivative = this._viscoelasticModel.CalculateStressByIntegralDerivative(input, time);
            }));

            await Task.WhenAll(tasks);

            streamWriter.WriteLine($"{time};{strain};{reducedRelaxationFunction};{elasticResponse};{stress};{stressByReducedRelaxationFunctionDerivative};{stressByIntegralDerivative}");

            return new TResult
            {
                Strain = strain,
                ReducedRelaxationFunction = reducedRelaxationFunction,
                ElasticResponse = elasticResponse,
                Stress = stress,
                StressByReducedRelaxationFunctionDerivative = stressByReducedRelaxationFunctionDerivative,
                StressByIntegralDerivative = stressByIntegralDerivative
            };
        }

        /// <summary>
        /// This method executes an analysis to calculate the stress for a quasi-linear viscoelasticity model.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override async Task<TResponse> ProcessOperation(TRequest request)
        {
            var response = new TResponse { Data = new TResponseData() };
            response.SetSuccessCreated();

            List<TInput> inputList = this.BuildInputList(request);

            var tasks = new List<Task>();

            foreach (var input in inputList)
            {
                tasks.Add(Task.Run(async () =>
                {
                    this.WriteInput(input);

                    double time = input.InitialTime;

                    TResult previousResult = this._viscoelasticModel.CalculateInitialConditions(input);

                    try
                    {
                        using (StreamWriter streamWriter = new StreamWriter(this.CreateSolutionFile(input)))
                        {
                            streamWriter.WriteLine(this.SolutionFileHeader);

                            while (time <= input.FinalTime)
                            {
                                input.RelaxationNumber = this.CalculateRelaxationNumber(input, time);

                                TResult result = await this.CalculateAndWriteResults(input, time, streamWriter).ConfigureAwait(false);

                                // It increases the time step when the stress is converging to its asymptote.
                                if (Math.Abs((result.Stress - previousResult.Stress) / previousResult.Stress) < Constants.RelativePrecision)
                                    time += 10 * input.TimeStep;
                                else
                                    time += input.TimeStep;

                                previousResult = result;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        response.AddError(OperationErrorCode.InternalServerError, $"Error trying to calculate and write the solutions in file. {ex.Message}.", HttpStatusCode.InternalServerError);
                    }
                }));

                await Task.WhenAll(tasks).ConfigureAwait(false);
            }

            return response;
        }

        protected int CalculateRelaxationNumber(TInput input, double time)
        {
            throw new NotImplementedException();
        }
    }
}