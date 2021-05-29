using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.Models;
using System.Collections.Generic;
using System.IO;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.Fung;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.DisregardRampTime.ConsiderRelaxationFunction
{
    /// <summary>
    /// It is responsible to calculate the stress disregarding the ramp time and considering the Reduced Relaxation Function to Fung Model.
    /// </summary>
    public class CalculateFungModelStressDisregardRampTime :
        CalculateQuasiLinearViscoelasticityStress<
            CalculateFungModelResultsDisregardRampTimeRequest,
            CalculateFungModelResultsDisregardRampTimeResponse,
            CalculateFungModelResultsDisregardRampTimeResponseData,
            FungModelInput,
            ReducedRelaxationFunctionData,
            FungModelResult>, 
        ICalculateFungModelStressDisregardRampTime
    {
        /// <summary>
        /// The base path to files.
        /// </summary>
        protected override string TemplateBasePath => Path.Combine(BasePaths.FungModel, "Disregard Ramp Time");

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateFungModelStressDisregardRampTime(IFungModel viscoelasticModel) : base(viscoelasticModel) { }

        /// <summary>
        /// This method builds a list of <see cref="FungModelInput"/> based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override List<FungModelInput> BuildInputList(CalculateFungModelResultsDisregardRampTimeRequest request)
        {
            var inputs = new List<FungModelInput>();

            foreach (var requestData in request.DataList)
            {
                inputs.Add(new FungModelInput
                {
                    ViscoelasticConsideration = ViscoelasticConsideration.DisregardRampTime,
                    SoftTissueType = requestData.SoftTissueType,
                    InitialStress = requestData.InitialStress,
                    MaximumStrain = requestData.Strain,
                    ReducedRelaxationFunctionInput = requestData.ReducedRelaxationFunctionData,
                    TimeStep = requestData.TimeStep ?? request.TimeStep,
                    FinalTime = requestData.FinalTime ?? request.FinalTime
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
    }
}
