using SoftTissue.Core.ConstitutiveEquations.LinearModel;
using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.Core.Operations.Base.CalculateResult;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStress;
using SoftTissue.DataContract.OperationBase;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It is responsible to calculate the stress to a linear viscoelastic model.
    /// </summary>
    public abstract class CalculateLinearViscosityStress<TRequest, TInput> : CalculateResult<TRequest, CalculateStressResponse, CalculateStressResponseData, TInput>, ICalculateLinearViscosityStress<TRequest, TInput>
        where TRequest : OperationRequestBase
        where TInput : LinearViscoelasticityModelInput, new()
    {
        private readonly ILinearViscoelasticityModel<TInput> _viscoelasticModel;

        /// <summary>
        /// The header to solution file.
        /// </summary>
        public override string SolutionFileHeader => "Time;Relaxation Function;Stress";

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateLinearViscosityStress(ILinearViscoelasticityModel<TInput> viscoelasticModel)
        {
            this._viscoelasticModel = viscoelasticModel;
        }

        /// <summary>
        /// This method executes an analysis to calculate the stress for a linear viscoelasticity model.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<CalculateStressResponse> ProcessOperation(TRequest request)
        {
            var response = new CalculateStressResponse { Data = new CalculateStressResponseData() };
            response.SetSuccessCreated();

            List<TInput> inputs = this.BuildInputList(request);

            foreach (var input in inputs)
            {
                string solutionFileName = this.CreateSolutionFile(input);
                string inputDataFileName = this.CreateInputDataFile(input);

                using (StreamWriter streamWriter = new StreamWriter(inputDataFileName))
                {
                    streamWriter.WriteLine($"Initial Time: {input.InitialTime} s");
                    streamWriter.WriteLine($"Time Step: {input.TimeStep} s");
                    streamWriter.WriteLine($"Final Time: {input.FinalTime} s");
                    streamWriter.WriteLine($"Initial Strain: {input.InitialStrain}");
                    streamWriter.WriteLine($"Stiffness: {input.Stiffness} Pa");
                    streamWriter.WriteLine($"Viscosity: {input.Viscosity} Ns/m²");
                    streamWriter.WriteLine($"Relaxation Time: {input.RelaxationTime} s");
                }

                double time = input.InitialTime;
                using (StreamWriter streamWriter = new StreamWriter(solutionFileName))
                {
                    streamWriter.WriteLine(this.SolutionFileHeader);

                    while (time - input.FinalTime <= 1e-3)
                    {
                        double relaxationFunction = this._viscoelasticModel.CalculateReducedRelaxationFunction(input, time);
                        double stress = this._viscoelasticModel.CalculateStress(input, time);

                        streamWriter.WriteLine($"{time};{relaxationFunction};{stress}");

                        time += input.TimeStep;
                    }
                }
            }

            return Task.FromResult(response);
        }
    }
}
