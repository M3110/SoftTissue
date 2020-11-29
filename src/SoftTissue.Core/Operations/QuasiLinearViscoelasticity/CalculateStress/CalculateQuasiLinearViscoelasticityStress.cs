using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.Core.Operations.Base.CalculateResult;
using SoftTissue.DataContract.OperationBase;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Request;
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
    public abstract class CalculateQuasiLinearViscoelasticityStress<TInput, TResult> : CalculateResult<CalculateQuasiLinearViscoelasticityStressRequest, CalculateQuasiLinearViscoelasticityStressResponse, CalculateQuasiLinearViscoelasticityStressResponseData, TInput>, ICalculateQuasiLinearViscoelasticityStress<TInput, TResult>
        where TInput : QuasiLinearViscoelasticityModelInput, new()
        where TResult : QuasiLinearViscoelasticityModelResult, new()
    {
        private readonly IQuasiLinearViscoelasticityModel<TInput, TResult> _viscoelasticModel;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateQuasiLinearViscoelasticityStress(IQuasiLinearViscoelasticityModel<TInput, TResult> viscoelasticModel) : base(viscoelasticModel)
        {
            this._viscoelasticModel = viscoelasticModel;
        }

        /// <summary>
        /// The header to solution file.
        /// This property depends on what is calculated on method <see cref="CalculateAndWriteResults"/>.
        /// </summary>
        protected override string SolutionFileHeader => $"Time;Strain;Reduced Relaxation Function;Elastic Response;Stress";

        /// <summary>
        /// This method builds a list with the inputs based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override List<TInput> BuildInputList(CalculateQuasiLinearViscoelasticityStressRequest request)
        {
            var inputList = new List<TInput>();

            foreach (var requestData in request.RequestDataList)
            {
                var input = new TInput
                {
                    ElasticStressConstant = requestData.ElasticStressConstant,
                    ElasticPowerConstant = requestData.ElasticPowerConstant,
                    MaximumStrain = requestData.MaximumStrain,
                    StrainRate = requestData.StrainRate,
                    FinalTime = requestData.FinalTime ?? request.FinalTime,
                    TimeStep = requestData.TimeStep ?? request.TimeStep,
                    InitialTime = requestData.InitialTime ?? request.InitialTime,
                    SoftTissueType = requestData.SoftTissueType,
                    UseSimplifiedReducedRelaxationFunction = requestData.UseSimplifiedReducedRelaxationFunction,
                };

                if (requestData.UseSimplifiedReducedRelaxationFunction == true)
                {
                    input.SimplifiedReducedRelaxationFunctionInput = requestData.SimplifiedReducedRelaxationFunctionData;
                    input.ReducedRelaxationFunctionInput = null;
                }
                else
                {
                    input.SimplifiedReducedRelaxationFunctionInput = null;
                    input.ReducedRelaxationFunctionInput = requestData.ReducedRelaxationFunctionData;
                }

                inputList.Add(input);
            }

            return inputList;
        }

        /// <summary>
        /// This method writes the input data into a file.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="streamWriter"></param>
        public virtual void WriteInput(TInput input, StreamWriter streamWriter)
        {
            streamWriter.WriteLine("Parameter;Value;Unity");
            streamWriter.WriteLine($"Analysis type;{input.SoftTissueType};");
            streamWriter.WriteLine($"Initial Time;{input.InitialTime};s");
            streamWriter.WriteLine($"Time Step;{input.TimeStep};s");
            streamWriter.WriteLine($"Final Time;{input.FinalTime};s");
            streamWriter.WriteLine($"Strain Rate;{input.StrainRate};/s");
            streamWriter.WriteLine($"Maximum Strain;{input.MaximumStrain};");
            streamWriter.WriteLine($"Elastic Stress Constant (A);{input.ElasticStressConstant};MPa");
            streamWriter.WriteLine($"Elastic Power Constant (B);{input.ElasticPowerConstant};");
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

            double reducedRelaxationFunction;
            if (input.UseSimplifiedReducedRelaxationFunction == true) reducedRelaxationFunction = this._viscoelasticModel.CalculateReducedRelaxationFunctionSimplified(input, time);
            else reducedRelaxationFunction = this._viscoelasticModel.CalculateReducedRelaxationFunction(input, time);

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
        protected override Task<CalculateQuasiLinearViscoelasticityStressResponse> ProcessOperation(CalculateQuasiLinearViscoelasticityStressRequest request)
        {
            var response = new CalculateQuasiLinearViscoelasticityStressResponse { Data = new CalculateQuasiLinearViscoelasticityStressResponseData() };
            response.SetSuccessCreated();

            List<TInput> inputList = this.BuildInputList(request);

            Parallel.ForEach(inputList, input =>
            {
                using (StreamWriter streamWriter = new StreamWriter(this.CreateInputFile(input)))
                {
                    this.WriteInput(input, streamWriter);
                }

                double time = input.InitialTime;
                var previousResult = this._viscoelasticModel.CalculateInitialConditions(input);

                try
                {
                    using (StreamWriter streamWriter = new StreamWriter(this.CreateSolutionFile(input)))
                    {
                        streamWriter.WriteLine(this.SolutionFileHeader);

                        while (time <= input.FinalTime)
                        {
                            TResult result = this.CalculateAndWriteResults(input, time, streamWriter);

                            // It increases the time step when the stress is converging to its asymptote.
                            if (Math.Abs((result.Stress - previousResult.Stress) / previousResult.Stress) < Constants.RelativePrecision) time += 10 * input.TimeStep;
                            else time += input.TimeStep;

                            previousResult = result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    response.AddError(OperationErrorCode.InternalServerError, $"Error trying to calculate and write the solutions in file. {ex.Message}.", HttpStatusCode.InternalServerError);
                    response.SetInternalServerError();
                }
            });

            return Task.FromResult(response);
        }
    }
}