using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftTissue.Application.Extensions;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateConvergenceTime;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.GenerateDomain.FungModel;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.Fung;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.SimplifiedFung;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.Fung;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.SimplifiedFung;
using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateConvergenceTime;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.GenerateDomain;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.Fung;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.SimplifiedFung;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.Fung;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.SimplifiedFung;
using System.Threading.Tasks;

namespace SoftTissue.Application.Controllers
{
    /// <summary>
    /// This controller executes quasi-linear viscoelasticity analysis.
    /// </summary>
    [Route("api/v1/quasi-linear-viscoelasticity")]
    public class QuasiLinearViscoelasticityController : ControllerBase
    {
        /// <summary>
        /// Calculates the results for Fung Model considering ramp time.
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
        [HttpPost("fung-model/consider-ramp-time/calculate-results")]
        public async Task<ActionResult<CalculateResultsResponse>> CalculateFungModelResultsConsiderRampTime(
            [FromServices] ICalculateFungModelResultsConsiderRampTime operation,
            [FromBody] CalculateFungModelResultsConsiderRampTimeRequest request)
        {
            CalculateResultsResponse response = await operation.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// Calculates the results for Fung Model disregarding ramp time.
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
        [HttpPost("fung-model/disregard-ramp-time/calculate-results")]
        public async Task<ActionResult<CalculateResultsResponse>> CalculateFungModelResultsDisregardRampTime(
            [FromServices] ICalculateFungModelResultsDisregardRampTime operation,
            [FromBody] CalculateFungModelResultsDisregardRampTimeRequest request)
        {
            CalculateResultsResponse response = await operation.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// Calculates the results for Simplified Fung Model considering ramp time.
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
        [HttpPost("simplified-fung-model/consider-ramp-time/calculate-results")]
        public async Task<ActionResult<CalculateResultsResponse>> CalculateSimplifiedFungModelResultsConsiderRampTime(
            [FromServices] ICalculateSimplifiedFungModelResultsConsiderRampTime operation,
            [FromBody] CalculateSimplifiedFungModelResultsConsiderRampTimeRequest request)
        {
            CalculateResultsResponse response = await operation.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// Calculates the results for Simplified Fung Model disregarding ramp time.
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
        [HttpPost("simplified-fung-model/disregard-ramp-time/calculate-results")]
        public async Task<ActionResult<CalculateResultsResponse>> CalculateSimplifiedFungModelResultsDisregardRampTime(
            [FromServices] ICalculateSimplifiedFungModelResultsDisregardRampTime operation,
            [FromBody] CalculateSimplifiedFungModelResultsDisregardRampTimeRequest request)
        {
            CalculateResultsResponse response = await operation.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// Calculates the results for Simplified Fung Model disregarding ramp time for a specific experimental model.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="strain"></param>
        /// <param name="experimentalModel"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created files.</response>
        /// <response code="400">If some validation do not passed.</response>
        /// <response code="500">If occurred some error in process.</response>
        /// <response code="501">If some resource is not implemented.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("simplifiedfung-model/disregard-ramp-time/experimental-model/calculate-results")]
        public async Task<ActionResult<CalculateResultsResponse>> CalculateSimplifiedFungModelResultsDisregardRampTimeToExperimentalModel(
            [FromServices] ICalculateSimplifiedFungModelResultsDisregardRampTime operation,
            [FromQuery] double timeStep, double finalTime, double strain, ExperimentalModel experimentalModel)
        {
            CalculateResultsResponse response = await operation.ProcessAsync(
                CalculateSimplifiedFungModelResultsDisregardRampTimeRequest.Create(timeStep, finalTime, strain, experimentalModel)).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// Generates the domain for Fung Model parameters.
        /// Here is just considered the fast and slow relaxation times, because the another parameters is avaiable for all real and positive domain.
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
        [HttpPost("fung-model/generate-domain")]
        public async Task<ActionResult<GenerateDomainResponse>> GenerateFungModelDomain(
            [FromServices] IGenerateFungModelDomain operation,
            [FromBody] GenerateDomainRequest request)
        {
            GenerateDomainResponse response = await operation.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// Calculates the convergence time when the results is constant.
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
        [HttpPost("convergence-time")]
        public async Task<ActionResult<CalculateConvergenceTimeResponse>> CalculateConvergenceTime(
            [FromServices] ICalculateConvergenceTime operation,
            [FromQuery] CalculateConvergenceTimeRequest request)
        {
            CalculateConvergenceTimeResponse response = await operation.ProcessAsync(request).ConfigureAwait(false);
            return response;
        }
    }
}