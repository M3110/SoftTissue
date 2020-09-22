namespace SoftTissue.DataContract
{
    /// <summary>
    /// It represents the essencial request content to operations.
    /// </summary>
    public class OperationRequestBase
    {
        public double InitialTime => 0;

        public double TimeStep { get; set; }
        
        public double FinalTime { get; set; }
    }
}
