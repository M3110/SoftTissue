using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftTissue.Application.Extensions;
using SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrainSensitivityAnalysis.MaxwellModel;
using SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStressSensitivityAnalysis.MaxwellModel;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.Linear.CalculateStrain.MaxwellModel;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.Linear.CalculateStress.MaxwellModel;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStress;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStressSensitivityAnalysis;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.CalculateStrain.Maxwell;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.CalculateStress.Maxwell;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.Maxwell;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSentivityAnalysis.Linear;
using System.Threading.Tasks;

namespace SoftTissue.Application.Controllers
{
    /// <summary>
    /// This controller executes linear viscoelasticity analysis.
    /// </summary>
    [Route("api/v1/linear-viscoelasticity")]
    public class LinearViscoelasticityController : ControllerBase
    {
        /// <summary>
        /// It is responsible to execute an analysis to calculate the stress considering Maxwell Model.
        /// </summary>
        /// <param name="calculateMaxwellModelStress"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created files.</response>
        /// <response code="400">If some validation do not passed.</response>
        /// <response code="500">If occurred some error in process.</response>
        /// <response code="501">If some resource is not implemented.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("calculate-stress/maxwell-model")]
        public async Task<ActionResult<CalculateStrainResponse>> CalculateStress(
            [FromServices] ICalculateMaxwellModelStress calculateMaxwellModelStress,
            [FromQuery] CalculateMaxwellModelResultsRequest request)
        {
            CalculateMaxwellModelResultsResponse response = await calculateMaxwellModelStress.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// It is responsible to execute a sensitivity analysis while calculating the stress considering Maxwell Model.
        /// </summary>
        /// <param name="calculateMaxwellModelStressSensitivityAnalysis"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created files.</response>
        /// <response code="400">If some validation do not passed.</response>
        /// <response code="500">If occurred some error in process.</response>
        /// <response code="501">If some resource is not implemented.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("calculate-stress/maxwell-model/sensitivity-analysis")]
        public async Task<ActionResult<CalculateStrainResponse>> CalculateStressSensitivityAnalysis(
            [FromServices] ICalculateMaxwellModelStressSensitivityAnalysis calculateMaxwellModelStressSensitivityAnalysis,
            [FromQuery] CalculateStressSensitivityAnalysisRequest request)
        {
            CalculateMaxwellModelResultsResponse response = await calculateMaxwellModelStressSensitivityAnalysis.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// It is responsible to execute an analysis to calculate the strain considering Maxwell Model.
        /// </summary>
        /// <param name="calculateMaxwellModelStrain"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created files.</response>
        /// <response code="400">If some validation do not passed.</response>
        /// <response code="500">If occurred some error in process.</response>
        /// <response code="501">If some resource is not implemented.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("calculate-strain/maxwell-model")]
        public async Task<ActionResult<CalculateStrainResponse>> CalculateStrain(
            [FromServices] ICalculateMaxwellModelStrain calculateMaxwellModelStrain,
            [FromQuery] CalculateMaxwellModelStrainRequest request)
        {
            CalculateStrainResponse response = await calculateMaxwellModelStrain.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// It is responsible to execute a sensitivity analysis while calculating the strain considering Maxwell Model.
        /// </summary>
        /// <param name="calculateMaxwellModelStrainSensitivityAnalysis"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created files.</response>
        /// <response code="400">If some validation do not passed.</response>
        /// <response code="500">If occurred some error in process.</response>
        /// <response code="501">If some resource is not implemented.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("calculate-strain/maxwell-model/sensitivity-analysis")]
        public async Task<ActionResult<CalculateStrainResponse>> CalculateStrain(
            [FromServices] ICalculateMaxwellModelStrainSensitivityAnalysis calculateMaxwellModelStrainSensitivityAnalysis,
            [FromQuery] CalculateMaxwellModelResultsSensitivityAnalysisRequest request)
        {
            CalculateStrainResponse response = await calculateMaxwellModelStrainSensitivityAnalysis.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }
    }
}