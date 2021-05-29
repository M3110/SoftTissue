using SoftTissue.Core.Models.Viscoelasticity.Linear.Maxwell;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.Maxwell;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.Linear.Maxwell
{
    /// <summary>
    /// It is responsible to calculate the results to a Maxwell model.
    /// </summary>
    public interface ICalculateMaxwellModelResults : ICalculateLinearModelResults<CalculateMaxwellModelResultsRequest, CalculateMaxwellModelResultsRequestData, MaxwellModelInput, MaxwellModelResult> { }
}