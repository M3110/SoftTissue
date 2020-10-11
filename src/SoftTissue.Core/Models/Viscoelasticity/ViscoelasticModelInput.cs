namespace SoftTissue.Core.Models.Viscoelasticity
{
    /// <summary>
    /// It contains the input data to a generic Viscoelastic Model.
    /// </summary>
    public class ViscoelasticModelInput
    {
        public string SoftTissueType { get; set; }

        public double InitialTime { get; set; }

        public double TimeStep { get; set; }

        public double FinalTime { get; set; }

        public double Index { get; set; }
    }
}
