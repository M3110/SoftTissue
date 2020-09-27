namespace SoftTissue.Core.Models
{
    /// <summary>
    /// It contains the input data to a generic Viscoelastic Model.
    /// </summary>
    public class ViscoelasticModelInput
    {
        public string AnalysisType { get; set; }

        public double InitialTime { get; set; }

        public double TimeStep { get; set; }

        public double FinalTime { get; set; }
    }
}
