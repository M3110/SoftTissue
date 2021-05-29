using SoftTissue.DataContract.Models;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResults;
using SoftTissue.DataContract.ViscoelasticModel.CalculateResultsSentivityAnalysis;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.DisregardRampTime.ConsiderSimplifiedRelaxationFunction
{
    /// <summary>
    /// It represents the request content to CalculateStress operation of Quasi-Linear Viscoelasticity Model for a soft tissue experimental model.
    /// </summary>
    public sealed class CalculateSimplifiedFungModelStressDisregardRampTimeToExperimentalModelRequest : CalculateResultsRequest
    {
        /// <summary>
        /// The experimental model.
        /// </summary>
        public ExperimentalModel ExperimentalModel { get; set; }

        /// <summary>
        /// The strain.
        /// Unit: Dimensionless.
        /// </summary>
        public double Strain { get; set; }
    }
}
