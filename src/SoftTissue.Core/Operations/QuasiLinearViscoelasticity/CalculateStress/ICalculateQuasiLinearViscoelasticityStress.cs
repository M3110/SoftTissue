using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.Core.Operations.Base.CalculateResult;
using SoftTissue.DataContract.OperationBase;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.FungModel;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It is responsible to calculate the stress to a quasi-linear viscoelastic model.
    /// </summary>
    public interface ICalculateQuasiLinearViscoelasticityStress<TRequest, TInput> : ICalculateResult<TRequest, CalculateFungModelStressResponse, CalculateFungModelStressResponseData, TInput>
        where TRequest : OperationRequestBase
        where TInput : QuasiLinearViscoelasticityModelInput, new()
    { }
}
