﻿using SoftTissue.DataContract.Models;

namespace SoftTissue.DataContract.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.Fung
{
    /// <summary>
    /// It represents the request content to CalculateFungModelResultsConsiderRampTime operation.
    /// </summary>
    public sealed class CalculateFungModelResultsConsiderRampTimeRequest : CalculateQuasiLinearModelResultsConsiderRampTimeRequest<CalculateFungModelResultsConsiderRampTimeRequestData, ReducedRelaxationFunctionData> { }
}
