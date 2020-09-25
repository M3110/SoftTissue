﻿using SoftTissue.Core.ConstitutiveEquations.LinearModel;
using SoftTissue.Core.Models;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStress;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It is responsible to calculate the stress to a linear viscoelastic model.
    /// </summary>
    public abstract class CalculateLinearViscosityStress : OperationBase<CalculateStressRequest, CalculateStressResponse, CalculateStressResponseData>, ICalculateLinearViscosityStress
    {
        private readonly ILinearViscoelasticityModel _viscoelasticModel;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateLinearViscosityStress(ILinearViscoelasticityModel viscoelasticModel)
        {
            this._viscoelasticModel = viscoelasticModel;
        }

        /// <summary>
        /// This method creates the path to save the solution on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract string CreateSolutionFile(LinearViscoelasticityModelInput input);

        /// <summary>
        /// This method creates the path to save the input data on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract string CreateInputDataFile(LinearViscoelasticityModelInput input);

        /// <summary>
        /// This method builds a list with the inputs based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<List<LinearViscoelasticityModelInput>> BuildInputList(CalculateStressRequest request)
        {
            var inputs = new List<LinearViscoelasticityModelInput>();

            foreach(var requestData in request.RequestDataList)
            {
                inputs.Add(new LinearViscoelasticityModelInput
                {
                    FinalTime = requestData.FinalTime,
                    TimeStep = requestData.TimeStep,
                    InitialTime = requestData.InitialTime,
                    InitialStrain = requestData.InitialStrain,
                    Stiffness = requestData.Stiffness,
                    Viscosity = requestData.Viscosity
                });
            }

            return Task.FromResult(inputs);
        }

        /// <summary>
        /// This method executes an analysis to calculate the stress for a linear viscoelasticity model.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override async Task<CalculateStressResponse> ProcessOperation(CalculateStressRequest request)
        {
            var response = new CalculateStressResponse { Data = new CalculateStressResponseData() };
            response.SetSuccessCreated();

            List<LinearViscoelasticityModelInput> inputs = await BuildInputList(request).ConfigureAwait(false);

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

                double time = request.InitialTime;
                using (StreamWriter streamWriter = new StreamWriter(solutionFileName))
                {
                    streamWriter.WriteLine("Time;Relaxation Function;Stress");

                    while (time - request.FinalTime <= 1e-3)
                    {
                        double relaxationFunction = this._viscoelasticModel.CalculateReducedRelaxationFunction(input, time);
                        double stress = this._viscoelasticModel.CalculateStress(input, time);

                        streamWriter.WriteLine($"{time};{relaxationFunction};{stress}");

                        time += request.TimeStep;
                    }
                }
            }

            return response;
        }
    }
}
