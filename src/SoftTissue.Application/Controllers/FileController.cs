﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftTissue.Application.Extensions;
using SoftTissue.Core.Operations.FileManager.SkipPoints;
using SoftTissue.DataContract.FileManager.SkipPoints;
using System.Threading.Tasks;

namespace SoftTissue.Application.Controllers
{
    /// <summary>
    /// This controller contains operations to file.
    /// </summary>
    [Route("api/v1/file")]
    public class FileController : Controller
    {
        /// <summary>
        /// It is responsible to skip points into a file.
        /// </summary>
        /// <param name="skipPoints"></param>
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
        [HttpPost("calculate-stress/maxwell-model")]
        public async Task<ActionResult<SkipPointsResponse>> SkipPoints(
            [FromServices] ISkipPoints skipPoints,
            [FromQuery] SkipPointsRequest request)
        {
            SkipPointsResponse response = await skipPoints.Process(request).ConfigureAwait(false);
            return response.BuildHttpResponse();
        }
    }
}