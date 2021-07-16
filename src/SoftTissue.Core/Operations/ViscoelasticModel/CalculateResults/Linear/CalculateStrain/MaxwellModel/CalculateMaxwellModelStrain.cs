using SoftTissue.Core.ConstitutiveEquations.LinearModel.Maxwell;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.Linear.Maxwell;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.CalculateStrain.Maxwell;
using System.Collections.Generic;
using System.IO;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.Linear.CalculateStrain.MaxwellModel
{
    /// <summary>
    /// It is responsible to calculate the strain to Maxwell model.
    /// </summary>
    public class CalculateMaxwellModelStrain : CalculateLinearModelStrain<CalculateMaxwellModelStrainRequest, CalculateMaxwellModelStrainRequestData, MaxwellModelInput, MaxwellModelResult>, ICalculateMaxwellModelStrain
    {
        /// <inheritdoc/>
        protected override string TemplateBasePath => Path.Combine(BasePaths.MaxwellModel, "Strain");

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateMaxwellModelStrain(IMaxwellModel viscoelasticModel) : base(viscoelasticModel) { }

        /// <summary>
        /// This method builds a list with <see cref="MaxwellModelInput"/> based on <see cref="CalculateMaxwellModelStrainRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override List<MaxwellModelInput> BuildInputList(CalculateMaxwellModelStrainRequest request)
        {
            var inputList = new List<MaxwellModelInput>();

            foreach (var requestData in request.DataList)
            {
                inputList.Add(new MaxwellModelInput
                {
                    FinalTime = requestData.FinalTime ?? request.FinalTime,
                    TimeStep = requestData.TimeStep ?? request.TimeStep,
                    InitialStress = requestData.InitialStress,
                    Stiffness = requestData.Stiffness,
                    Viscosity = requestData.Viscosity,
                    SoftTissueType = requestData.SoftTissueType
                });
            }

            return inputList;
        }
    }
}
