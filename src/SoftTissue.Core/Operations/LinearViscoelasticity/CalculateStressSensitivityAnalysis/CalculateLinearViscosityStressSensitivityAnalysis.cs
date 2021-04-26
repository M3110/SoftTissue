using SoftTissue.Core.ConstitutiveEquations.LinearModel;
using SoftTissue.Core.ExtensionMethods;
using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.Core.Operations.Base.CalculateResultSensitivityAnalysis;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStress;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStressSensitivityAnalysis;
using SoftTissue.DataContract.OperationBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStressSensitivityAnalysis
{
    /// <summary>
    /// It is responsible to calculate the stress to a linear viscoelastic model.
    /// </summary>
    public abstract class CalculateLinearViscosityStressSensitivityAnalysis<TInput> : CalculateResultSensitivityAnalysis<CalculateStressSensitivityAnalysisRequest, CalculateStressResponse, CalculateStressResponseData, TInput>, ICalculateLinearViscosityStressSensitivityAnalysis<TInput>
        where TInput : LinearViscoelasticityModelInput, new()
    {
        private readonly ILinearViscoelasticityModel<TInput> _viscoelasticModel;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateLinearViscosityStressSensitivityAnalysis(ILinearViscoelasticityModel<TInput> viscoelasticModel) : base(viscoelasticModel)
        {
            _viscoelasticModel = viscoelasticModel;
        }

        /// <summary>
        /// This method builds a list with the inputs based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override List<TInput> BuildInputList(CalculateStressSensitivityAnalysisRequest request)
        {
            var inputList = new List<TInput>();

            foreach (var initialStrain in request.InitialStrainList.ToEnumerable())
            {
                foreach (var stiffness in request.StiffnessList.ToEnumerable())
                {
                    foreach (var viscosity in request.ViscosityList.ToEnumerable())
                    {
                        inputList.Add(new TInput
                        {
                            FinalTime = request.FinalTime,
                            TimeStep = request.TimeStep,
                            InitialStrain = initialStrain,
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
            List<double> initialStrainList = new List<double>();
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
                initialStrainList.Add(input.InitialStrain);
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
            streamWriter.WriteLine($"Initial Strain;{string.Join(';', initialStrainList)};");
            streamWriter.WriteLine($"Viscosity;{string.Join(';', viscosityList)};");
        }

        /// <summary>
        /// This method calculates the results and writes them in a file.
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="initialTime"></param>
        /// <param name="finalTime"></param>
        /// <param name="timeStep"></param>
        public override void CalculateAndWriteResults(List<TInput> inputList, double finalTime, double timeStep, double initialTime = 0)
        {
            using (StreamWriter relaxationFunctionStreamWriter = new StreamWriter(CreateSolutionFile(functionName: "Relaxation Function")))
            using (StreamWriter stressStreamWriter = new StreamWriter(CreateSolutionFile(functionName: "Stress")))
            {
                StringBuilder fileHeader = CreteFileHeader(inputList);

                relaxationFunctionStreamWriter.WriteLine(fileHeader);
                stressStreamWriter.WriteLine(fileHeader);

                double time = initialTime;

                while (time <= finalTime)
                {
                    StringBuilder relaxationFunctionResults = new StringBuilder($"{time};");
                    StringBuilder stressResults = new StringBuilder($"{time};");

                    foreach (var input in inputList)
                    {
                        double relaxationFunction = _viscoelasticModel.CalculateReducedRelaxationFunction(input, time);
                        relaxationFunctionResults.Append($"{relaxationFunction};");

                        double stress = _viscoelasticModel.CalculateStress(input, time);
                        stressResults.Append($"{stress};");
                    }

                    relaxationFunctionStreamWriter.WriteLine(relaxationFunctionResults);
                    stressStreamWriter.WriteLine(stressResults);

                    time += timeStep;
                }
            }
        }

        /// <summary>
        /// Asynchronously, this method executes an analysis to calculate the stress for a linear viscoelasticity model.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<CalculateStressResponse> ProcessOperationAsync(CalculateStressSensitivityAnalysisRequest request)
        {
            var response = new CalculateStressResponse { Data = new CalculateStressResponseData() };
            response.SetSuccessCreated();

            List<TInput> inputList = BuildInputList(request);

            using (StreamWriter streamWriter = new StreamWriter(CreateInputFile()))
            {
                WriteInputData(inputList, streamWriter);
            }

            try
            {
                this.CalculateAndWriteResults(inputList, request.FinalTime, request.TimeStep);
            }
            catch (Exception ex)
            {
                response.SetInternalServerError(OperationErrorCode.InternalServerError, $"Error trying to calculate and write the solutions in file. {ex.Message}.");
            }

            return Task.FromResult(response);
        }
    }
}
