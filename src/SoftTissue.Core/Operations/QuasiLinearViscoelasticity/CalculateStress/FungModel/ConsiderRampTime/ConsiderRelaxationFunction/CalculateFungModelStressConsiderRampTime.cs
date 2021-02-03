using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.OperationBase;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.ConsiderRampTime.ConsiderRelaxationFunction;
using SoftTissue.Infrastructure.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.ConsiderRampTime.ConsiderRelaxationFunction
{
    /// <summary>
    /// It is responsible to calculate the stress considering the ramp time and the Reduced Relaxation Function to Fung Model.
    /// </summary>
    public class CalculateFungModelStressConsiderRampTime :
        CalculateQuasiLinearViscoelasticityStress<
            CalculateFungModelStressConsiderRampTimeRequest, 
            CalculateFungModelStressConsiderRampTimeResponse, 
            CalculateFungModelStressConsiderRampTimeResponseData,
            FungModelInput,
            ReducedRelaxationFunctionData,
            FungModelResult>, 
        ICalculateFungModelStressConsiderRampTime
    {
        /// <summary>
        /// The base path to files.
        /// </summary>
        protected override string TemplateBasePath => Path.Combine(Constants.FungModelBasePath, "Consider Ramp Time");

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateFungModelStressConsiderRampTime(IFungModel viscoelasticModel) : base(viscoelasticModel) { }

        /// <summary>
        /// This method builds a list of <see cref="FungModelInput"/> based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override List<FungModelInput> BuildInputList(CalculateFungModelStressConsiderRampTimeRequest request)
        {
            var inputs = new List<FungModelInput>();

            foreach (var requestData in request.Data)
            {
                inputs.Add(new FungModelInput
                {
                    ViscoelasticConsideration = requestData.ViscoelasticConsideration,
                    StrainRate = requestData.StrainRate,
                    MaximumStrain = requestData.MaximumStrain,
                    TimeWithConstantStrain = requestData.TimeWithConstantStrain,
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
        public override void WriteInput(FungModelInput input)
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
                streamWriter.WriteLine($"Relaxation Index (C);{input.ReducedRelaxationFunctionInput.RelaxationIndex};");
                streamWriter.WriteLine($"Fast Relaxation Time (Tau 1);{input.ReducedRelaxationFunctionInput.FastRelaxationTime};s");
                streamWriter.WriteLine($"Slow Relaxation Time (Tau 2);{input.ReducedRelaxationFunctionInput.SlowRelaxationTime};s");
            }
        }

        /// <summary>
        /// This method validates the request sent to operation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override async Task<CalculateFungModelStressConsiderRampTimeResponse> ValidateOperation(CalculateFungModelStressConsiderRampTimeRequest request)
        {
            CalculateFungModelStressConsiderRampTimeResponse response = await base.ValidateOperation(request).ConfigureAwait(false);

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
