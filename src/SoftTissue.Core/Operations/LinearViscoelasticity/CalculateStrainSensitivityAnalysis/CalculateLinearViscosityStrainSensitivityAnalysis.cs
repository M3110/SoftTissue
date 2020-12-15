using SoftTissue.Core.ConstitutiveEquations.LinearModel;
using SoftTissue.Core.ExtensionMethods;
using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.Core.Operations.Base.CalculateResultSensitivityAnalysis;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStrainSensitivityAnalysis;
using SoftTissue.DataContract.OperationBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrainSensitivityAnalysis
{
    /// <summary>
    /// It is responsible to calculate the strain to a linear viscoelastic model.
    /// </summary>
    public abstract class CalculateLinearViscosityStrainSensitivityAnalysis<TInput> : CalculateResultSensitivityAnalysis<CalculateStrainSensitivityAnalysisRequest, CalculateStrainResponse, CalculateStrainResponseData, TInput>, ICalculateLinearViscosityStrainSensitivityAnalysis<TInput>
        where TInput : LinearViscoelasticityModelInput, new()
    {
        private readonly ILinearViscoelasticityModel<TInput> _viscoelasticModel;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateLinearViscosityStrainSensitivityAnalysis(ILinearViscoelasticityModel<TInput> viscoelasticModel) : base(viscoelasticModel)
        {
            this._viscoelasticModel = viscoelasticModel;
        }

        /// <summary>
        /// This method builds a list with the inputs based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override List<TInput> BuildInputList(CalculateStrainSensitivityAnalysisRequest request)
        {
            var inputList = new List<TInput>();

            foreach (var initialStress in request.InitialStressList.ToEnumerable())
            {
                foreach (var stiffness in request.StiffnessList.ToEnumerable())
                {
                    foreach (var viscosity in request.ViscosityList.ToEnumerable())
                    {
                        inputList.Add(new TInput
                        {
                            FinalTime = request.FinalTime,
                            TimeStep = request.TimeStep,
                            InitialTime = request.InitialTime,
                            InitialStress = initialStress,
                            Stiffness = stiffness,
                            Viscosity = viscosity
                        });
                    }
                }
            }

            return inputList;
        }

        /// <summary>
        /// This method writes the input data into a file.
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="streamWriter"></param>
        public virtual void WriteInputData(List<TInput> inputList, StreamWriter streamWriter)
        {
            List<double> initialTimeList = new List<double>();
            List<double> timeStepList = new List<double>();
            List<double> finalTimeList = new List<double>();
            List<double> relaxationTimeList = new List<double>();
            List<double> stiffnessList = new List<double>();
            List<double> initialStressList = new List<double>();
            List<double> viscosityList = new List<double>();

            StringBuilder header = new StringBuilder("Parameter;");

            int index = 1;

            foreach (var input in inputList)
            {
                initialTimeList.Add(input.InitialTime);
                timeStepList.Add(input.TimeStep);
                finalTimeList.Add(input.FinalTime);
                relaxationTimeList.Add(input.RelaxationTime);
                stiffnessList.Add(input.Stiffness);
                initialStressList.Add(input.InitialStress);
                viscosityList.Add(input.Viscosity);

                header.Append($"Input {index};");
                index++;
            }

            header.Append("Unit");

            streamWriter.WriteLine(header);
            streamWriter.WriteLine($"Initial Time;{string.Join(';', initialTimeList)};s");
            streamWriter.WriteLine($"Time Step;{string.Join(';', timeStepList)};s");
            streamWriter.WriteLine($"Final Time;{string.Join(';', finalTimeList)};s");
            streamWriter.WriteLine($"Relaxation Time;{string.Join(';', relaxationTimeList)};/s");
            streamWriter.WriteLine($"Stiffness;{string.Join(';', stiffnessList)};");
            streamWriter.WriteLine($"Initial Stress;{string.Join(';', initialStressList)};MPa");
            streamWriter.WriteLine($"Viscosity;{string.Join(';', viscosityList)};");
        }

        /// <summary>
        /// This method calculates the results and writes them in a file.
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="initialTime"></param>
        /// <param name="finalTime"></param>
        /// <param name="timeStep"></param>
        public override void CalculateAndWriteResults(List<TInput> inputList, double initialTime, double finalTime, double timeStep)
        {
            using (StreamWriter creepComplianceStreamWriter = new StreamWriter(CreateSolutionFile(functionName: "Creep Compliance")))
            using (StreamWriter strainStreamWriter = new StreamWriter(CreateSolutionFile(functionName: "Strain")))
            {
                StringBuilder fileHeader = CreteFileHeader(inputList);

                creepComplianceStreamWriter.WriteLine(fileHeader);
                strainStreamWriter.WriteLine(fileHeader);

                double time = initialTime;

                while (time <= finalTime)
                {
                    StringBuilder creepComplianceResults = new StringBuilder($"{time};");
                    StringBuilder strainResults = new StringBuilder($"{time};");

                    foreach (var input in inputList)
                    {
                        double creepCompliance = this._viscoelasticModel.CalculateCreepCompliance(input, time);
                        creepComplianceResults.Append($"{creepCompliance};");

                        double strain = this._viscoelasticModel.CalculateStrain(input, time);
                        strainResults.Append($"{strain};");
                    }

                    creepComplianceStreamWriter.WriteLine(creepComplianceResults);
                    strainStreamWriter.WriteLine(strainResults);

                    time += timeStep;
                }
            }
        }

        /// <summary>
        /// This method executes an analysis to calculate the strain for a linear viscoelasticity model.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<CalculateStrainResponse> ProcessOperation(CalculateStrainSensitivityAnalysisRequest request)
        {
            var response = new CalculateStrainResponse { Data = new CalculateStrainResponseData() };
            response.SetSuccessCreated();

            List<TInput> inputList = BuildInputList(request);

            using (StreamWriter streamWriter = new StreamWriter(CreateInputFile()))
            {
                WriteInputData(inputList, streamWriter);
            }

            try
            {
                CalculateAndWriteResults(inputList, request.InitialTime, request.FinalTime, request.TimeStep);
            }
            catch (Exception ex)
            {
                response.AddError(OperationErrorCode.InternalServerError, $"Error trying to calculate and write the solutions in file. {ex.Message}.", HttpStatusCode.InternalServerError);
                response.SetInternalServerError();
            }

            return Task.FromResult(response);
        }
    }
}
