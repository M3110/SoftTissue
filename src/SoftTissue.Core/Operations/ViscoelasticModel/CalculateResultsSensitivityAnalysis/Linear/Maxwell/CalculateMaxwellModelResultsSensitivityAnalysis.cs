using SoftTissue.Core.ConstitutiveEquations.LinearModel.Maxwell;
using SoftTissue.Core.ExtensionMethods;
using SoftTissue.Core.Models;
using SoftTissue.Core.Models.Viscoelasticity.Linear.Maxwell;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResultsSensitivityAnalysis.Linear;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSensitivityAnalysis.Linear;
using System.Collections.Generic;
using System.IO;

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

            foreach (var initialStress in request.InitialStressRange.ToEnumerable())
            {
                foreach (var initialStrain in request.InitialStrainRange.ToEnumerable())
                {
                    foreach (var stiffness in request.StiffnessRange.ToEnumerable())
                    {
                        foreach (var viscosity in request.ViscosityRange.ToEnumerable())
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
    }
}
