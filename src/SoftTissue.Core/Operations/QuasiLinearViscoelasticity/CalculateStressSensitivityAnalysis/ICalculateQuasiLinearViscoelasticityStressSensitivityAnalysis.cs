using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.Core.Operations.Base.CalculateResult;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Request;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStressSensitivityAnalysis
{
    /// <summary>
    /// It is responsible to do a sensitivity analysis while calculating the stress to quasi-linear viscoelastic model.
    /// </summary>
    public interface ICalculateQuasiLinearViscoelasticityStressSensitivityAnalysis<TInput, TResult> : ICalculateResultSensitivityAnalysis<CalculateQuasiLinearViscoelasticityStressSensitivityAnalysisRequest, CalculateQuasiLinearViscoelasticityStressResponse, CalculateQuasiLinearViscoelasticityStressResponseData, TInput>
        where TInput : QuasiLinearViscoelasticityModelInput, new()
        where TResult : QuasiLinearViscoelasticityModelResult, new()
    {
        /// <summary>
        /// This method writes the input data into a file.
        /// </summary>
        /// <param name="inputList"></param>
        /// <param name="streamWriter"></param>
        /// <param name="useSimplifiedReducedRelaxationFunction"></param>
        void WriteInputData(List<TInput> inputList, StreamWriter streamWriter, bool useSimplifiedReducedRelaxationFunction);
    }
}
