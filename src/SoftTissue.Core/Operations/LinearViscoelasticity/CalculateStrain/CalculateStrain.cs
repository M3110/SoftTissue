using SoftTissue.Core.ConstitutiveEquations;
using SoftTissue.Core.Models;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrain
{
    public abstract class CalculateStrain : OperationBase<CalculateStrainRequest, CalculateStrainResponse, CalculateStrainResponseData>, ICalculateStrain
    {
        private readonly IViscoelasticModel<LinearModelInput> _viscoelasticModel;

        public CalculateStrain(IViscoelasticModel<LinearModelInput> viscoelasticModel)
        {
            this._viscoelasticModel = viscoelasticModel;
        }

        public Task<List<LinearModelInput>> BuildInput(CalculateStrainRequest request)
        {
            var inputs = new List<LinearModelInput>();

            foreach (var stiffness in request.StiffnessList)
            {
                foreach (var viscosity in request.ViscosityList)
                {
                    foreach (var initialStress in request.InitialStressList)
                    {
                        var input = new LinearModelInput
                        {
                            InitialStress = initialStress,
                            Viscosity = viscosity,
                            Stiffness = stiffness,
                            Time = request.InitialTime
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

            List<LinearModelInput> inputs = await this.BuildInput(request).ConfigureAwait(false);

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

                using (StreamWriter streamWriter = new StreamWriter(solutionFileName))
                {
                    streamWriter.WriteLine("Time;Creep Compliance;Strain");

                    while (input.Time - request.FinalTime <= 1e-3)
                    {
                        double creepCompliance = await this._viscoelasticModel.CalculateCreepCompliance(input).ConfigureAwait(false);
                        double strain = await this._viscoelasticModel.CalculateStrain(input).ConfigureAwait(false);

                        streamWriter.WriteLine($"{input.Time};{creepCompliance};{strain}");

                        input.Time += request.TimeStep;
                    }
                }
            }

            return response;
        }
    }
}
