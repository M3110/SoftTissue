using Newtonsoft.Json;
using SoftTissue.DataContract.CalculateResult;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStress.Fung.DisregardRampTime.ConsiderSimplifiedRelaxationFunction
{
    /// <summary>
    /// It represents the request content to CalculateStress operation of Quasi-Linear Viscoelasticity Model for a soft tissue experimental model.
    /// </summary>
    public sealed class CalculateSimplifiedFungModelStressDisregardRampTimeToExperimentalModelRequest : CalculateResultRequest
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="experimentalModel"></param>
        /// <param name="strain"></param>
        [JsonConstructor]
        public CalculateSimplifiedFungModelStressDisregardRampTimeToExperimentalModelRequest(
            double timeStep, 
            double finalTime,
            ExperimentalModel experimentalModel,
            double strain) : base(timeStep, finalTime)
        {
            this.ExperimentalModel = experimentalModel;
            this.Strain = strain;
        }

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
