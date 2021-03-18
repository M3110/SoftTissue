using Newtonsoft.Json;
using SoftTissue.DataContract.OperationBase;
using SoftTissue.DataContract.Models;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateConvergenceTime
{
    /// <summary>
    /// It represents the request content to CalculateConvergenceTime operation.
    /// </summary>
    public sealed class CalculateConvergenceTimeRequest : OperationRequestBase
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="viscoelasticConsideration"></param>
        /// <param name="strainRate"></param>
        /// <param name="maximumStrain"></param>
        /// <param name="elasticStressConstant"></param>
        /// <param name="elasticPowerConstant"></param>
        /// <param name="relaxationIndex"></param>
        /// <param name="fastRelaxationTime"></param>
        /// <param name="slowRelaxationTime"></param>
        [JsonConstructor]
        public CalculateConvergenceTimeRequest(
            double timeStep, 
            double finalTime, 
            ViscoelasticConsideration viscoelasticConsideration, 
            double strainRate, 
            double maximumStrain, 
            double elasticStressConstant, 
            double elasticPowerConstant, 
            double relaxationIndex, 
            double fastRelaxationTime, 
            double slowRelaxationTime)
        {
            this.TimeStep = timeStep;
            this.FinalTime = finalTime;
            this.ViscoelasticConsideration = viscoelasticConsideration;
            this.StrainRate = strainRate;
            this.MaximumStrain = maximumStrain;
            this.ElasticStressConstant = elasticStressConstant;
            this.ElasticPowerConstant = elasticPowerConstant;
            this.RelaxationIndex = relaxationIndex;
            this.FastRelaxationTime = fastRelaxationTime;
            this.SlowRelaxationTime = slowRelaxationTime;
        }

        /// <summary>
        /// Time step.
        /// Unit: s (second).
        /// </summary>
        public double TimeStep { get; private set; }

        /// <summary>
        /// Final time.
        /// Unit: s (second).
        /// </summary>
        public double FinalTime { get; private set; }

        /// <summary>
        /// The viscoelasctic consideration.
        /// </summary>
        public ViscoelasticConsideration ViscoelasticConsideration { get; private set; }

        /// <summary>
        /// The analysis strain rate.
        /// Unit: Dimensionless.
        /// </summary>
        public double StrainRate { get; private set; }

        /// <summary>
        /// The maximum strain, this is obtained after the ramp time.
        /// Unit: Dimensionless.
        /// </summary>
        public double MaximumStrain { get; private set; }

        /// <summary>
        /// The elastic stress constant. Constant A.
        /// Unit: Pa (Pascal). In some cases, can be N (Newton).
        /// </summary>
        public double ElasticStressConstant { get; private set; }

        /// <summary>
        /// The elastic power constant. Constant B.
        /// Unit: Dimensionless.
        /// </summary>
        public double ElasticPowerConstant { get; private set; }

        /// <summary>
        /// The relaxation index. Constant C.
        /// Unit: Dimensionless.
        /// </summary>
        public double RelaxationIndex { get; private set; }

        /// <summary>
        /// The fast relaxation time. Tau 1.
        /// Unit: s (second).
        /// </summary>
        public double FastRelaxationTime { get; private set; }

        /// <summary>
        /// The slow relaxation time. Tau 2.
        /// Unit: s (second).
        /// </summary>
        public double SlowRelaxationTime { get; private set; }
    }
}
