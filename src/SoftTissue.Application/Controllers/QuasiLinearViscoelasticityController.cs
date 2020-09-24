using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftTissue.Application.Extensions;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.FungModel;
using System.Threading.Tasks;

namespace SoftTissue.Application.Controllers
{
    /// <summary>
    /// This controller executes linear viscoelasticity analysis.
    /// </summary>
    [Route("api/v1/linear-viscoelasticity")]
    public class QuasiLinearViscoelasticityController : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [HttpPost("calculate-stress/fung-model")]
        public async Task<ActionResult<CalculateFungModelStressResponse>> CalculateStress(
            [FromServices] ICalculateFungModelStress calculateFungModelStress,
            [FromQuery] CalculateFungModelStressRequest request)
        {
            CalculateFungModelStressResponse response = await calculateFungModelStress.Process(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }

        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status501NotImplemented)]
        //[HttpPost("calculate-stress/fung-model/sensitivity-test")]
        //public async Task<ActionResult<CalculateQuasiLinearViscoelasticityStressResponse>> CalculateStressByList(
        //    [FromServices] ICalculateFungModelStress calculateFungModelStress,
        //    [FromQuery] CalculateQuasiLinearViscoelasticityStressRequest request)
        //{
        //    CalculateQuasiLinearViscoelasticityStressResponse response = await calculateFungModelStress.Process(request).ConfigureAwait(false);
        //    return response.BuildHttpResponse();
        //}

        //foreach (double strainRate in request.StrainRateList)
        //{
        //    foreach (double maximumStrain in request.MaximumStrainList)
        //    {
        //        foreach (double elasticStressConstant in request.ElasticStressConstantList)
        //        {
        //            foreach (double elasticPowerConstant in request.ElasticPowerConstantList)
        //            {
        //                foreach (double relaxationIndex in request.RelaxationIndexList)
        //                {
        //                    foreach (double fastRelaxationTime in request.FastRelaxationTimeList)
        //                    {
        //                        foreach (double slowRelaxationTime in request.SlowRelaxationTimeList)
        //                        {
        //                            inputList.Add(new QuasiLinearViscoelasticityModelInput
        //                            {
        //                                StrainRate = strainRate,
        //                                MaximumStrain = maximumStrain,
        //                                ElasticStressConstant = elasticStressConstant,
        //                                ElasticPowerConstant = elasticPowerConstant,
        //                                RelaxationIndex = relaxationIndex,
        //                                FastRelaxationTime = fastRelaxationTime,
        //                                SlowRelaxationTime = slowRelaxationTime,
        //                                TimeStep = request.TimeStep
        //                            });
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }
}