//using SoftTissue.Core.Models.Viscoelasticity;
//using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
//using SoftTissue.Core.Operations.Base.CalculateResultSensitivityAnalysis;
//using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStressSensitivityAnalysis;
//using System.Collections.Generic;
//using System.IO;

//namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStressSensitivityAnalysis
//{
//    /// <summary>
//    /// It is responsible to do a sensitivity analysis while calculating the stress to quasi-linear viscoelastic model.
//    /// </summary>
//    public interface ICalculateQuasiLinearViscoelasticityStressSensitivityAnalysis<TInput, TResult> : ICalculateResultSensitivityAnalysis<CalculateStressSensitivityAnalysisRequest, CalculateStressResponse, CalculateStressResponseData, TInput>
//        where TInput : QuasiLinearViscoelasticityModelInput, new()
//        where TResult : QuasiLinearViscoelasticityModelResult, new()
//    {
//        /// <summary>
//        /// This method writes the input data into a file.
//        /// </summary>
//        /// <param name="inputList"></param>
//        /// <param name="streamWriter"></param>
//        /// <param name="useSimplifiedReducedRelaxationFunction"></param>
//        void WriteInputData(List<TInput> inputList, StreamWriter streamWriter, bool useSimplifiedReducedRelaxationFunction);
//    }
//}
