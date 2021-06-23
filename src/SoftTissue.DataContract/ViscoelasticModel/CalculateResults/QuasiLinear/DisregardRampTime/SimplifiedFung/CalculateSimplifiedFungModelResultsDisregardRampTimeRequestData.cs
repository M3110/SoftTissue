using SoftTissue.DataContract.Models;
using System;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.SimplifiedFung
{
    /// <summary>
    /// It represents the 'data' content to CalculateSimplifiedFungModelResultsDisregardRampTime operation request.
    /// </summary>
    public sealed class CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData : CalculateQuasiLinearModelResultsDisregardRampTimeRequestData<SimplifiedReducedRelaxationFunctionData>
    {
        /// <summary>
        /// This method creates a new instance of <see cref="CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData"/>.
        /// </summary>
        /// <param name="experimentalModel"></param>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <returns></returns>
        public static CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData Create(ExperimentalModel experimentalModel, double? timeStep = null, double? finalTime = null)
        {
            double initialStress = experimentalModel switch
            {
                ExperimentalModel.AnteriorCruciateLigamentFirstRelaxation => 2.28012,
                ExperimentalModel.AnteriorCruciateLigamentSecondRelaxation => 1.82552,
                ExperimentalModel.PosteriorCruciateLigamentFirstRelaxation => 1.58844,
                ExperimentalModel.PosteriorCruciateLigamentSecondRelaxation => 1.47335,
                ExperimentalModel.LateralCollateralLigamentFirstRelaxation => 4.09225,
                ExperimentalModel.LateralCollateralLigamentSecondRelaxation => 3.79098,
                ExperimentalModel.MedialCollateralLigamentFirstRelaxation => 0.85733,
                ExperimentalModel.MedialCollateralLigamentSecondRelaxation => 0.79454,
                _ => throw new Exception($"An invalid experimental model was passed.")
            };

            return new CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData
            {
                TimeStep = timeStep,
                FinalTime = finalTime,
                SoftTissueType = experimentalModel.ToString(),
                Strain = 0.06,
                InitialStress = initialStress,
                ReducedRelaxationFunctionData = SimplifiedReducedRelaxationFunctionData.Create(experimentalModel)
            };
        }
    }
}
