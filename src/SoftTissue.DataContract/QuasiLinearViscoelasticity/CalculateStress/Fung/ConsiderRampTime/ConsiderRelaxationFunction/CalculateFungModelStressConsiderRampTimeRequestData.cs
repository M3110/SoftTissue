using Newtonsoft.Json;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.ConsiderRampTime.ConsiderRelaxationFunction
{
    /// <summary>
    /// It represents the 'data' content to CalculateFungModelStressConsiderRampTime operation request of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public sealed class CalculateFungModelStressConsiderRampTimeRequestData : CalculateStressConsiderRampTimeRequestData<ReducedRelaxationFunctionData> 
    {
        // Para tornar 
        [JsonConstructor]
        public CalculateFungModelStressConsiderRampTimeRequestData(int numerOfRelaxations)
        {
            NumerOfRelaxations = numerOfRelaxations;
        }
    }
}
