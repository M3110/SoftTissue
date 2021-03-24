using SoftTissue.Core.ConstitutiveEquations.LinearModel;
using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.Core.Operations.Base.CalculateResult;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStress;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It is responsible to calculate the stress to a linear viscoelastic model.
    /// </summary>
    public abstract class CalculateLinearViscosityStress<TInput> : CalculateResult<CalculateStressRequest, CalculateStressResponse, CalculateStressResponseData, TInput>, ICalculateLinearViscosityStress<TInput>
        where TInput : LinearViscoelasticityModelInput, new()
    {
        private readonly ILinearViscoelasticityModel<TInput> _viscoelasticModel;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateLinearViscosityStress(ILinearViscoelasticityModel<TInput> viscoelasticModel) : base(viscoelasticModel)
        {
            this._viscoelasticModel = viscoelasticModel;
        }

        /// <summary>
        /// The header to solution file.
        /// </summary>
        protected override string SolutionFileHeader => "Time;Relaxation Function;Stress";

        /// <summary>
        /// This method builds a list with the inputs based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override List<TInput> BuildInputList(CalculateStressRequest request)
        {
            var inputList = new List<TInput>();

            foreach (var requestData in request.Data)
            {
                inputList.Add(new TInput
                {
                    FinalTime = requestData.FinalTime ?? request.FinalTime,
                    TimeStep = requestData.TimeStep ?? request.TimeStep,
                    InitialStrain = requestData.InitialStrain,
                    Stiffness = requestData.Stiffness,
                    Viscosity = requestData.Viscosity,
                    SoftTissueType = requestData.SoftTissueType
                });
            }

            return inputList;
        }

        /// <summary>
        /// Asynchronously, this method executes an analysis to calculate the stress for a linear viscoelasticity model.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<CalculateStressResponse> ProcessOperationAsync(CalculateStressRequest request)
        {
            var response = new CalculateStressResponse { Data = new CalculateStressResponseData() };
            response.SetSuccessCreated();

            List<TInput> inputs = this.BuildInputList(request);

            foreach (var input in inputs)
            {
                using (StreamWriter streamWriter = new StreamWriter(this.CreateInputFile(input)))
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
                using (StreamWriter streamWriter = new StreamWriter(this.CreateSolutionFile(input)))
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
