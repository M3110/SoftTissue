﻿using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStress;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStress.MaxwellModel
{
    /// <summary>
    /// It is responsible to do a semsitivity analysis while calculating the stress to Maxwell model.
    /// </summary>
    public interface ICalculateMaxwellModelStressSensitivityAnalysis : ICalculateLinearViscosityStress<CalculateStressSensitivityAnalysisRequest, MaxwellModelInput> { }
}
