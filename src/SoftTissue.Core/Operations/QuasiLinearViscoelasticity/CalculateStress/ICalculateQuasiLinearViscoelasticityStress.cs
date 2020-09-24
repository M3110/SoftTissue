using SoftTissue.Core.Models;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.FungModel;
using System.Collections.Generic;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress
{
    public interface ICalculateQuasiLinearViscoelasticityStress : IOperationBase<CalculateFungModelStressRequest, CalculateFungModelStressResponse, CalculateFungModelStressResponseData> 
    {
        IEnumerable<QuasiLinearViscoelasticityModelInput> BuildInputList(CalculateFungModelStressRequest request);
    }
}
