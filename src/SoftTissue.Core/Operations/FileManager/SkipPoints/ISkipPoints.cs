using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.FileManager.SkipPoints;

namespace SoftTissue.Core.Operations.FileManager.SkipPoints
{
    /// <summary>
    /// It is responsible to skip points into the file, increasing the time step and generating a new file.
    /// </summary>
    public interface ISkipPoints : IOperationBase<SkipPointsRequest, SkipPointsResponse, SkipPointsResponseData>
    {
        /// <summary>
        /// This method creates the path to save the solution on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string CreateSolutionFile(SkipPointsRequest request);
    }
}
