using SoftTissue.DataContract.OperationBase;
using System.Collections.Generic;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain
{
    /// <summary>
    /// It represents the request content to CalculateStrain operation of Linear Viscoelasticity Model.
    /// </summary>
    public class CalculateStrainRequest : OperationRequestBase<List<CalculateStrainRequestData>> { }
}
