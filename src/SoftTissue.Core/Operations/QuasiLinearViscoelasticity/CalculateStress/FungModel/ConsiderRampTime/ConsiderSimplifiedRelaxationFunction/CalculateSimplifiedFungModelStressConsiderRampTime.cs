using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.SimplifiedFung;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.OperationBase;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.ConsiderRampTime.ConsiderSimplifiedRelaxationFunction;
using SoftTissue.Infrastructure.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.ConsiderRampTime.ConsiderSimplifiedRelaxationFunction
{
    /// <summary>
    /// It is responsible to calculate the stress considering the ramp time and the Simplified Reduced Relaxation Function to Fung Model.
    /// </summary>
    public class CalculateSimplifiedFungModelStressConsiderRampTime :
        CalculateQuasiLinearViscoelasticityStress<
            CalculateSimplifiedFungModelStressConsiderRampTimeRequest, 
            CalculateSimplifiedFungModelStressConsiderRampTimeResponse, 
            CalculateSimplifiedFungModelStressConsiderRampTimeResponseData,
            SimplifiedFungModelInput,
            SimplifiedReducedRelaxationFunctionData,
            SimplifiedFungModelResult>, 
        ICalculateSimplifiedFungModelStressConsiderRampTime
    {
        /// <summary>
        /// The base path to files.
        /// </summary>
        protected override string TemplateBasePath => Path.Combine(Constants.SimplifiedFungModelBasePath, "Consider Ramp Time");

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateSimplifiedFungModelStressConsiderRampTime(ISimplifiedFungModel viscoelasticModel) : base(viscoelasticModel) { }

        /// <summary>
        /// This method builds a list of <see cref="FungModelInput"/> based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override List<SimplifiedFungModelInput> BuildInputList(CalculateSimplifiedFungModelStressConsiderRampTimeRequest request)
        {
            var inputs = new List<SimplifiedFungModelInput>();

            foreach (var requestData in request.Data)
            {
                inputs.Add(new SimplifiedFungModelInput
                {
                    ViscoelasticConsideration = requestData.ViscoelasticConsideration,
                    StrainRate = requestData.StrainRate,
                    MaximumStrain = requestData.MaximumStrain,
                    TimeWithConstantMaximumStrain = requestData.TimeWithConstantMaximumStrain,
                    StrainDecreaseRate = requestData.StrainDecreaseRate,
                    MinimumStrain = requestData.MinimumStrain,
                    ElasticStressConstant = requestData.ElasticStressConstant,
                    ElasticPowerConstant = requestData.ElasticPowerConstant,
                    ReducedRelaxationFunctionInput = requestData.ReducedRelaxationFunctionData,
                    TimeStep = requestData.TimeStep ?? request.TimeStep,
                    FinalTime = requestData.FinalTime ?? request.FinalTime,
                    SoftTissueType = requestData.SoftTissueType,
                });
            }

            return inputs;
        }

        /// <summary>
        /// This method writes the input data into a file.
        /// </summary>
        /// <param name="input"></param>
        public override void WriteInput(SimplifiedFungModelInput input)
        {
            using (StreamWriter streamWriter = new StreamWriter(CreateInputFile(input)))
            {
                streamWriter.WriteLine("Parameter;Value;Unit");
                streamWriter.WriteLine($"Soft Tissue type;{input.SoftTissueType};");
                streamWriter.WriteLine($"Viscoelastic Consideration;{input.ViscoelasticConsideration};");
                streamWriter.WriteLine($"Initial Time;{input.InitialTime};s");
                streamWriter.WriteLine($"Time Step;{input.TimeStep};s");
                streamWriter.WriteLine($"Final Time;{input.FinalTime};s");
                streamWriter.WriteLine($"Final Strain Time;{input.DecreaseTime};s");
                streamWriter.WriteLine($"Strain Rate;{input.StrainRate};");
                streamWriter.WriteLine($"Maximum Strain;{input.MaximumStrain};");
                streamWriter.WriteLine($"Elastic Stress Constant;{input.ElasticStressConstant};Pa");
                streamWriter.WriteLine($"Elastic Power Constant;{input.ElasticPowerConstant};");
                streamWriter.WriteLine($"First Viscoelastic Stiffness;{input.ReducedRelaxationFunctionInput.FirstViscoelasticStiffness}");

                var viscoelasticStiffnessList = new List<double>();
                var relaxationTimeList = new List<double>();

                foreach (var iteratorValues in input.ReducedRelaxationFunctionInput.IteratorValues)
                {
                    viscoelasticStiffnessList.Add(iteratorValues.ViscoelasticStiffness);
                    relaxationTimeList.Add(iteratorValues.RelaxationTime);
                }

                streamWriter.WriteLine($"Viscoelastic Stiffness List;{string.Join(" - ", viscoelasticStiffnessList)};");
                streamWriter.WriteLine($"Relaxation Time List;{string.Join(" - ", relaxationTimeList)}");
            }
        }

        /// <summary>
        /// This method validates the request sent to operation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override async Task<CalculateSimplifiedFungModelStressConsiderRampTimeResponse> ValidateOperation(CalculateSimplifiedFungModelStressConsiderRampTimeRequest request)
        {
            CalculateSimplifiedFungModelStressConsiderRampTimeResponse response = await base.ValidateOperation(request).ConfigureAwait(false);

            if (response.Success == false)
            {
                return response;
            }

            foreach (var requestData in request.Data)
            {
                string errorMessage = $"Error on request Data index {request.Data.IndexOf(requestData)}";

                if (requestData.ViscoelasticConsideration == ViscoelasticConsideration.DisregardRampTime)
                {
                    response.AddError(OperationErrorCode.RequestValidationError, $"{errorMessage}. The 'ViscoelasticConsideration' cannot be {requestData.ViscoelasticConsideration} to that operation because the ramp time must be considered.");
                }
            }

            return response;
        }
    }
}
