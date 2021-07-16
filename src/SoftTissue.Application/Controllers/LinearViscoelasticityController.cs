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
        /// Calculate the stress for Maxwell Model.
        /// </summary>
        /// <param name="operation"></param>
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
            [FromServices] ICalculateMaxwellModelStress operation,
            [FromQuery] CalculateMaxwellModelStressRequest request)
        {
            CalculateResultsResponse response = await operation.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// Calculate the strain for Maxwell Model.
        /// </summary>
        /// <param name="operation"></param>
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
            [FromServices] ICalculateMaxwellModelStrain operation,
            [FromQuery] CalculateMaxwellModelStrainRequest request)
        {
            CalculateResultsResponse response = await operation.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// Calculate the results for Maxwell Model.
        /// </summary>
        /// <param name="operation"></param>
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
            [FromServices] ICalculateMaxwellModelResults operation,
            [FromQuery] CalculateMaxwellModelResultsRequest request)
        {
            CalculateResultsResponse response = await operation.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// Calculate the results for Maxwell Model and execute a sensitivity analysis.
        /// </summary>
        /// <param name="operation"></param>
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
            [FromServices] ICalculateMaxwellModelResultsSensitivityAnalysis operation,
            [FromQuery] CalculateMaxwellModelResultsSensitivityAnalysisRequest request)
        {
            CalculateResultsSensitivityAnalysisResponse response = await operation.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }
    }
}