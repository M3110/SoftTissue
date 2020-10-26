using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung;
using SoftTissue.Core.ExtensionMethods;
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
        /// The base path to files.
        /// </summary>
        private static readonly string TemplateBasePath = Path.Combine(
            Directory.GetCurrentDirectory(), 
            "sheets");

        /// <summary>
        /// The header to solution file.
        /// This property depends on what is calculated on method <see cref="CalculateAndWriteResults"/>.
        /// </summary>
        public virtual string DomainFileHeader => $"Fast Relaxation Time;Solw Relaxation Time;Initial Time;Final Time";

        /// <summary>
        /// The header to solution file.
        /// This property depends on what is calculated on method <see cref="CalculateAndWriteResults"/>.
        /// </summary>
        public virtual string SolutionFileHeader => $"Time;Left Size/Right Size";

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="fungModel"></param>
        public GenerateFungModelDomain(IFungModel fungModel)
        {
            this._fungModel = fungModel;
        }

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
                        InitialTime = 0,
                        TimeStep = request.TimeStep,
                        FinalTime = request.FinalTime
                    });
                }
            }

            return inputList;
        }

        public virtual string CreateDomainFile()
        {
            var fileInfo = new FileInfo(Path.Combine(
                TemplateBasePath,
                $"Domain.csv"));

            if (fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }

            return fileInfo.FullName;
        }

        public virtual string CreateSolutionFile(GenerateFungModelDomainInput input)
        {
            var fileInfo = new FileInfo(Path.Combine(
                TemplateBasePath,
                $"Solution_Tau1={input.FastRelaxationTime}_Tau2={input.SlowRelaxationTime}.csv"));

            if (fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }

            return fileInfo.FullName;
        }

        protected override Task<GenerateDomainResponse> ProcessOperation(GenerateDomainRequest request)
        {
            var response = new GenerateDomainResponse { Data = new GenerateDomainResponseData() };
            response.SetSuccessCreated();

            using (StreamWriter domainStreamWriter = new StreamWriter(this.CreateDomainFile()))
            {
                domainStreamWriter.WriteLine(this.DomainFileHeader);

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
