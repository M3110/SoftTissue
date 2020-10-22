using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung;
using SoftTissue.Core.ExtensionMethods;
using SoftTissue.Core.Models;
using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.GenerateDominium;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.GenerateDominium.FungModel
{
    public class GenerateFungModelDominium : OperationBase<GenerateDominiumRequest, GenerateDominiumResponse, GenerateDominiumResponseData>, IGenerateFungModelDominium
    {
        private readonly IFungModel _fungModel;

        public GenerateFungModelDominium(IFungModel fungModel)
        {
            this._fungModel = fungModel;
        }

        protected override Task<GenerateDominiumResponse> ProcessOperation(GenerateDominiumRequest request)
        {
            var response = new GenerateDominiumResponse { Data = new GenerateDominiumResponseData() };
            response.SetSuccessCreated();

            IEnumerable<GenerateFungModelDominiumInput> inputList = this.BuildInputList(request);

            double time = request.InitialTime;

            foreach (var input in inputList)
            {
                while (time < request.FinalTime)
                {
                    // The inequation used to generate the dominium.
                    // integral(e^-x/x) from time/slow relaxation time to time/fast relaxation time < ln(slow relaxation time/fast relaxation time)
                    // The left size of equation.
                    double leftSize = this._fungModel.CalculateI(input.SlowRelaxationTimeList, input.FastRelaxationTimeList, request.TimeStep, time);
                    // The right size of equation.
                    double rightSize = Math.Log(input.SlowRelaxationTimeList / input.FastRelaxationTimeList);

                    if (leftSize < rightSize)
                    {
                        // escrever os valores no arquivos e pegar o espaço de tempo
                    }

                    time += request.TimeStep;
                }
            }

            return Task.FromResult(response);
        }

        public IEnumerable<GenerateFungModelDominiumInput> BuildInputList(GenerateDominiumRequest request)
        {
            var inputList = new List<GenerateFungModelDominiumInput>();

            IEnumerable<double> fastRelaxationTimeList = request.FastRelaxationTimeList.ToEnumerable();
            IEnumerable<double> slowRelaxationTimeList = request.SlowRelaxationTimeList.ToEnumerable();

            foreach (double fastRelaxationTime in fastRelaxationTimeList)
            {
                foreach (double slowRelaxationTime in slowRelaxationTimeList)
                {
                    inputList.Add(new GenerateFungModelDominiumInput
                    {
                        FastRelaxationTimeList = fastRelaxationTime,
                        SlowRelaxationTimeList = slowRelaxationTime
                    });
                }
            }

            return inputList;
        }
    }
}
