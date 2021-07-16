using SoftTissue.Core.ConstitutiveEquations.LinearModel.Maxwell;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.Linear.Maxwell;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults.Linear.CalculateStress.Maxwell;
using System.Collections.Generic;
using System.IO;

namespace SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.Linear.CalculateStress.MaxwellModel
{
    /// <summary>
    /// It is responsible to calculate the stress to Maxwell model.
    /// </summary>
    public class CalculateMaxwellModelStress : CalculateLinearModelStress<CalculateMaxwellModelStressRequest, CalculateMaxwellModelStressRequestData, MaxwellModelInput, MaxwellModelResult>, ICalculateMaxwellModelStress
    {
        /// <inheritdoc/>
        protected override string TemplateBasePath => Path.Combine(BasePaths.MaxwellModel, "Stress");
        
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateMaxwellModelStress(IMaxwellModel viscoelasticModel) : base(viscoelasticModel) { }

        /// <summary>
        /// This method builds a list with <see cref="MaxwellModelInput"/> based on <see cref="CalculateMaxwellModelStressRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override List<MaxwellModelInput> BuildInputList(CalculateMaxwellModelStressRequest request)
        {
            var inputList = new List<MaxwellModelInput>();

            foreach (var requestData in request.DataList)
            {
                inputList.Add(new MaxwellModelInput
                {
                    FinalTime = requestData.FinalTime ?? request.FinalTime,
                    TimeStep = requestData.TimeStep ?? request.TimeStep,
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
