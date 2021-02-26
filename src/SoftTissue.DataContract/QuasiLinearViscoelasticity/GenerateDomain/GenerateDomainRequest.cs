using Newtonsoft.Json;
using SoftTissue.DataContract.OperationBase;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.GenerateDomain
{
    /// <summary>
    /// It represents the request content to GenerateDomain operation of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public class GenerateDomainRequest : OperationRequestBase
    {
        // TODO: Ver se tem atalho para construir construtor com todos os campos.

        /// <summary>
        /// Time step.
        /// Unit: s (second).
        /// </summary>
        public double TimeStep { get; }

        [JsonConstructor]
        public GenerateDomainRequest(double timeStep, double finalTime, Value fastRelaxationTimeList, Value slowRelaxationTimeList)
        {
            TimeStep = timeStep;
            FinalTime = finalTime;
            FastRelaxationTimeList = fastRelaxationTimeList;
            SlowRelaxationTimeList = slowRelaxationTimeList;
        }

        /// <summary>
        /// Final time.
        /// Unit: s (second).
        /// </summary>
        public double FinalTime { get; }

        /// <summary>
        /// Tau 1.
        /// List of fast relaxation time.
        /// </summary>
        public Value FastRelaxationTimeList { get; }

        /// <summary>
        /// Tau 2.
        /// List of slow relaxation time.
        /// </summary>
        public Value SlowRelaxationTimeList { get; }
    }
}
