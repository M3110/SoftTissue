using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.Core.Operations.Base.CalculateResultSensitivityAnalysis;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStrain;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSentivityAnalysis.Linear;
using System.Collections.Generic;
using System.IO;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrainSensitivityAnalysis
{
    /// <summary>
    /// It is responsible to calculate the strain to a linear viscoelastic model.
    /// </summary>
    public interface ICalculateLinearViscosityStrainSensitivityAnalysis<TInput> : ICalculateResultSensitivityAnalysis<CalculateMaxwellModelResultsSensitivityAnalysisRequest, CalculateStrainResponse, CalculateStrainResponseData, TInput>
        where TInput : LinearModelInput, new()
    {
        /// <summary>
        /// This method writes the input data into a file.
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="streamWriter"></param>
        void WriteInputData(List<TInput> inputList, StreamWriter streamWriter);
    }
}