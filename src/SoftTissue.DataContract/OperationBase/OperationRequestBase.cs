namespace SoftTissue.DataContract.OperationBase
{
    /// <summary>
    /// It represents the essencial request content to operations.
    /// </summary>
    public class OperationRequestBase
    {
        public double InitialTime { get; set; }

        public double TimeStep { get; set; }

        public double FinalTime { get; set; }
    }
}
