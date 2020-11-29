using SoftTissue.DataContract.OperationBase;
using System.Collections.Generic;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It represents the request content to CalculateStress operation of Linear Viscoelasticity Model.
    /// </summary>
    public class CalculateStressRequest : OperationRequestBase<List<CalculateStressRequestData>> { }
}
