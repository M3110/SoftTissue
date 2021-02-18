using CsvHelper;

namespace SoftTissue.Core.ExtensionMethods
{
    /// <summary>
    /// It contains the extension methods to <see cref="CsvWriter"/>.
    /// </summary>
    public static class CsvWriterExtensions
    {
        /// <summary>
        /// This method writes a new line in the file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="csvWriter"></param>
        /// <param name="record"></param>
        public static void WriteLine<T>(this CsvWriter csvWriter, T record)
        {
            csvWriter.WriteRecord(record);
            csvWriter.NextRecord();
        }
    }
}
