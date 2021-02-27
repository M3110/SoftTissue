using Newtonsoft.Json;
using SoftTissue.DataContract.CalculateResult;
using SoftTissue.Infrastructure.Models;
using System.Collections.Generic;

namespace SoftTissue.DataContract.QuasiLinearViscoelasticity.CalculateStressSensitivityAnalysis
{
    /// <summary>
    /// It represents the request content to CalculateStressSensitivityAnalysisExplicit operation of Quasi-Linear Viscoelasticity Model.
    /// </summary>
    public sealed class CalculateStressSensitivityAnalysisExplicitRequest : CalculateResultRequest
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="timeStep"></param>
        /// <param name="finalTime"></param>
        /// <param name="useSimplifiedReducedRelaxationFunction"></param>
        /// <param name="viscoelasticConsideration"></param>
        /// <param name="strainRateList"></param>
        /// <param name="maximumStrainList"></param>
        /// <param name="elasticStressConstantList"></param>
        /// <param name="elasticPowerConstantList"></param>
        /// <param name="relaxationIndexList"></param>
        /// <param name="fastRelaxationTimeList"></param>
        /// <param name="slowRelaxationTimeList"></param>
        /// <param name="simplifiedReducedRelaxationFunctionDataList"></param>
        [JsonConstructor]
        public CalculateStressSensitivityAnalysisExplicitRequest(
            double timeStep, 
            double finalTime, 
            bool useSimplifiedReducedRelaxationFunction, 
            ViscoelasticConsideration viscoelasticConsideration, 
            Value strainRateList, 
            Value maximumStrainList, 
            Value elasticStressConstantList, 
            Value elasticPowerConstantList, 
            Value relaxationIndexList, 
            Value fastRelaxationTimeList, 
            Value slowRelaxationTimeList, 
            IEnumerable<SimplifiedReducedRelaxationFunctionData> simplifiedReducedRelaxationFunctionDataList) 
            : base(timeStep, finalTime)
        {
            this.UseSimplifiedReducedRelaxationFunction = useSimplifiedReducedRelaxationFunction;
            this.ViscoelasticConsideration = viscoelasticConsideration;
            this.StrainRateList = strainRateList;
            this.MaximumStrainList = maximumStrainList;
            this.ElasticStressConstantList = elasticStressConstantList;
            this.ElasticPowerConstantList = elasticPowerConstantList;
            this.RelaxationIndexList = relaxationIndexList;
            this.FastRelaxationTimeList = fastRelaxationTimeList;
            this.SlowRelaxationTimeList = slowRelaxationTimeList;
            this.SimplifiedReducedRelaxationFunctionDataList = simplifiedReducedRelaxationFunctionDataList;
        }

        /// <summary>
        /// True, if have to use the simplified Reduced Relaxation Function.
        /// False, otherwise.
        /// </summary>
        public bool UseSimplifiedReducedRelaxationFunction { get; private set; }

        /// <summary>
        /// The viscoelasctic consideration.
        /// </summary>
        public ViscoelasticConsideration ViscoelasticConsideration { get; private set; }

        public Value StrainRateList { get; private set; }

        public Value MaximumStrainList { get; private set; }

        public Value ElasticStressConstantList { get; private set; }

        public Value ElasticPowerConstantList { get; private set; }

        /// <summary>
        /// Constant C.
        /// </summary>
        public Value RelaxationIndexList { get; private set; }

        /// <summary>
        /// Tau 1.
        /// </summary>
        public Value FastRelaxationTimeList { get; private set; }

        /// <summary>
        /// Tau 2.
        /// </summary>
        public Value SlowRelaxationTimeList { get; private set; }

        public IEnumerable<SimplifiedReducedRelaxationFunctionData> SimplifiedReducedRelaxationFunctionDataList { get; private set; }
    }
}
