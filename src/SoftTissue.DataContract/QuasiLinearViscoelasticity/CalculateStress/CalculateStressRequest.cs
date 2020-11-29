using SoftTissue.DataContract.OperationBase;
using SoftTissue.Infrastructure.Models;
using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It represents the request content to CalculateStress operation of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public class CalculateStressRequest : OperationRequestBase<List<CalculateStressRequestData>>
    {
        /// <summary>
        /// The viscoelasctic consideration.
        /// </summary>
        public ViscoelasticConsideration ViscoelasticConsideration { get; set; }
    }
}
