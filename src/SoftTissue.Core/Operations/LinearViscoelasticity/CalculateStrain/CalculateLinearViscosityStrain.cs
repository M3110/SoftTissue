using SoftTissue.Core.ConstitutiveEquations.LinearModel;
using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain;
using SoftTissue.DataContract.OperationBase;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrain
{
    /// <summary>
    /// It is responsible to calculate the strain to a linear viscoelastic model.
    /// </summary>
    public abstract class CalculateLinearViscosityStrain<TRequest, TInput> : OperationBase<TRequest, CalculateStrainResponse, CalculateStrainResponseData>, ICalculateLinearViscosityStrain<TRequest, TInput>
        where TRequest : OperationRequestBase
        where TInput : LinearViscoelasticityModelInput, new()
    {
        private readonly ILinearViscoelasticityModel<TInput> _viscoelasticModel;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateLinearViscosityStrain(ILinearViscoelasticityModel<TInput> viscoelasticModel)
        {
            this._viscoelasticModel = viscoelasticModel;
        }

        /// <summary>
        /// This method creates the path to save the solution on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract string CreateSolutionFile(TInput input);

        /// <summary>
        /// This method creates the path to save the input data on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract string CreateInputDataFile(TInput input);

        /// <summary>
        /// This method builds a list with the inputs based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public abstract List<TInput> BuildInputList(TRequest request);

        /// <summary>
        /// This method executes an analysis to calculate the strain for a linear viscoelasticity model.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<CalculateStrainResponse> ProcessOperation(TRequest request)
        {
            var response = new CalculateStrainResponse { Data = new CalculateStrainResponseData() };
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
                    streamWriter.WriteLine($"Initial Stress: {input.InitialStress} Pa");
                    streamWriter.WriteLine($"Stiffness: {input.Stiffness} Pa");
                    streamWriter.WriteLine($"Viscosity: {input.Viscosity} Ns/m²");
                    streamWriter.WriteLine($"Relaxation Time: {input.RelaxationTime} s");
                }

                double time = input.InitialTime;
                using (StreamWriter streamWriter = new StreamWriter(solutionFileName))
                {
                    streamWriter.WriteLine("Time;Creep Compliance;Strain");

                    while (time - input.FinalTime <= 1e-3)
                    {
                        double creepCompliance = this._viscoelasticModel.CalculateCreepCompliance(input, time);
                        double strain = this._viscoelasticModel.CalculateStrain(input, time);

                        streamWriter.WriteLine($"{time};{creepCompliance};{strain}");

                        time += input.TimeStep;
                    }
                }
            }

            return Task.FromResult(response);
        }
    }
}
