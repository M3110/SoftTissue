using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.SimplifiedFung;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.SimplifiedFung;
using System.IO;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.SimplifiedFung
{
    /// <summary>
    /// It is responsible to calculate the results considering the ramp time for Simplified Fung Model.
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
    }
}
