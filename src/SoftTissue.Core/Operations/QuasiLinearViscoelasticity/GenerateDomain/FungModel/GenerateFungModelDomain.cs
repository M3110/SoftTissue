using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung;
using SoftTissue.Core.ExtensionMethods;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.GenerateDomain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.GenerateDomain.FungModel
{
    /// <summary>
    /// It is responsible to generate the domain with valid parameters for Fung Model.
    /// Only the fast and slow relaxation times are considered here, the other parameters (Elastic Tension Constant, Elastic Power Constant and Relaxation Index) have no limitations, being valid for the entire positive real domain.
    /// </summary>
    public class GenerateFungModelDomain : OperationBase<GenerateDomainRequest, GenerateDomainResponse, GenerateDomainResponseData>, IGenerateFungModelDomain
    {
        private readonly IFungModel _fungModel;

        /// <summary>
        /// The base path to domain analysis.
        /// </summary>
        public virtual string TemplateBasePath => Path.Combine(BasePaths.FungModel, "Domain");

        /// <summary>
        /// The header to solution file.
        /// This property depends on what wants to write on file.
        /// </summary>
        public virtual string DomainFileHeader => $"Fast Relaxation Time;Solw Relaxation Time;Initial Time;Final Time";

        /// <summary>
        /// The header to solution file.
        /// This property depends on what wants to write on file.
        /// </summary>
        public virtual string SolutionFileHeader => $"Time;Left Size;Right Size";

        /// <summary>
        /// The main folder for each analysis.
        /// </summary>
        public string MainFolder { get; set; }

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="fungModel"></param>
        public GenerateFungModelDomain(IFungModel fungModel)
        {
            this._fungModel = fungModel;
        }

        /// <summary>
        /// This method builds the input list.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual List<GenerateFungModelDomainInput> BuildInputList(GenerateDomainRequest request)
        {
            var inputList = new List<GenerateFungModelDomainInput>();

            IEnumerable<double> fastRelaxationTimeList = request.FastRelaxationTimeList.ToEnumerable();
            IEnumerable<double> slowRelaxationTimeList = request.SlowRelaxationTimeList.ToEnumerable();

            foreach (double fastRelaxationTime in fastRelaxationTimeList)
            {
                foreach (double slowRelaxationTime in slowRelaxationTimeList)
                {
                    inputList.Add(new GenerateFungModelDomainInput
                    {
                        FastRelaxationTime = fastRelaxationTime,
                        SlowRelaxationTime = slowRelaxationTime,
                        TimeStep = request.TimeStep,
                        FinalTime = request.FinalTime
                    });
                }
            }

            return inputList;
        }

        /// <summary>
        /// This method creates the domain file.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual string CreateDomainFile(GenerateDomainRequest request)
        {
            this.MainFolder = $"Tau1={request.FastRelaxationTimeList.InitialPoint}; {request.FastRelaxationTimeList.FinalPoint} - Tau2={request.SlowRelaxationTimeList.InitialPoint}; {request.SlowRelaxationTimeList.FinalPoint}";

            var fileInfo = new FileInfo(Path.Combine(
                this.TemplateBasePath,
                this.MainFolder,
                $"Domain - {this.MainFolder}.csv"));

            if (fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }

            return fileInfo.FullName;
        }

        /// <summary>
        /// This method creates the solution file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual string CreateSolutionFile(GenerateFungModelDomainInput input)
        {
            var fileInfo = new FileInfo(Path.Combine(
                this.TemplateBasePath,
                this.MainFolder,
                $"Solution_Tau1={input.FastRelaxationTime}_Tau2={input.SlowRelaxationTime}.csv"));

            if (fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }

            return fileInfo.FullName;
        }

        /// <summary>
        /// Asynchronously, this method generates the domain for slow and fast relaxation times.
        /// The valid domain is where the variables usaed for slow and fast relaxation times obey the inequation below for all times.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<GenerateDomainResponse> ProcessOperationAsync(GenerateDomainRequest request)
        {
            var response = new GenerateDomainResponse { Data = new GenerateDomainResponseData() };
            response.SetSuccessCreated();

            using (StreamWriter domainStreamWriter = new StreamWriter(this.CreateDomainFile(request)))
            {
                domainStreamWriter.WriteLine(this.DomainFileHeader);

                // TODO: Usar SemaphoreSlim para ter no máximo 100 threads
                Parallel.ForEach(this.BuildInputList(request), input =>
                {
                    using (StreamWriter solutionStreamWriter = new StreamWriter(this.CreateSolutionFile(input)))
                    {
                        solutionStreamWriter.WriteLine(this.SolutionFileHeader);

                        double time = input.TimeStep;
                        double finalTime = input.FinalTime;

                        // The inequation used to generate the domain:
                        // integral(e^-x/x) from time/slow relaxation time to time/fast relaxation time < ln(slow relaxation time/fast relaxation time)
                        while (time <= input.FinalTime)
                        {
                            // The left size of equation.
                            double leftSize = this._fungModel.CalculateI(input.SlowRelaxationTime, input.FastRelaxationTime, input.TimeStep, time);
                            // The right size of equation.
                            double rightSize = Math.Log(input.SlowRelaxationTime / input.FastRelaxationTime);

                            solutionStreamWriter.WriteLine($"{time};{leftSize};{rightSize}");

                            if (leftSize >= rightSize)
                            {
                                finalTime = time;
                                break;
                            }

                            time += input.TimeStep;
                        }

                        domainStreamWriter.WriteLine($"{input.FastRelaxationTime};{input.SlowRelaxationTime};{input.InitialTime};{finalTime}");
                    }
                });
            }

            return Task.FromResult(response);
        }
    }
}
