using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.QuasiLinearViscoelasticity.GenerateDomain;
using System.Collections.Generic;

namespace SoftTissue.Core.Operations.QuasiLinearViscoelasticity.GenerateDomain.FungModel
{
    /// <summary>
    /// It is responsible to generate the domain with valid parameters for Fung Model.
    /// Only the fast and slow relaxation times are considered here, the other parameters (Elastic Tension Constant, Elastic Power Constant and Relaxation Index) have no limitations, being valid for the entire positive real domain.
    /// </summary>
    public interface IGenerateFungModelDomain : IOperationBase<GenerateDomainRequest, GenerateDomainResponse, GenerateDomainResponseData>
    {
        /// <summary>
        /// This method builds the input list.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<GenerateFungModelDomainInput> BuildInputList(GenerateDomainRequest request);

        /// <summary>
        /// This method creates the domain file.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string CreateDomainFile(GenerateDomainRequest request);

        /// <summary>
        /// This method creates the solution file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string CreateSolutionFile(GenerateFungModelDomainInput input);
    }
}
