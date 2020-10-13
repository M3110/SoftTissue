using SoftTissue.Core.ExtensionMethods;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress;

namespace SoftTissue.Application.Extensions
{
    public static class HttpRequestBuilder
    {
        public static CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisRequest MapToSensitivityAnalysis(this CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisExplicitRequest analysisExplicitRequest)
        {
            if(analysisExplicitRequest == null)
            {
                return null;
            }

            return new CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisRequest
            {
                InitialTime = analysisExplicitRequest.InitialTime,
                TimeStep = analysisExplicitRequest.TimeStep,
                FinalTime = analysisExplicitRequest.FinalTime,
                UseSimplifiedReducedRelaxationFunction = analysisExplicitRequest.UseSimplifiedReducedRelaxationFunction,
                MaximumStrainList = analysisExplicitRequest.MaximumStrainList.ToEnumerable(),
                StrainRateList = analysisExplicitRequest.StrainRateList.ToEnumerable(),
                ElasticStressConstantList = analysisExplicitRequest.ElasticStressConstantList.ToEnumerable(),
                ElasticPowerConstantList = analysisExplicitRequest.ElasticPowerConstantList.ToEnumerable(),
                RelaxationIndexList = analysisExplicitRequest.RelaxationIndexList.ToEnumerable(),
                FastRelaxationTimeList = analysisExplicitRequest.FastRelaxationTimeList.ToEnumerable(),
                SlowRelaxationTimeList = analysisExplicitRequest.SlowRelaxationTimeList.ToEnumerable(),
                SimplifiedReducedRelaxationFunctionDataList = analysisExplicitRequest.SimplifiedReducedRelaxationFunctionDataList
            };
        }
    }
}
