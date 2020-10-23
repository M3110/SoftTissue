using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftTissue.Application.Extensions;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.GenerateDomain.FungModel;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.GenerateDomain;
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
        public async Task<ActionResult<CalculateQuasiLinearViscoelasticityStressResponse>> CalculateStress(
            [FromServices] ICalculateFungModelStress calculateFungModelStress,
            [FromBody] CalculateQuasiLinearViscoelasticityStressRequest request)
        {
            CalculateQuasiLinearViscoelasticityStressResponse response = await calculateFungModelStress.Process(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("calculate-stress/fung-model/sensitivity-analysis")]
        public async Task<ActionResult<CalculateQuasiLinearViscoelasticityStressResponse>> CalculateStressSensivityAnalysis(
            [FromServices] ICalculateFungModelStressSentivityAnalysis calculateFungModelStressSentivityAnalysis,
            [FromBody] CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisRequest request)
        {
            CalculateQuasiLinearViscoelasticityStressResponse response = await calculateFungModelStressSentivityAnalysis.Process(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("calculate-stress/fung-model/sensitivity-analysis/explicit")]
        public async Task<ActionResult<CalculateQuasiLinearViscoelasticityStressResponse>> CalculateStressSensivityAnalysisExplicit(
            [FromServices] ICalculateFungModelStressSentivityAnalysis calculateFungModelStressSentivityAnalysis,
            [FromBody] CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisExplicitRequest request)
        {
            CalculateQuasiLinearViscoelasticityStressResponse response = await calculateFungModelStressSentivityAnalysis.Process(request.MapToSensitivityAnalysis()).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("calculate-stress/fung-model/generate-domain")]
        public async Task<ActionResult<GenerateDomainResponse>> GenerateFungModelDomain(
            [FromServices] IGenerateFungModelDomain generateFungModelDomain,
            [FromBody] GenerateDomainRequest request)
        {
            GenerateDomainResponse response = await generateFungModelDomain.Process(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }
    }
}