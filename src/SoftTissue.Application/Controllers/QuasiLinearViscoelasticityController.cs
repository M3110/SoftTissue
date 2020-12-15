using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftTissue.Application.Extensions;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateConvergenceTime;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.ConsiderRampTime.ConsiderRelaxationFunction;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.ConsiderRampTime.ConsiderSimplifiedRelaxationFunction;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.DisregardRampTime;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.DisregardRampTime.ConsiderRelaxationFunction;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.DisregardRampTime.ConsiderSimplifiedRelaxationFunction;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.GenerateDomain.FungModel;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateConvergenceTime_;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.DisregardRampTime;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.ConsiderRampTime.ConsiderRelaxationFunction;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.ConsiderRampTime.ConsiderSimplifiedRelaxationFunction;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.DisregardRampTime.ConsiderRelaxationFunction;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.DisregardRampTime.ConsiderSimplifiedRelaxationFunction;
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
        public async Task<ActionResult<CalculateFungModelStressConsiderRampTimeResponse>> CalculateFungModelStressConsiderRampTime(
            [FromServices] ICalculateFungModelStressConsiderRampTime operation,
            [FromBody] CalculateFungModelStressConsiderRampTimeRequest request)
        {
            CalculateFungModelStressConsiderRampTimeResponse response = await operation.Process(request).ConfigureAwait(false);
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
        public async Task<ActionResult<CalculateFungModelStressDisregardRampTimeResponse>> CalculateFungModelStressDisregardRampTime(
            [FromServices] ICalculateFungModelStressDisregardRampTime operation,
            [FromBody] CalculateFungModelStressDisregardRampTimeRequest request)
        {
            CalculateFungModelStressDisregardRampTimeResponse response = await operation.Process(request).ConfigureAwait(false);
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
        public async Task<ActionResult<CalculateSimplifiedFungModelStressConsiderRampTimeResponse>> CalculateSimplifiedFungModelStressConsiderRampTime(
            [FromServices] ICalculateSimplifiedFungModelStressConsiderRampTime operation,
            [FromBody] CalculateSimplifiedFungModelStressConsiderRampTimeRequest request)
        {
            CalculateSimplifiedFungModelStressConsiderRampTimeResponse response = await operation.Process(request).ConfigureAwait(false);
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
        public async Task<ActionResult<CalculateSimplifiedFungModelStressDisregardRampTimeResponse>> CalculateSimplifiedFungModelStressDisregardRampTime(
            [FromServices] ICalculateSimplifiedFungModelStressDisregardRampTime operation,
            [FromBody] CalculateSimplifiedFungModelStressDisregardRampTimeRequest request)
        {
            CalculateSimplifiedFungModelStressDisregardRampTimeResponse response = await operation.Process(request).ConfigureAwait(false);
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
        public async Task<ActionResult<CalculateSimplifiedFungModelStressDisregardRampTimeResponse>> CalculateSimplifiedFungModelStressDisregardRampTimeToExperimentalModel(
            [FromServices] ICalculateSimplifiedFungModelStressDisregardRampTime operation,
            [FromBody] CalculateSimplifiedFungModelStressDisregardRampTimeToExperimentalModelRequest request)
        {
            CalculateSimplifiedFungModelStressDisregardRampTimeResponse response = await operation.Process(request.CreateQuasiLinearRequest()).ConfigureAwait(false);
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
            GenerateDomainResponse response = await operation.Process(request).ConfigureAwait(false);
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
            CalculateConvergenceTimeResponse response = await operation.Process(request).ConfigureAwait(false);
            return response;
        }
    }
}