﻿using SoftTissue.DataContract;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.FungModel;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It is responsible to calculate the stress to a quasi-linear viscoelastic model.
    /// </summary>
    public interface ICalculateQuasiLinearViscoelasticityStress<TRequest> : IOperationBase<TRequest, CalculateFungModelStressResponse, CalculateFungModelStressResponseData> 
        where TRequest : OperationRequestBase
    { }
}
