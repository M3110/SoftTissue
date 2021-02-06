﻿using SoftTissue.DataContract.CalculateResult;
using SoftTissue.Infrastructure.Models;

namespace SoftTissue.DataContract.LinearViscoelasticity.CalculateStrainSensitivityAnalysis
{
    /// <summary>
    /// It represents the request content to CalculateStrainSensitivityAnalysis operation of Linear Viscoelasticity Model.
    /// </summary>
    public class CalculateStrainSensitivityAnalysisRequest : CalculateResultRequest
    {
        /// <summary>
        /// List of stiffness.
        /// Unity: Pa (Pascal).
        /// </summary>
        public Value StiffnessList { get; set; }

        /// <summary>
        /// List of initial stress.
        /// Unity: Pa (Pascal).
        /// </summary>
        public Value InitialStressList { get; set; }

        /// <summary>
        /// List of viscosity.
        /// Unit: N.s/m (Newton-second per meter).
        /// </summary>
        public Value ViscosityList { get; set; }
    }
}
