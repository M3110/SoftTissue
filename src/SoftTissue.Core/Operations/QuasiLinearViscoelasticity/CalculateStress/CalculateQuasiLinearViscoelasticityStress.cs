using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel;
using SoftTissue.Core.Models;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress
{
    public abstract class CalculateQuasiLinearViscoelasticityStress : OperationBase<CalculateQuasiLinearViscoelasticityStressRequest, CalculateQuasiLinearViscoelasticityStressResponse, CalculateQuasiLinearViscoelasticityStressResponseData>, ICalculateQuasiLinearViscoelasticityStress
    {
        private readonly IQuasiLinearViscoelasticityModel _viscoelasticModel;

        public CalculateQuasiLinearViscoelasticityStress(IQuasiLinearViscoelasticityModel viscoelasticModel)
        {
            this._viscoelasticModel = viscoelasticModel;
        }

        private IEnumerable<QuasiLinearViscoelasticityModelInput> BuildInputList(CalculateQuasiLinearViscoelasticityStressRequest request)
        {
            var inputList = new List<QuasiLinearViscoelasticityModelInput>();

            foreach (double strainRate in request.StrainRateList)
            {
                foreach (double maximumStrain in request.MaximumStrainList)
                {
                    foreach (double elasticStressConstant in request.ElasticStressConstantList)
                    {
                        foreach (double elasticPowerConstant in request.ElasticPowerConstantList)
                        {
                            foreach (double relaxationIndex in request.RelaxationIndexList)
                            {
                                foreach (double fastRelaxationTime in request.FastRelaxationTimeList)
                                {
                                    foreach (double slowRelaxationTime in request.SlowRelaxationTimeList)
                                    {
                                        inputList.Add(new QuasiLinearViscoelasticityModelInput
                                        {
                                            StrainRate = strainRate,
                                            MaximumStrain = maximumStrain,
                                            ElasticStressConstant = elasticStressConstant,
                                            ElasticPowerConstant = elasticPowerConstant,
                                            RelaxationIndex = relaxationIndex,
                                            FastRelaxationTime = fastRelaxationTime,
                                            SlowRelaxationTime = slowRelaxationTime,
                                            TimeStep = request.TimeStep
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return inputList;
        }

        protected override Task<CalculateQuasiLinearViscoelasticityStressResponse> ProcessOperation(CalculateQuasiLinearViscoelasticityStressRequest request)
        {
            var response = new CalculateQuasiLinearViscoelasticityStressResponse { Data = new CalculateQuasiLinearViscoelasticityStressResponseData() };

            IEnumerable<QuasiLinearViscoelasticityModelInput> inputList = this.BuildInputList(request);

            int i = 0;
            foreach (var input in inputList)
            {
                string solutionFileName = $"SolutionFile_{i}.csv";
                string inputDataFileName = $"InputDataFile_{i}.csv";
                i++;

                using (StreamWriter streamWriter = new StreamWriter(inputDataFileName))
                {
                    streamWriter.WriteLine($"Initial Time: {request.InitialTime} s");
                    streamWriter.WriteLine($"Time Step: {request.TimeStep} s");
                    streamWriter.WriteLine($"Final Time: {request.FinalTime} s");
                    streamWriter.WriteLine($"Elastic Stress Constant (A): {input.ElasticStressConstant}");
                    streamWriter.WriteLine($"Elastic Power Constant (B): {input.ElasticPowerConstant}");
                    streamWriter.WriteLine($"Relaxation Index (C): {input.RelaxationIndex}");
                    streamWriter.WriteLine($"Fast Relaxation Time (Tau1): {input.FastRelaxationTime} s");
                    streamWriter.WriteLine($"Slow Relaxation Time (Tau2): {input.SlowRelaxationTime} s");
                    streamWriter.WriteLine($"Strain Rate: {input.StrainRate}");
                    streamWriter.WriteLine($"Maximum Strain: {input.MaximumStrain}");
                }

                double time = request.InitialTime;
                using (StreamWriter streamWriter = new StreamWriter(solutionFileName))
                {
                    streamWriter.WriteLine("Time;Strain;Reduced Relaxation Function;Elastic Response;Stress");

                    while (time <= request.FinalTime)
                    {
                        double strain = this._viscoelasticModel.CalculateStrain(input, time);
                        double reducedRelaxationFunction = this._viscoelasticModel.CalculateReducedRelaxationFunction(input, time);
                        double elasticResponse = this._viscoelasticModel.CalculateElasticResponse(input, time);
                        double stress = this._viscoelasticModel.CalculateStress(input, time);

                        streamWriter.WriteLine($"{time};{strain};{reducedRelaxationFunction};{elasticResponse};{stress}");

                        time += request.TimeStep;
                    }
                }
            }

            return Task.FromResult(response);
        }

        protected override Task<CalculateQuasiLinearViscoelasticityStressResponse> ValidateOperation(CalculateQuasiLinearViscoelasticityStressRequest request)
        {
            return base.ValidateOperation(request);
        }
    }
}
