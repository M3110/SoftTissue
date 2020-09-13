using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress
{
    public class CalculateQuasiLinearViscosityStress : OperationBase<CalculateQuasiLinearStressRequest, CalculateQuasiLinearStressResponse, CalculateQuasiLinearStressResponseData>, ICalculateQuasiLinearViscosityStress
    {
        private readonly IFungModel _fungModel;

        public CalculateQuasiLinearViscosityStress(IFungModel fungModel)
        {
            this._fungModel = fungModel;
        }

        protected override Task<CalculateQuasiLinearStressResponse> ProcessOperation(CalculateQuasiLinearStressRequest request)
        {
            throw new System.NotImplementedException();
        }

        protected override Task<CalculateQuasiLinearStressResponse> ValidateOperation(CalculateQuasiLinearStressRequest request)
        {
            return base.ValidateOperation(request);
        }
    }
}
