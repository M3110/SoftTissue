using SoftTissue.Core.Models.Viscoelasticity;
using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.Core.Operations.Base.CalculateResult;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Request;
using System.IO;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It is responsible to calculate the stress to a quasi-linear viscoelastic model.
    /// </summary>
    public interface ICalculateQuasiLinearViscoelasticityStress<TInput, TResult> : ICalculateResult<CalculateQuasiLinearViscoelasticityStressRequest, CalculateQuasiLinearViscoelasticityStressResponse, CalculateQuasiLinearViscoelasticityStressResponseData, TInput>
        where TInput : QuasiLinearViscoelasticityModelInput, new()
        where TResult : QuasiLinearViscoelasticityModelResult, new()
    {
        /// <summary>
        /// This method writes the input data into a file.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="streamWriter"></param>
        void WriteInput(TInput input, StreamWriter streamWriter);

        /// <summary>
        /// This method calculates the results and writes them to a file.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <param name="streamWriter"></param>
        TResult CalculateAndWriteResults(TInput input, double time, StreamWriter streamWriter);
    }
}
