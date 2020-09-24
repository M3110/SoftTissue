using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel;
using SoftTissue.Core.Models;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.FungModel;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress
{
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

        public abstract string CreateSolutionFile(QuasiLinearViscoelasticityModelInput input);

        public abstract string CreateInputDataFile(QuasiLinearViscoelasticityModelInput input);

        public virtual string SolutionFileHeader => $"Time;Strain;Reduced Relaxation Function;Elastic Response;Stress";

        public virtual void WriteInputDataInFile(QuasiLinearViscoelasticityModelInput input, StreamWriter streamWriter)
        {
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
                    InitialTime = requestData.InitialTime
                });
            }

            return inputList;
        }

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
                        double strain = this._viscoelasticModel.CalculateStrain(input, time);
                        double reducedRelaxationFunction = this._viscoelasticModel.CalculateReducedRelaxationFunction(input, time);
                        double elasticResponse = this._viscoelasticModel.CalculateElasticResponse(input, time);
                        double stress = this._viscoelasticModel.CalculateStress(input, time);

                        streamWriter.WriteLine($"{time};{strain};{reducedRelaxationFunction};{elasticResponse};{stress}");

                        time += input.TimeStep;
                    }
                }
            }

            return Task.FromResult(response);
        }

        protected override Task<CalculateFungModelStressResponse> ValidateOperation(CalculateFungModelStressRequest request)
        {
            return base.ValidateOperation(request);
        }
    }
}
