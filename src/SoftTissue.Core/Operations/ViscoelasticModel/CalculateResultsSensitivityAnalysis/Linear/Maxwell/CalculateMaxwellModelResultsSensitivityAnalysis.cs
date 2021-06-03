using SoftTissue.Core.ConstitutiveEquations.LinearModel.Maxwell;
using SoftTissue.Core.ExtensionMethods;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.Linear.Maxwell;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResultsSensitivityAnalysis.Linear;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSensitivityAnalysis;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSensitivityAnalysis.Linear;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrainSensitivityAnalysis.MaxwellModel
{
    /// <summary>
    /// It is responsible to do a semsitivity analysis while calculating the strain to Maxwell model.
    /// </summary>
    public class CalculateMaxwellModelResultsSensitivityAnalysis : CalculateLinearModelResultsSensitivityAnalysis<CalculateMaxwellModelResultsSensitivityAnalysisRequest, MaxwellModelInput, MaxwellModelResult>, ICalculateMaxwellModelResultsSensitivityAnalysis
    {
        /// <inheritdoc/>
        protected override string TemplateBasePath => Path.Combine(BasePaths.MaxwellModel, "Sensitivity Analysis");

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateMaxwellModelResultsSensitivityAnalysis(IMaxwellModel viscoelasticModel) : base(viscoelasticModel) { }

        /// <summary>
        /// This method builds a list with <see cref="MaxwellModelInput"/> based on <see cref="CalculateMaxwellModelResultsSensitivityAnalysisRequest"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override List<MaxwellModelInput> BuildInputList(CalculateMaxwellModelResultsSensitivityAnalysisRequest request)
        {
            var inputList = new List<MaxwellModelInput>();

            foreach (var initialStress in request.InitialStressRange.ToList())
            {
                foreach (var initialStrain in request.InitialStrainRange.ToList())
                {
                    foreach (var stiffness in request.StiffnessRange.ToList())
                    {
                        foreach (var viscosity in request.ViscosityRange.ToList())
                        {
                            inputList.Add(new MaxwellModelInput
                            {
                                FinalTime = request.FinalTime,
                                TimeStep = request.TimeStep,
                                InitialStress = initialStress,
                                InitialStrain = initialStrain,
                                Stiffness = stiffness,
                                Viscosity = viscosity
                            });
                        }
                    }
                }
            }

            return inputList;
        }

        /// <summary>
        /// Asynchronously, this method validates the request sent to operation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override async Task<CalculateResultsSensitivityAnalysisResponse> ValidateOperationAsync(CalculateMaxwellModelResultsSensitivityAnalysisRequest request)
        {
            var response = await base.ValidateOperationAsync(request).ConfigureAwait(false);
            if (response.Success == false)
            {
                return response;
            }

            response
                .AddErrorIfInvalidRange(request.StiffnessRange, nameof(request.StiffnessRange))
                .AddErrorIfInvalidRange(request.ViscosityRange, nameof(request.ViscosityRange));

            return response;
        }
    }
}
