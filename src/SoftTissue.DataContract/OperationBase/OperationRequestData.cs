namespace SoftTissue.DataContract.OperationBase
{
    public class OperationRequestData
    {
        public string SoftTissueType { get; set; }

        public double? InitialTime { get; set; }

        public double? TimeStep { get; set; }

        public double? FinalTime { get; set; }
    }
}
