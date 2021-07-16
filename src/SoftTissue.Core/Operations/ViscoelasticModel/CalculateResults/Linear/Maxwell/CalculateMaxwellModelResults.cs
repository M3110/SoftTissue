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

        /// <summary>
        /// This method builds a list with <see cref="MaxwellModelInput"/> based on <see cref="CalculateMaxwellModelResultsRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override List<MaxwellModelInput> BuildInputList(CalculateMaxwellModelResultsRequest request)
        {
            var inputList = new List<MaxwellModelInput>();

            foreach (var requestData in request.DataList)
            {
                inputList.Add(new MaxwellModelInput
                {
                    FinalTime = requestData.FinalTime ?? request.FinalTime,
                    TimeStep = requestData.TimeStep ?? request.TimeStep,
                    InitialStress = requestData.InitialStress,
                    InitialStrain = requestData.InitialStrain,
                    Stiffness = requestData.Stiffness,
                    Viscosity = requestData.Viscosity,
                    SoftTissueType = requestData.SoftTissueType
                });
            }

            return inputList;
        }
    }
}
