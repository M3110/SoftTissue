using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftTissue.Application.Extensions;
using SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrainSensitivityAnalysis.MaxwellModel;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.Linear.CalculateStrain.MaxwellModel;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.Linear.CalculateStress.MaxwellModel;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.Linear.Maxwell;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.CalculateStrain.Maxwell;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.CalculateStress.Maxwell;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.Maxwell;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSensitivityAnalysis;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSensitivityAnalysis.Linear;
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
        /// It is responsible to calculate the stress for Maxwell Model.
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
        [HttpPost("maxwell-model/calculate-stress")]
        public async Task<ActionResult<CalculateResultsResponse>> CalculateStress(
            [FromServices] ICalculateMaxwellModelStress calculateMaxwellModelStress,
            [FromQuery] CalculateMaxwellModelStressRequest request)
        {
            CalculateResultsResponse response = await calculateMaxwellModelStress.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// It is responsible to calculate the strain for Maxwell Model.
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
        [HttpPost("maxwell-model/calculate-strain")]
        public async Task<ActionResult<CalculateResultsResponse>> CalculateStrain(
            [FromServices] ICalculateMaxwellModelStrain calculateMaxwellModelStrain,
            [FromQuery] CalculateMaxwellModelStrainRequest request)
        {
            CalculateResultsResponse response = await calculateMaxwellModelStrain.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// It is responsible to calculate the results for Maxwell Model.
        /// </summary>
        /// <param name="calculateMaxwellModelResults"></param>
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
        [HttpPost("maxwell-model/calculate-results")]
        public async Task<ActionResult<CalculateResultsResponse>> CalculateResults(
            [FromServices] ICalculateMaxwellModelResults calculateMaxwellModelResults,
            [FromQuery] CalculateMaxwellModelResultsRequest request)
        {
            CalculateResultsResponse response = await calculateMaxwellModelResults.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// It is responsible to calculate the results for Maxwell Model and execute a sensitivity analysis.
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
        [HttpPost("maxwell-model/calculate-results/sensitivity-analysis")]
        public async Task<ActionResult<CalculateResultsSensitivityAnalysisResponse>> CalculateStressSensitivityAnalysis(
            [FromServices] ICalculateMaxwellModelResultsSensitivityAnalysis calculateMaxwellModelStressSensitivityAnalysis,
            [FromQuery] CalculateMaxwellModelResultsSensitivityAnalysisRequest request)
        {
            CalculateResultsSensitivityAnalysisResponse response = await calculateMaxwellModelStressSensitivityAnalysis.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }
    }
}