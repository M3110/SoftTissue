using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.Core.Operations.Base.CalculateResult;
using SoftTissue.DataContract.CalculateResult;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress
{
    /// <summary>
    /// It is responsible to calculate the stress to a quasi-linear viscoelastic model.
    /// </summary>
    public interface ICalculateQuasiLinearViscoelasticityStress<TRequest, TResponse, TResponseData, TInput, TReducedRelaxation, TResult> : ICalculateResult<TRequest, TResponse, TResponseData, TInput>
        where TRequest : CalculateResultRequest
        where TResponse : CalculateResultResponse<TResponseData>, new()
        where TResponseData : CalculateResultResponseData, new()
        where TInput : QuasiLinearViscoelasticityModelInput<TReducedRelaxation>, new()
        where TResult : QuasiLinearViscoelasticityModelResult, new()
    {
        /// <summary>
        /// This method writes the input data into a file.
        /// </summary>
        /// <param name="input"></param>
        void WriteInput(TInput input);

        /// <summary>
        /// This method calculates the results and writes them to a file.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <param name="streamWriter"></param>
        Task<TResult> CalculateAndWriteResults(TInput input, double time, StreamWriter streamWriter);
    }
}
