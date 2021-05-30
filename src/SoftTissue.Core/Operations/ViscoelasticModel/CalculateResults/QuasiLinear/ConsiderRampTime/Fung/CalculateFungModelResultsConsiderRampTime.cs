using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung;
using SoftTissue.Core.ExtensionMethods;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.Fung;
using System.IO;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.Fung
{
    /// <summary>
    /// It is responsible to calculate the results considering the ramp time to Fung Model.
    /// </summary>
    public class CalculateFungModelResultsConsiderRampTime :
        CalculateQuasiLinearModelResultsConsiderRampTime<
            CalculateFungModelResultsConsiderRampTimeRequest,
            CalculateFungModelResultsConsiderRampTimeRequestData,
            FungModelInput,
            FungModelResult,
            ReducedRelaxationFunctionData>,
        ICalculateFungModelResultsConsiderRampTime
    {
        /// <summary>
        /// The base path to files.
        /// </summary>
        protected override string TemplateBasePath => Path.Combine(BasePaths.FungModel, "Consider Ramp Time");

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateFungModelResultsConsiderRampTime(IFungModel viscoelasticModel) : base(viscoelasticModel) { }

        /// <summary>
        /// This method checks if the parameters for Reduced Relaxation Function is valid.
        /// </summary>
        /// <param name="reducedRelaxationFunctionData"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        protected override void ValidateReducedRelaxationFunctionData(ReducedRelaxationFunctionData reducedRelaxationFunctionData, CalculateResultsResponse response)
            => reducedRelaxationFunctionData.Validate(response);
    }
}
