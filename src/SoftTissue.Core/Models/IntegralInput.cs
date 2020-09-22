namespace SoftTissue.Core.Models
{
    public class IntegralInput
    {
        public double InitialPoint { get; set; }
        
        public double? FinalPoint { get; set; } 
        
        public double Step { get; set; }

        public double Precision { get; set; }
    }
}
