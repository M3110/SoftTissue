using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung;
using SoftTissue.Core.ExtensionMethods;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.Fung;
using System.IO;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.Fung
{
    /// <summary>
    /// It is responsible to calculate the results disregarding the ramp time to Fung Model.
    /// </summary>
    public class CalculateFungModelStressDisregardRampTime :
        CalculateQuasiLinearModelResultsDisregardRampTime<
            CalculateFungModelResultsDisregardRampTimeRequest,
            CalculateFungModelResultsDisregardRampTimeRequestData,
            FungModelInput,
            FungModelResult,
            ReducedRelaxationFunctionData>,
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
        /// This method validates the parameters for Reduced Relaxation Function.
        /// </summary>
        /// <param name="reducedRelaxationFunctionData"></param>
        /// <param name="response"></param>
        protected override void ValidateReducedRelaxationFunctionData(ReducedRelaxationFunctionData reducedRelaxationFunctionData, CalculateResultsResponse response)
            => reducedRelaxationFunctionData.Validate(response);
    }
}
