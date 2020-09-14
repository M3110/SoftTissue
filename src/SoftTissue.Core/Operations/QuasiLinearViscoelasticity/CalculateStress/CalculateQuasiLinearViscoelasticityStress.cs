using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel;
using SoftTissue.Core.Models;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress
{
    public class CalculateQuasiLinearViscoelasticityStress : OperationBase<CalculateQuasiLinearViscoelasticityStressRequest, CalculateQuasiLinearViscoelasticityStressResponse, CalculateQuasiLinearViscoelasticityStressResponseData>, ICalculateQuasiLinearViscoelasticityStress
    {
        private readonly IQuasiLinearViscoelasticityModel _viscoelasticModel;

        public CalculateQuasiLinearViscoelasticityStress(IQuasiLinearViscoelasticityModel viscoelasticModel)
        {
            this._viscoelasticModel = viscoelasticModel;
        }

        protected async override Task<CalculateQuasiLinearViscoelasticityStressResponse> ProcessOperation(CalculateQuasiLinearViscoelasticityStressRequest request)
        {
            var response = new CalculateQuasiLinearViscoelasticityStressResponse { Data = new CalculateQuasiLinearViscoelasticityStressResponseData() };

            IEnumerable<QuasiLinearViscoelasticityModelInput> inputList = this.BuildInputList(request);

            foreach (var input in inputList)
            {
                double time = request.InitialTime;
                double strain = input.InitialStrain;

                while (time <= request.FinalTime)
                {
                    double a = await this._viscoelasticModel.CalculateStress(input, time, strain).ConfigureAwait(false);

                    if (strain >= input.InitialStrain || strain < input.MaximumStrain)
                    {
                        strain += input.StrainRate * time;
                    }

                    time += request.TimeStep;
                }
            }

            return response;
        }

        private IEnumerable<QuasiLinearViscoelasticityModelInput> BuildInputList(CalculateQuasiLinearViscoelasticityStressRequest request)
        {
            foreach (double initialStrain in request.InitialStainList)
            {
                foreach (double strainRate in request.StrainRateList)
                {
                    foreach (double maximumStrain in request.MaximumStrainList)
                    {
                        foreach (double strainFinalTime in request.StrainFinalTimeList)
                        {
                            foreach (double variableA in request.VariableAList)
                            {
                                foreach (double variableB in request.VariableBList)
                                {
                                    foreach (double variableC in request.VariableCList)
                                    {
                                        foreach (double relaxationTime1 in request.RelaxationTime1List)
                                        {
                                            foreach (double relaxationTime2 in request.RelaxationTime2List)
                                            {
                                                yield return new QuasiLinearViscoelasticityModelInput
                                                {
                                                    InitialStrain = initialStrain,
                                                    StrainRate = strainRate,
                                                    MaximumStrain = maximumStrain,
                                                    StrainFinalTime = strainFinalTime,
                                                    VariableA = variableA,
                                                    VariableB = variableB,
                                                    VariableC = variableC,
                                                    RelaxationTime1 = relaxationTime1,
                                                    RelaxationTime2 = relaxationTime2
                                                };
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected override Task<CalculateQuasiLinearViscoelasticityStressResponse> ValidateOperation(CalculateQuasiLinearViscoelasticityStressRequest request)
        {
            return base.ValidateOperation(request);
        }
    }
}
