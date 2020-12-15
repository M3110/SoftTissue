using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.SimplifiedFung;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.DisregardRampTime.ConsiderSimplifiedRelaxationFunction;
using SoftTissue.Infrastructure.Models;
using System.Collections.Generic;
using System.IO;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.DisregardRampTime.ConsiderSimplifiedRelaxationFunction
{
    /// <summary>
    /// It is responsible to calculate the stress disregarding the ramp time and considering the Simplified Reduced Relaxation Function to Fung Model.
    /// </summary>
    public class CalculateSimplifiedFungModelStressDisregardRampTime : 
        CalculateFungModelStress<
            CalculateSimplifiedFungModelStressDisregardRampTimeRequest, 
            CalculateSimplifiedFungModelStressDisregardRampTimeResponse,
            CalculateSimplifiedFungModelStressDisregardRampTimeResponseData,
            SimplifiedFungModelInput,
            SimplifiedReducedRelaxationFunctionData,
            SimplifiedFungModelResult>, 
        ICalculateSimplifiedFungModelStressDisregardRampTime
    {
        /// <summary>
        /// The base path to files.
        /// </summary>
        protected override string TemplateBasePath => Path.Combine(Constants.SimplifiedFungModelBasePath, "Disregard Ramp Time");

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateSimplifiedFungModelStressDisregardRampTime(ISimplifiedFungModel viscoelasticModel) : base(viscoelasticModel) { }

        /// <summary>
        /// This method builds a list of <see cref="FungModelInput"/> based on the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override List<SimplifiedFungModelInput> BuildInputList(CalculateSimplifiedFungModelStressDisregardRampTimeRequest request)
        {
            var inputs = new List<SimplifiedFungModelInput>();

            foreach (var requestData in request.Data)
            {
                inputs.Add(new SimplifiedFungModelInput
                {
                    ViscoelasticConsideration = ViscoelasticConsideration.DisregardRampTime,
                    SoftTissueType = requestData.SoftTissueType,
                    InitialStress = requestData.InitialStress,
                    MaximumStrain = requestData.Strain,
                    ReducedRelaxationFunctionInput = requestData.ReducedRelaxationFunctionData,
                    InitialTime = requestData.InitialTime ?? request.InitialTime,
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
                streamWriter.WriteLine($"Final Strain Time;{input.FinalStrainTime};s");
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
    }
}
