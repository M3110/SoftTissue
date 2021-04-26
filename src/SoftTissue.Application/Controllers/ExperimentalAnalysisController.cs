using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftTissue.Application.Extensions;
using SoftTissue.Core.Operations.ExperimentalAnalysis.AnalyzeAndExtrapolateResults;
using SoftTissue.Core.Operations.ExperimentalAnalysis.AnalyzeResults;
using SoftTissue.DataContract.ExperimentalAnalysis.AnalyzeAndExtrapolateResults;
using SoftTissue.DataContract.ExperimentalAnalysis.AnalyzeResults;
using System.Threading.Tasks;

namespace SoftTissue.Application.Controllers
{
    /// <summary>
    /// This controller executes operations with the experimental analysis.
    /// </summary>
    [Route("api/v1/experimental")]
    public class ExperimentalAnalysisController : Controller
    {
        /// <summary>
        /// It is responsible to analyze and extrapolate the experimental results.
        /// </summary>
        /// <param name="analyzeAndExtrapolateResults"></param>
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
        [HttpPost("analyze-and-extrapolate")]
        public async Task<ActionResult<AnalyzeAndExtrapolateResultsResponse>> AnalyzeAndExtrapolateResults(
            [FromServices] IAnalyzeAndExtrapolateResults analyzeAndExtrapolateResults,
            [FromQuery] AnalyzeAndExtrapolateResultsRequest request)
        {
            AnalyzeAndExtrapolateResultsResponse response = await analyzeAndExtrapolateResults.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// It is responsible to analyze the experimental results.
        /// </summary>
        /// <param name="analyzeResults"></param>
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
        [HttpPost("analyze")]
        public async Task<ActionResult<AnalyzeResultsResponse>> AnalyzeResults(
            [FromServices] IAnalyzeResults analyzeResults,
            [FromQuery] AnalyzeResultsRequest request)
        {
            AnalyzeResultsResponse response = await analyzeResults.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }
    }
}
