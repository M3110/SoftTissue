using SoftTissue.Core.ConstitutiveEquations;
using SoftTissue.Core.Models;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStress;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStress
{
    public abstract class CalculateLinearViscosityStress : OperationBase<CalculateStressRequest, CalculateStressResponse, CalculateStressResponseData>, ICalculateLinearViscosityStress
    {
        private readonly IViscoelasticModel<LinearViscoelasticityModelInput> _viscoelasticModel;

        public CalculateLinearViscosityStress(IViscoelasticModel<LinearViscoelasticityModelInput> viscoelasticModel)
        {
            this._viscoelasticModel = viscoelasticModel;
        }

        public Task<List<LinearViscoelasticityModelInput>> BuildInput(CalculateStressRequest request)
        {
            var inputs = new List<LinearViscoelasticityModelInput>();

            foreach (var stiffness in request.StiffnessList)
            {
                foreach (var viscosity in request.ViscosityList)
                {
                    foreach (var initialStrain in request.InitialStrainList)
                    {
                        var input = new LinearViscoelasticityModelInput
                        {
                            InitialStrain = initialStrain,
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

        protected override async Task<CalculateStressResponse> ProcessOperation(CalculateStressRequest request)
        {
            var response = new CalculateStressResponse { Data = new CalculateStressResponseData() };

            List<LinearViscoelasticityModelInput> inputs = await BuildInput(request).ConfigureAwait(false);

            int i = 0;
            foreach (var input in inputs)
            {
                string solutionFileName = $"SolutionFile_{i}";
                string inputDataFileName = $"InputDataFile_{i}";
                i++;

                using (StreamWriter streamWriter = new StreamWriter(inputDataFileName))
                {
                    streamWriter.WriteLine($"Initial Time: {request.InitialTime} s");
                    streamWriter.WriteLine($"Time Step: {request.TimeStep} s");
                    streamWriter.WriteLine($"Final Time: {request.FinalTime} s");
                    streamWriter.WriteLine($"Initial Strain: {input.InitialStrain} %");
                    streamWriter.WriteLine($"Stiffness: {input.Stiffness} Pa");
                    streamWriter.WriteLine($"Viscosity: {input.Viscosity} Ns/m²");
                    streamWriter.WriteLine($"Relaxation Time: {input.RelaxationTime} s");
                }

                using (StreamWriter streamWriter = new StreamWriter(solutionFileName))
                {
                    streamWriter.WriteLine("Time;Relaxation Function;Stress");

                    while (input.Time - request.FinalTime <= 1e-3)
                    {
                        double relaxationFunction = await this._viscoelasticModel.CalculateReducedRelaxationFunction(input).ConfigureAwait(false);
                        double stress = await this._viscoelasticModel.CalculateStress(input).ConfigureAwait(false);

                        streamWriter.WriteLine($"{input.Time};{relaxationFunction};{stress}");

                        input.Time += request.TimeStep;
                    }
                }
            }

            return response;
        }
    }
}
