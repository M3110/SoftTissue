﻿using SoftTissue.Core.ConstitutiveEquations.LinearModel.Maxwell;
using SoftTissue.Core.Models;
using System.IO;

namespace SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStress.MaxwellModel
{
    /// <summary>
    /// It is responsible to calculate the stress to Maxwell model.
    /// </summary>
    public class CalculateMaxwellModelStress : CalculateLinearViscosityStress, ICalculateMaxwellModelStress
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="viscoelasticModel"></param>
        public CalculateMaxwellModelStress(IMaxwellModel viscoelasticModel) : base(viscoelasticModel) { }

        /// <summary>
        /// The base path to files.
        /// </summary>
        private static readonly string TemplateBasePath = Path.Combine(Directory.GetCurrentDirectory(), "sheets/Solutions/Linear Viscosity/Maxwell Model/Stress");

        /// <summary>
        /// This method creates the path to save the input data on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override string CreateInputDataFile(LinearViscoelasticityModelInput input)
        {
            var fileInfo = new FileInfo(Path.Combine(
                TemplateBasePath,
                $"InputData_k={input.Stiffness}_v={input.Viscosity}_tau={input.RelaxationTime}_E0={input.InitialStrain}.csv"));

            if (fileInfo.Exists == false || fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }

            return fileInfo.FullName;
        }

        /// <summary>
        /// This method creates the path to save the solution on a file.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override string CreateSolutionFile(LinearViscoelasticityModelInput input)
        {
            var fileInfo = new FileInfo(Path.Combine(
                TemplateBasePath,
                $"Solution_k={input.Stiffness}_v={input.Viscosity}_tau={input.RelaxationTime}_E0={input.InitialStrain}.csv"));

            if (fileInfo.Exists == false || fileInfo.Directory.Exists == false)
            {
                fileInfo.Directory.Create();
            }

            return fileInfo.FullName;
        }
    }
}
