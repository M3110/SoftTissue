using SoftTissue.Core.Models.Viscoelasticity.Linear;
using SoftTissue.Core.Operations.Base.CalculateResultSensitivityAnalysis;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStress;
using SoftTissue.DataContract.LinearViscoelasticity.CalculateStressSensitivityAnalysis;
using System.Collections.Generic;
using System.IO;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStressSensitivityAnalysis
{
    /// <summary>
    /// It is responsible to calculate the stress to a linear viscoelastic model.
    /// </summary>
    public interface ICalculateLinearViscosityStressSensitivityAnalysis<TInput> : ICalculateResultSensitivityAnalysis<CalculateStressSensitivityAnalysisRequest, CalculateStressResponse, CalculateStressResponseData, TInput>
        where TInput : LinearViscoelasticityModelInput, new()
    {
        /// <summary>
        /// This method writes the input data into a file.
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="streamWriter"></param>
        void WriteInputData(List<TInput> inputList, StreamWriter streamWriter);
    }
}