using System;
using System.Collections.Generic;

namespace SoftTissue.Infrastructure.Models
{
    public class SimplifiedReducedRelaxationFunctionData
    {
        public int NumberOfVariables => this.VariableEList.Count == this.RelaxationTimeList.Count 
            ? this.VariableEList.Count
            : throw new Exception($"Number of variables E: {this.VariableEList.Count} must be equals to number of relaxation times: {this.RelaxationTimeList.Count}.");

        public double IndependentVariable { get; set; }

        public List<double> VariableEList { get; set; }

        public List<double> RelaxationTimeList { get; set; }
    }
}
