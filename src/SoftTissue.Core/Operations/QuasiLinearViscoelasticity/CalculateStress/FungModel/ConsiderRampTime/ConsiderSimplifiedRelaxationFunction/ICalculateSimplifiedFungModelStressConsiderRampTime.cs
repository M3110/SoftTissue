﻿using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.ConsiderRampTime.ConsiderSimplifiedRelaxationFunction;
using SoftTissue.DataContract.Models;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.ConsiderRampTime.ConsiderSimplifiedRelaxationFunction
{
    /// <summary>
    /// It is responsible to calculate the stress considering the ramp time and the Simplified Reduced Relaxation Function to Fung Model.
    /// </summary>
    public interface ICalculateSimplifiedFungModelStressConsiderRampTime :
        ICalculateQuasiLinearViscoelasticityStress<
            CalculateSimplifiedFungModelStressConsiderRampTimeRequest, 
            CalculateSimplifiedFungModelStressConsiderRampTimeResponse, 
            CalculateSimplifiedFungModelStressConsiderRampTimeResponseData,
            SimplifiedFungModelInput,
            SimplifiedReducedRelaxationFunctionData,
            SimplifiedFungModelResult> 
    { }
}