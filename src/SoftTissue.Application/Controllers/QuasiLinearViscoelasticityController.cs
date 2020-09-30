using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftTissue.Application.Extensions;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.FungModel;
using System.Threading.Tasks;

namespace SoftTissue.Application.Controllers
{
    /// <summary>
    /// This controller executes quasi-linear viscoelasticity analysis.
    /// </summary>
    [Route("api/v1/quasi-linear-viscoelasticity")]
    public class QuasiLinearViscoelasticityController : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("calculate-stress/fung-model")]
        public async Task<ActionResult<CalculateFungModelStressResponse>> CalculateStress(
            [FromServices] ICalculateFungModelStress calculateFungModelStress,
            [FromBody] CalculateFungModelStressRequest request)
        {
            CalculateFungModelStressResponse response = await calculateFungModelStress.Process(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("calculate-stress/fung-model/sensitivity-analysis")]
        public async Task<ActionResult<CalculateFungModelStressResponse>> CalculateStressSensivityAnalysis(
            [FromServices] ICalculateFungModelStressSentivityAnalysis calculateFungModelStressSentivityAnalysis,
            [FromBody] CalculateFungModelStressSensitivityAnalysisRequest request)
        {
            CalculateFungModelStressResponse response = await calculateFungModelStressSentivityAnalysis.Process(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }
    }
}