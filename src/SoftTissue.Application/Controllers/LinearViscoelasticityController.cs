using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftTissue.Application.Extensions;
using SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrain.MaxwellModel;
using SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStress.MaxwellModel;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStress;
using System.Threading.Tasks;

namespace SoftTissue.Application.Controllers
{
    /// <summary>
    /// This controller executes linear viscoelasticity analysis.
    /// </summary>
    [Route("api/v1/linear-viscoelastic")]
    public class LinearViscoelasticityController : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("calculate-stress/maxwell-model")]
        public async Task<ActionResult<CalculateStrainResponse>> CalculateStress(
            [FromServices] ICalculateMaxwellModelStress calculateMaxwellModelStress,
            [FromBody] CalculateStressRequest request)
        {
            CalculateStressResponse response = await calculateMaxwellModelStress.Process(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("calculate-strain/maxwell-model")]
        public async Task<ActionResult<CalculateStrainResponse>> CalculateStrain(
            [FromServices] ICalculateMaxwellModelStrain calculateMaxwellModelStrain,
            [FromBody] CalculateStrainRequest request)
        {
            CalculateStrainResponse response = await calculateMaxwellModelStrain.Process(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }
    }
}