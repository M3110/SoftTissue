using System.Collections.Generic;

namespace SoftTissue.Infrastructure.Models
{
    public class SimplifiedReducedRelaxationFunctionData
    {
        public IEnumerable<double> VariableEList { get; set; }

        public IEnumerable<double> RelaxationTimeList { get; set; }
    }
}
