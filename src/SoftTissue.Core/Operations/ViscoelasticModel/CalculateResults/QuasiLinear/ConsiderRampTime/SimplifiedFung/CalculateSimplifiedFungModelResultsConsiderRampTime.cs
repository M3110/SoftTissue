using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.SimplifiedFung;
using SoftTissue.Core.ExtensionMethods;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.SimplifiedFung;
using System.IO;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.SimplifiedFung
{
    /// <summary>
    /// It is responsible to calculate the stress considering the ramp time and the Simplified Reduced Relaxation Function to Fung Model.
    /// </summary>
    public class CalculateSimplifiedFungModelResultsConsiderRampTime :
        CalculateQuasiLinearModelResultsConsiderRampTime<
            CalculateSimplifiedFungModelResultsConsiderRampTimeRequest,
            CalculateSimplifiedFungModelResultsConsiderRampTimeRequestData,
            SimplifiedFungModelInput,
            SimplifiedFungModelResult,
            SimplifiedReducedRelaxationFunctionData>,
        ICalculateSimplifiedFungModelResultsConsiderRampTime
    {
        /// <summary>
        /// The base path to files.
        /// </summary>
        protected override string TemplateBasePath => Path.Combine(BasePaths.SimplifiedFungModel, "Consider Ramp Time");

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateSimplifiedFungModelResultsConsiderRampTime(ISimplifiedFungModel viscoelasticModel) : base(viscoelasticModel) { }

        /// <summary>
        /// This method checks if the parameters for Reduced Relaxation Function is valid.
        /// </summary>
        /// <param name="reducedRelaxationFunctionData"></param>
        /// <param name="response"></param>
        protected override void ValidateReducedRelaxationFunctionData(SimplifiedReducedRelaxationFunctionData reducedRelaxationFunctionData, CalculateResultsResponse response)
            => reducedRelaxationFunctionData.Validate(response);
    }
}
