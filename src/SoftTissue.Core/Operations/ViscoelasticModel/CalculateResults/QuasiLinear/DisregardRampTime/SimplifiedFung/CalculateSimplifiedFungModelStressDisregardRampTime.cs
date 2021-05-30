using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.SimplifiedFung;
using SoftTissue.Core.ExtensionMethods;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime;
using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.SimplifiedFung;
using System.IO;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.DisregardRampTime.ConsiderSimplifiedRelaxationFunction
{
    /// <summary>
    /// It is responsible to calculate the results disregarding the ramp time to Simplified Fung Model.
    /// </summary>
    public class CalculateSimplifiedFungModelStressDisregardRampTime :
        CalculateQuasiLinearModelResultsDisregardRampTime<
            CalculateSimplifiedFungModelResultsDisregardRampTimeRequest,
            CalculateSimplifiedFungModelResultsDisregardRampTimeRequestData,
            SimplifiedFungModelInput,
            SimplifiedFungModelResult,
            SimplifiedReducedRelaxationFunctionData>,
        ICalculateSimplifiedFungModelStressDisregardRampTime
    {
        /// <summary>
        /// The base path to files.
        /// </summary>
        protected override string TemplateBasePath => Path.Combine(BasePaths.SimplifiedFungModel, "Disregard Ramp Time");

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateSimplifiedFungModelStressDisregardRampTime(ISimplifiedFungModel viscoelasticModel) : base(viscoelasticModel) { }

        /// <summary>
        /// This method validates the parameters for Reduced Relaxation Function.
        /// </summary>
        /// <param name="reducedRelaxationFunctionData"></param>
        /// <param name="response"></param>
        protected override void ValidateReducedRelaxationFunctionData(SimplifiedReducedRelaxationFunctionData reducedRelaxationFunctionData, CalculateResultsResponse response)
            => reducedRelaxationFunctionData.Validate(response);
    }
}
