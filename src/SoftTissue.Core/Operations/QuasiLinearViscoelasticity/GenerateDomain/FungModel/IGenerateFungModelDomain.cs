using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.GenerateDomain;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.GenerateDomain.FungModel
{
    public interface IGenerateFungModelDomain : IOperationBase<GenerateDomainRequest, GenerateDomainResponse, GenerateDomainResponseData>
    {
    }
}
