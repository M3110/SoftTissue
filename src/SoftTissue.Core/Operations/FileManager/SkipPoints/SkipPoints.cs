using SoftTissue.Core.Operations.Base;
using SoftTissue.DataContract.FileManager.SkipPoints;
using System.IO;
using System.Threading.Tasks;

namespace SoftTissue.Core.Operations.FileManager.SkipPoints
{
    /// <summary>
    /// It is responsible to skip points into the file, increasing the time step and generating a new file.
    /// </summary>
    public class SkipPoints : OperationBase<SkipPointsRequest, SkipPointsResponse, SkipPointsResponseData>, ISkipPoints
    {
        /// <summary>
        /// This method skips points into the file, increasing the time step and generating a new file.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override Task<SkipPointsResponse> ProcessOperation(SkipPointsRequest request)
        {
            var response = new SkipPointsResponse { Data = new SkipPointsResponseData() };

            string solutionFileName = this.CreateSolutionFile(request);

            using (StreamReader streamReader = new StreamReader(Path.Combine(request.FileUri, request.FileName)))
            using (StreamWriter streamWriter = new StreamWriter(Path.Combine(request.FileUri, solutionFileName)))
            {
                // Read the file header and first line and writes them into the solution file.
                string header = streamReader.ReadLine();
                string firstLine = streamReader.ReadLine();
                streamWriter.WriteLine(header);
                streamWriter.WriteLine(firstLine);

                int count = 1;
                while (streamReader.EndOfStream == false)
                {
                    string line = streamReader.ReadLine();

                    if (count == request.PointsToSkip)
                    {
                        streamWriter.WriteLine(line);

                        count = 0;
                    }

                    count += 1;
                }
            }

            // Maps to response.
            response.Data.FileUri = request.FileUri;
            response.Data.FileName = solutionFileName;

            return Task.FromResult(response);
        }

        /// <summary>
        /// This method creates the path to save the solution on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string CreateSolutionFile(SkipPointsRequest request)
        {
            string extension = Path.GetExtension(request.FileName);

            return $"{Path.GetFileNameWithoutExtension(request.FileName)}_simplified{extension}";
        }
    }
}
