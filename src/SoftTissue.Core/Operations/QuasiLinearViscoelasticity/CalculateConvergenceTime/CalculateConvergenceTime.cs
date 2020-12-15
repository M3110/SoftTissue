using SoftTissue.Core.Models;
using SoftTissue.Core.NumericalMethods.Integral.Simpson;
using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.OperationBase;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateConvergenceTime_;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateConvergenceTime
{
    public class CalculateConvergenceTime : OperationBase<CalculateConvergenceTimeRequest, CalculateConvergenceTimeResponse, CalculateConvergenceTimeResponseData>, ICalculateConvergenceTime
    {
        private readonly ISimpsonRuleIntegration _simpsonRuleIntegration;

        public CalculateConvergenceTime(ISimpsonRuleIntegration simpsonRuleIntegration)
        {
            this._simpsonRuleIntegration = simpsonRuleIntegration;
        }

        protected override async Task<CalculateConvergenceTimeResponse> ProcessOperation(CalculateConvergenceTimeRequest request)
        {
            var response = new CalculateConvergenceTimeResponse { Data = new CalculateConvergenceTimeResponseData() };

            var tasks = new List<Task>();
            tasks.Add(Task.Run(async () => await this.CalculateConvergenceTimeFirstWay(request)));
            tasks.Add(Task.Run(async () => await this.CalculateConvergenceTimeSecondWay(request)));

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return response;
        }

        public Task<CalculateConvergenceTimeResponse> CalculateConvergenceTimeFirstWay(CalculateConvergenceTimeRequest request)
        {
            var response = new CalculateConvergenceTimeResponse { Data = new CalculateConvergenceTimeResponseData() };

            Func<double, double> equation = (variable) => Math.Exp(-variable) / variable;

            double fastParameter = 1 / request.FastRelaxationTime + request.ElasticPowerConstant * request.StrainRate;
            double slowParameter = 1 / request.SlowRelaxationTime + request.ElasticPowerConstant * request.StrainRate;

            double rampTime = request.MaximumStrain / request.StrainRate;

            double time = rampTime + request.TimeStep;

            double fastValue = 0;
            double slowValue = 0;
            bool continueCalculating = true;

            while (continueCalculating)
            {
                fastValue = (1 / fastParameter) * this._simpsonRuleIntegration.Calculate(
                    equation,
                    new IntegralInput
                    {
                        FinalPoint = time * fastParameter,
                        InitialPoint = (time - rampTime) * fastParameter,
                        Step = 1e-3
                    });

                slowValue = (1 / slowParameter) * this._simpsonRuleIntegration.Calculate(equation,
                    new IntegralInput
                    {
                        FinalPoint = time * slowParameter,
                        InitialPoint = (time - rampTime) * slowParameter,
                        Step = 1e-3
                    });

                if (Math.Abs(fastValue - slowValue) <= Constants.Precision)
                {
                    continueCalculating = false;

                    response.Data.ConvergenceTime = time;
                }

                time += request.TimeStep;

                if (time >= request.FinalTime)
                {
                    response.AddError(OperationErrorCode.InternalServerError, $"Was not possible to calculate the convergence time before {request.FinalTime} seconds.", HttpStatusCode.InternalServerError);
                }
            }

            return Task.FromResult(response);
        }

        public Task<CalculateConvergenceTimeResponse> CalculateConvergenceTimeSecondWay(CalculateConvergenceTimeRequest request)
        {
            var response = new CalculateConvergenceTimeResponse { Data = new CalculateConvergenceTimeResponseData() };

            return Task.FromResult(response);
        }
    }
}
