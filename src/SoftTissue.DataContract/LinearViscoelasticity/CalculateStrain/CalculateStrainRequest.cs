using SoftTissue.DataContract.CalculateResult;
using System.Collections.Generic;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain
{
    /// <summary>
    /// It represents the request content to CalculateStrain operation of Linear Viscoelasticity Model.
    /// </summary>
    public sealed class CalculateStrainRequest : CalculateResultRequest<List<CalculateStrainRequestData>> { }
}
