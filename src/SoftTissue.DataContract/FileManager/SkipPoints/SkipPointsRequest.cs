﻿using SoftTissue.DataContract.OperationBase;

namespace SoftTissue.DataContract.FileManager.SkipPoints
{
    /// <summary>
    /// It represents the request content to SkipPoints operation.
    /// </summary>
    public sealed class SkipPointsRequest : OperationRequestBase
    {
        /// <summary>
        /// The file URI.
        /// </summary>
        public string FileUri { get; set; }

        /// <summary>
        /// The file name to be used in the operation.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The number of points to be skipped in the file.
        /// </summary>
        public int PointsToSkip { get; set; }
    }
}
