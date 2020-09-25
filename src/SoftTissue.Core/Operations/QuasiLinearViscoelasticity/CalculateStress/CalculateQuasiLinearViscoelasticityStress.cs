using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel;
using SoftTissue.Core.Models;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.FungModel;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It is responsible to calculate the stress to a quasi-linear viscoelastic model.
    /// </summary>
    public abstract class CalculateQuasiLinearViscoelasticityStress : OperationBase<CalculateFungModelStressRequest, CalculateFungModelStressResponse, CalculateFungModelStressResponseData>, ICalculateQuasiLinearViscoelasticityStress
    {
        private readonly IQuasiLinearViscoelasticityModel _viscoelasticModel;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateQuasiLinearViscoelasticityStress(IQuasiLinearViscoelasticityModel viscoelasticModel)
        {
            this._viscoelasticModel = viscoelasticModel;
        }

        /// <summary>
        /// This method creates the path to save the solution on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract string CreateSolutionFile(QuasiLinearViscoelasticityModelInput input);

        /// <summary>
        /// This method creates the path to save the input data on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract string CreateInputDataFile(QuasiLinearViscoelasticityModelInput input);

        /// <summary>
        /// The header to solution file.
        /// </summary>
        public virtual string SolutionFileHeader => $"Time;Strain;Reduced Relaxation Function;Elastic Response;Stress";

        /// <summary>
        /// This method writes the input data into a file.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="streamWriter"></param>
        public virtual void WriteInputDataInFile(QuasiLinearViscoelasticityModelInput input, StreamWriter streamWriter)
        {
            streamWriter.WriteLine($"Analysis type: {input.AnalysisType}");
            streamWriter.WriteLine($"Initial Time: {input.InitialTime} s");
            streamWriter.WriteLine($"Time Step: {input.TimeStep} s"); 
            streamWriter.WriteLine($"Final Time: {input.FinalTime} s");
            streamWriter.WriteLine($"Elastic Stress Constant (A): {input.ElasticStressConstant} MPa");
            streamWriter.WriteLine($"Elastic Power Constant (B): {input.ElasticPowerConstant}");
            streamWriter.WriteLine($"Relaxation Index (C): {input.RelaxationIndex}");
            streamWriter.WriteLine($"Fast Relaxation Time (Tau1): {input.FastRelaxationTime} s");
            streamWriter.WriteLine($"Slow Relaxation Time (Tau2): {input.SlowRelaxationTime} s");
            streamWriter.WriteLine($"Strain Rate: {input.StrainRate} /s");
            streamWriter.WriteLine($"Maximum Strain: {input.MaximumStrain}");
        }

        /// <summary>
        /// This method builds a list with the inputs based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<QuasiLinearViscoelasticityModelInput> BuildInputList(CalculateFungModelStressRequest request)
        {
            var inputList = new List<QuasiLinearViscoelasticityModelInput>();

            foreach (var requestData in request.RequestDataList)
            {
                inputList.Add(new QuasiLinearViscoelasticityModelInput
                {
                    ElasticStressConstant = requestData.ElasticStressConstant,
                    ElasticPowerConstant = requestData.ElasticPowerConstant,
                    RelaxationIndex = requestData.RelaxationIndex,
                    FastRelaxationTime = requestData.FastRelaxationTime,
                    SlowRelaxationTime = requestData.SlowRelaxationTime,
                    MaximumStrain = requestData.MaximumStrain,
                    StrainRate = requestData.StrainRate,
                    FinalTime = requestData.FinalTime,
                    TimeStep = requestData.TimeStep,
                    InitialTime = requestData.InitialTime,
                    AnalysisType = requestData.AnalysisType
                });
            }

            return inputList;
        }

        /// <summary>
        /// This method calculates the results and writes them to a file.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <param name="streamWriter"></param>
        public virtual void CalculateAndWriteResults(QuasiLinearViscoelasticityModelInput input, double time, StreamWriter streamWriter)
        {
            double strain = this._viscoelasticModel.CalculateStrain(input, time);
            double reducedRelaxationFunction = this._viscoelasticModel.CalculateReducedRelaxationFunction(input, time);
            double elasticResponse = this._viscoelasticModel.CalculateElasticResponse(input, time);
            double stress = this._viscoelasticModel.CalculateStress(input, time);

            streamWriter.WriteLine($"{time};{strain};{reducedRelaxationFunction};{elasticResponse};{stress}");
        }

        /// <summary>
        /// This method executes an analysis to calculate the stress for a quasi-linear viscoelasticity model.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<CalculateFungModelStressResponse> ProcessOperation(CalculateFungModelStressRequest request)
        {
            var response = new CalculateFungModelStressResponse { Data = new CalculateFungModelStressResponseData() };
            response.SetSuccessCreated();

            IEnumerable<QuasiLinearViscoelasticityModelInput> inputList = this.BuildInputList(request);

            foreach(var input in inputList)
            {
                string solutionFileName = this.CreateSolutionFile(input);
                string inputDataFileName = this.CreateInputDataFile(input);

                using (StreamWriter streamWriter = new StreamWriter(inputDataFileName))
                {
                    this.WriteInputDataInFile(input, streamWriter);
                }

                double time = input.InitialTime;
                using (StreamWriter streamWriter = new StreamWriter(solutionFileName))
                {
                    streamWriter.WriteLine(this.SolutionFileHeader);

                    while (time <= input.FinalTime)
                    {
                        this.CalculateAndWriteResults(input, time, streamWriter);

                        time += input.TimeStep;
                    }
                }
            }

            return Task.FromResult(response);
        }

        /// <summary>
        /// This method validates the request to be used on process.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<CalculateFungModelStressResponse> ValidateOperation(CalculateFungModelStressRequest request)
        {
            return base.ValidateOperation(request);
        }
    }
}