using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftTissue.Application.Extensions;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateConvergenceTime;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.DisregardRampTime.ConsiderSimplifiedRelaxationFunction;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.GenerateDomain.FungModel;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.Fung;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.SimplifiedFung;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.Fung;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateConvergenceTime;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.DisregardRampTime.ConsiderSimplifiedRelaxationFunction;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.GenerateDomain;
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
        /// It is responsible to execute an analysis to calculate the stress applied to Fung Model considering ramp time and the Reduced Relaxation Function.
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
        [HttpPost("calculate-stress/fung-model/consider-ramp-time")]
        public async Task<ActionResult<CalculateFungModelResultsConsiderRampTimeResponse>> CalculateFungModelStressConsiderRampTime(
            [FromServices] ICalculateFungModelResultsConsiderRampTime operation,
            [FromBody] CalculateFungModelResultsConsiderRampTimeRequest request)
        {
            CalculateFungModelResultsConsiderRampTimeResponse response = await operation.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// It is responsible to execute an analysis to calculate the stress applied to Fung Model disregarding ramp time and considering the Reduced Relaxation Function.
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
        [HttpPost("calculate-stress/fung-model/disregard-ramp-time")]
        public async Task<ActionResult<CalculateFungModelResultsDisregardRampTimeResponse>> CalculateFungModelStressDisregardRampTime(
            [FromServices] ICalculateFungModelStressDisregardRampTime operation,
            [FromBody] CalculateFungModelResultsDisregardRampTimeRequest request)
        {
            CalculateFungModelResultsDisregardRampTimeResponse response = await operation.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// It is responsible to execute an analysis to calculate the stress applied to Fung Model disregarding ramp time and considering the Simplified Reduced Relaxation Function.
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
        [HttpPost("calculate-stress/simplified-fung-model/consider-ramp-time")]
        public async Task<ActionResult<CalculateSimplifiedFungModelResultsConsiderRampTimeResponse>> CalculateSimplifiedFungModelStressConsiderRampTime(
            [FromServices] ICalculateSimplifiedFungModelResultsConsiderRampTime operation,
            [FromBody] CalculateSimplifiedFungModelResultsConsiderRampTimeRequest request)
        {
            CalculateSimplifiedFungModelResultsConsiderRampTimeResponse response = await operation.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// It is responsible to execute an analysis to calculate the stress applied to Fung Model disregarding ramp time and considering the Reduced Relaxation Function.
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
        [HttpPost("calculate-stress/simplified-fung-model/disregard-ramp-time")]
        public async Task<ActionResult<CalculateSimplifiedFungModelResultsDisregardRampTimeResponse>> CalculateSimplifiedFungModelStressDisregardRampTime(
            [FromServices] ICalculateSimplifiedFungModelStressDisregardRampTime operation,
            [FromBody] CalculateSimplifiedFungModelResultsDisregardRampTimeRequest request)
        {
            CalculateSimplifiedFungModelResultsDisregardRampTimeResponse response = await operation.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// It is responsible to execute an analysis to calculate the stress considering Fung Model.
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
        [HttpPost("calculate-stress/simplifiedfung-model/disregard-ramp-time/experimental-model")]
        public async Task<ActionResult<CalculateSimplifiedFungModelResultsDisregardRampTimeResponse>> CalculateSimplifiedFungModelStressDisregardRampTimeToExperimentalModel(
            [FromServices] ICalculateSimplifiedFungModelStressDisregardRampTime operation,
            [FromBody] CalculateSimplifiedFungModelStressDisregardRampTimeToExperimentalModelRequest request)
        {
            CalculateSimplifiedFungModelResultsDisregardRampTimeResponse response = await operation.ProcessAsync(request.CreateQuasiLinearRequest()).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// It is responsible to execute a sensitivity analysis while calculating the stress considering Fung Model.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created files.</response>
        /// <response code="400">If some validation do not passed.</response>
        /// <response code="500">If occurred some error in process.</response>
        /// <response code="501">If some resource is not implemented.</response>
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status501NotImplemented)]
        //[HttpPost("calculate-stress/fung-model/sensitivity-analysis")]
        //public async Task<ActionResult<CalculateStressResponse>> CalculateStressSensivityAnalysis(
        //    [FromServices] ICalculateFungModelStressSentivityAnalysis operation,
        //    [FromBody] CalculateStressSensitivityAnalysisRequest request)
        //{
        //    CalculateStressResponse response = await calculateFungModelStressSentivityAnalysis.Process(request).ConfigureAwait(false);
        //    return response.BuildHttpResponse();
        //}

        /// <summary>
        /// It is responsible to execute a sensitivity analysis while calculating the stress considering Fung Model.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created files.</response>
        /// <response code="400">If some validation do not passed.</response>
        /// <response code="500">If occurred some error in process.</response>
        /// <response code="501">If some resource is not implemented.</response>
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status501NotImplemented)]
        //[HttpPost("calculate-stress/fung-model/sensitivity-analysis/explicit")]
        //public async Task<ActionResult<CalculateStressResponse>> CalculateStressSensivityAnalysisExplicit(
        //    [FromServices] ICalculateFungModelStressSentivityAnalysis operation,
        //    [FromBody] CalculateStressSensitivityAnalysisExplicitRequest request)
        //{
        //    CalculateStressResponse response = await calculateFungModelStressSentivityAnalysis.Process(request.MapToSensitivityAnalysis()).ConfigureAwait(false);
        //    return response.BuildHttpResponse();
        //}

        /// <summary>
        /// It is responsible to generate the domain for Fung Model parameters.
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
        [HttpPost("generate-domain/fung-model")]
        public async Task<ActionResult<GenerateDomainResponse>> GenerateFungModelDomain(
            [FromServices] IGenerateFungModelDomain operation,
            [FromBody] GenerateDomainRequest request)
        {
            GenerateDomainResponse response = await operation.ProcessAsync(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        /// <summary>
        /// It is responsible to calculate the convergence time when the stress is constant.
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