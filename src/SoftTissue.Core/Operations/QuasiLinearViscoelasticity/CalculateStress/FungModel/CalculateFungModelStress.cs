using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung;
using SoftTissue.Core.Models;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.FungModel;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel
{
    public class CalculateFungModelStress : CalculateQuasiLinearViscoelasticityStress, ICalculateFungModelStress
    {
        private readonly IFungModel _viscoelasticModel;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateFungModelStress(IFungModel viscoelasticModel) : base(viscoelasticModel) 
        {
            this._viscoelasticModel = viscoelasticModel;
        }

        protected override Task<CalculateFungModelStressResponse> ProcessOperation(CalculateFungModelStressRequest request)
        {
            var response = new CalculateFungModelStressResponse { Data = new CalculateFungModelStressResponseData() };
            response.SetSuccessCreated();

            IEnumerable<QuasiLinearViscoelasticityModelInput> inputList = base.BuildInputList(request);

            int i = 0;
            foreach(var input in inputList)
            {
                string solutionFileName = $"SolutionFile_{i}.csv";
                string inputDataFileName = $"InputDataFile_{i}.csv";
                i++;

                using (StreamWriter streamWriter = new StreamWriter(inputDataFileName))
                {
                    streamWriter.WriteLine($"Initial Time: {request.InitialTime} s");
                    streamWriter.WriteLine($"Time Step: {request.TimeStep} s");
                    streamWriter.WriteLine($"Final Time: {request.FinalTime} s");
                    streamWriter.WriteLine($"Elastic Stress Constant (A): {input.ElasticStressConstant}");
                    streamWriter.WriteLine($"Elastic Power Constant (B): {input.ElasticPowerConstant}");
                    streamWriter.WriteLine($"Relaxation Index (C): {input.RelaxationIndex}");
                    streamWriter.WriteLine($"Fast Relaxation Time (Tau1): {input.FastRelaxationTime} s");
                    streamWriter.WriteLine($"Slow Relaxation Time (Tau2): {input.SlowRelaxationTime} s");
                    streamWriter.WriteLine($"Strain Rate: {input.StrainRate}");
                    streamWriter.WriteLine($"Maximum Strain: {input.MaximumStrain}");
                }

                double time = request.InitialTime;
                using (StreamWriter streamWriter = new StreamWriter(solutionFileName)) 
                {
                    streamWriter.WriteLine("Time;Strain;Reduced Relaxation Function;Elastic Response;Stress;Stress ith dSigma;Stress with dG");

                    while (time <= request.FinalTime)
                    {
                        double strain = this._viscoelasticModel.CalculateStrain(input, time);
                        double reducedRelaxationFunction = this._viscoelasticModel.CalculateReducedRelaxationFunction(input, time);
                        double elasticResponse = this._viscoelasticModel.CalculateElasticResponse(input, time);
                        double stress = this._viscoelasticModel.CalculateStress(input, time);
                        double stressWithElasticResponseDerivative = this._viscoelasticModel.CalculateStressByIntegrationDerivative(input, time);
                        double stressWithReducedRelaxationFunctionDerivative = this._viscoelasticModel.CalculateStressByReducedRelaxationFunctionDerivative(input, time);

                        streamWriter.WriteLine($"{time};{strain};{reducedRelaxationFunction};{elasticResponse};{stress};{stressWithElasticResponseDerivative};{stressWithReducedRelaxationFunctionDerivative}");

                        time += request.TimeStep;
                    }
                }
            }

            return Task.FromResult(response);
        }
    }
}
