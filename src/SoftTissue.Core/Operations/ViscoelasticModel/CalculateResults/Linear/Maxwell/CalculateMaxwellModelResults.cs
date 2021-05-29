using SoftTissue.Core.ConstitutiveEquations.LinearModel.Maxwell;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.Linear.Maxwell;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.Maxwell;
using System.Collections.Generic;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.Linear.Maxwell
{
    /// <summary>
    /// It is responsible to calculate the results to a Maxwell model.
    /// </summary>
    public class CalculateMaxwellModelResults : CalculateLinearModelResults<CalculateMaxwellModelResultsRequest, CalculateMaxwellModelResultsRequestData, MaxwellModelInput, MaxwellModelResult>, ICalculateMaxwellModelResults
    {
        /// <inheritdoc/>
        protected override string TemplateBasePath => BasePaths.MaxwellModel;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateMaxwellModelResults(IMaxwellModel viscoelasticModel) : base(viscoelasticModel) { }
    }
}
