using SoftTissue.DataContract.CalculateResult;
using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.ConsiderRampTime.ConsiderRelaxationFunction
{
    /// <summary>
    /// It represents the request content to CalculateFungModelStressConsiderRampTime operation of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public class CalculateFungModelStressConsiderRampTimeRequest : CalculateResultRequest<List<CalculateFungModelStressConsiderRampTimeRequestData>> { }
}
