using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftTissue.Application.Extensions;
using SoftTissue.Core.Operations.ExperimentalAnalysis.AnalyzeAndExtendResults;
using SoftTissue.DataContract.Experimental.AnalyzeAndExtendResults;
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
        /// It is responsible to analyze and extend the experimental results.
        /// </summary>
        /// <param name="analyzeAndExtendResults"></param>
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
        [HttpPost("analyze-and-extend")]
        public async Task<ActionResult<AnalyzeAndExtendResultsResponse>> CalculateStress(
            [FromServices] IAnalyzeAndExtendResults analyzeAndExtendResults,
            [FromQuery] AnalyzeAndExtendResultsRequest request)
        {
            AnalyzeAndExtendResultsResponse response = await analyzeAndExtendResults.Process(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }
    }
}
