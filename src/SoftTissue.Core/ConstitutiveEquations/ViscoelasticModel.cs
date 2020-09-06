﻿using SoftTissue.Core.Models;
using System.Threading.Tasks;

namespace SoftTissue.Core.ConstitutiveEquations
{
    /// <summary>
    /// It represents a generic viscoelastic model.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    public abstract class ViscoelasticModel<TInput> : IViscoelasticModel<TInput>
        where TInput : ViscoelasticModelInput, new()
    {
        /// <summary>
        /// This method calculates the reduced relaxation function for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract Task<double> CalculateReducedRelaxationFunction(TInput input);

        /// <summary>
        /// This method calculates the strain for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract Task<double> CalculateStrain(TInput input);

        /// <summary>
        /// This method calculates the stress for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract Task<double> CalculateStress(TInput input);

        /// <summary>
        /// This method calculates the Creep Compliance J(t) for a specific time.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract Task<double> CalculateCreepCompliance(TInput input);
    }
}
