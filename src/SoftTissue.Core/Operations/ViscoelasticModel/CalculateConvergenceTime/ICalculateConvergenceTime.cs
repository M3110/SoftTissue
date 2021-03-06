﻿using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateConvergenceTime;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateConvergenceTime
{
    public interface ICalculateConvergenceTime : IOperationBase<CalculateConvergenceTimeRequest, CalculateConvergenceTimeResponse, CalculateConvergenceTimeResponseData>
    {
    }
}
