using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftTissue.Application.Extensions;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateConvergenceTime;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.GenerateDomain.FungModel;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateConvergenceTime_;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Request;
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
        /// <summary>
        /// It is responsible to execute an analysis to calculate the stress considering Fung Model.
        /// </summary>
        /// <param name="calculateFungModelStress"></param>
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
        [HttpPost("calculate-stress/fung-model")]
        public async Task<ActionResult<CalculateQuasiLinearViscoelasticityStressResponse>> CalculateStress(
            [FromServices] ICalculateFungModelStress calculateFungModelStress,
            [FromBody] CalculateQuasiLinearViscoelasticityStressRequest request)
        {
            CalculateQuasiLinearViscoelasticityStressResponse response = await calculateFungModelStress.Process(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// It is responsible to execute an analysis to calculate the stress considering Fung Model.
        /// </summary>
        /// <param name="calculateFungModelStress"></param>
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
        [HttpPost("calculate-stress/fung-model/experimental-model")]
        public async Task<ActionResult<CalculateQuasiLinearViscoelasticityStressResponse>> CalculateStressToExperimentalModel(
            [FromServices] ICalculateFungModelStress calculateFungModelStress,
            [FromBody] CalculateQuasiLinearViscoelasticityStressToExperimentalModelRequest request)
        {
            CalculateQuasiLinearViscoelasticityStressResponse response = await calculateFungModelStress.Process(request.CreateQuasiLinearRequest()).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// It is responsible to execute a sensitivity analysis while calculating the stress considering Fung Model.
        /// </summary>
        /// <param name="calculateFungModelStressSentivityAnalysis"></param>
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
        [HttpPost("calculate-stress/fung-model/sensitivity-analysis")]
        public async Task<ActionResult<CalculateQuasiLinearViscoelasticityStressResponse>> CalculateStressSensivityAnalysis(
            [FromServices] ICalculateFungModelStressSentivityAnalysis calculateFungModelStressSentivityAnalysis,
            [FromBody] CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisRequest request)
        {
            CalculateQuasiLinearViscoelasticityStressResponse response = await calculateFungModelStressSentivityAnalysis.Process(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// It is responsible to execute a sensitivity analysis while calculating the stress considering Fung Model.
        /// </summary>
        /// <param name="calculateFungModelStressSentivityAnalysis"></param>
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
        [HttpPost("calculate-stress/fung-model/sensitivity-analysis/explicit")]
        public async Task<ActionResult<CalculateQuasiLinearViscoelasticityStressResponse>> CalculateStressSensivityAnalysisExplicit(
            [FromServices] ICalculateFungModelStressSentivityAnalysis calculateFungModelStressSentivityAnalysis,
            [FromBody] CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisExplicitRequest request)
        {
            CalculateQuasiLinearViscoelasticityStressResponse response = await calculateFungModelStressSentivityAnalysis.Process(request.MapToSensitivityAnalysis()).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// It is responsible to generate the domain for Fung Model parameters.
        /// Here is just considered the fast and slow relaxation times, because the another parameters is avaiable for all real and positive domain.
        /// </summary>
        /// <param name="generateFungModelDomain"></param>
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
        [HttpPost("generate-domain/fung-model")]
        public async Task<ActionResult<GenerateDomainResponse>> GenerateFungModelDomain(
            [FromServices] IGenerateFungModelDomain generateFungModelDomain,
            [FromBody] GenerateDomainRequest request)
        {
            GenerateDomainResponse response = await generateFungModelDomain.Process(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("convergence-time")]
        public async Task<ActionResult<CalculateConvergenceTimeResponse>> CalculateConvergenceTime(
            [FromServices] ICalculateConvergenceTime calculateConvergenceTime,
            [FromQuery] CalculateConvergenceTimeRequest request)
        {
            CalculateConvergenceTimeResponse response = await calculateConvergenceTime.Process(request).ConfigureAwait(false);
            return response;
        }
    }
}