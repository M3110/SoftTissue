using SoftTissue.DataContract.CalculateResult;
using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.DisregardRampTime.ConsiderRelaxationFunction
{
    /// <summary>
    /// It represents the request content to CalculateFungModelStressDisregardRampTime operation of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public class CalculateFungModelStressDisregardRampTimeRequest : CalculateResultRequest<List<CalculateFungModelStressDisregardRampTimeRequestData>> { }
}
