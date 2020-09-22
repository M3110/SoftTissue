using SoftTissue.Core.ConstitutiveEquations.LinearModel;
using SoftTissue.Core.Models;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrain
{
    public abstract class CalculateStrain : OperationBase<CalculateStrainRequest, CalculateStrainResponse, CalculateStrainResponseData>, ICalculateStrain
    {
        private readonly ILinearViscoelasticityModel _viscoelasticModel;

        public CalculateStrain(ILinearViscoelasticityModel viscoelasticModel)
        {
            this._viscoelasticModel = viscoelasticModel;
        }

        public Task<List<LinearViscoelasticityModelInput>> BuildInput(CalculateStrainRequest request)
        {
            var inputs = new List<LinearViscoelasticityModelInput>();

            foreach (var stiffness in request.StiffnessList)
            {
                foreach (var viscosity in request.ViscosityList)
                {
                    foreach (var initialStress in request.InitialStressList)
                    {
                        var input = new LinearViscoelasticityModelInput
                        {
                            InitialStress = initialStress,
                            Viscosity = viscosity,
                            Stiffness = stiffness
                        };

                        inputs.Add(input);
                    }
                }
            }

            return Task.FromResult(inputs);
        }

        protected override async Task<CalculateStrainResponse> ProcessOperation(CalculateStrainRequest request)
        {
            var response = new CalculateStrainResponse { Data = new CalculateStrainResponseData() };

            List<LinearViscoelasticityModelInput> inputs = await this.BuildInput(request).ConfigureAwait(false);

            int i = 0;
            foreach (var input in inputs)
            {
                string solutionFileName = $"SolutionFile_{i}";
                string inputDataFileName = $"InputDataFile_{i}";
                i++;

                using(StreamWriter streamWriter = new StreamWriter(inputDataFileName))
                {
                    streamWriter.WriteLine($"Initial Time: {request.InitialTime} s");
                    streamWriter.WriteLine($"Time Step: {request.TimeStep} s");
                    streamWriter.WriteLine($"Final Time: {request.FinalTime} s");
                    streamWriter.WriteLine($"Initial Stress: {input.InitialStress} Pa");
                    streamWriter.WriteLine($"Stiffness: {input.Stiffness} Pa");
                    streamWriter.WriteLine($"Viscosity: {input.Viscosity} Ns/m²");
                    streamWriter.WriteLine($"Relaxation Time: {input.RelaxationTime} s");
                }

                double time = request.InitialTime;
                using (StreamWriter streamWriter = new StreamWriter(solutionFileName))
                {
                    streamWriter.WriteLine("Time;Creep Compliance;Strain");

                    while (time - request.FinalTime <= 1e-3)
                    {
                        double creepCompliance = this._viscoelasticModel.CalculateCreepCompliance(input, time);
                        double strain = this._viscoelasticModel.CalculateStrain(input, time);

                        streamWriter.WriteLine($"{time};{creepCompliance};{strain}");

                        time += request.TimeStep;
                    }
                }
            }

            return response;
        }
    }
}
