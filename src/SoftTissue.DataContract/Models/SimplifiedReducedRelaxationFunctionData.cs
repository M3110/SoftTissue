using System;
using System.Collections.Generic;

namespace SoftTissue.DataContract.Models
{
    /// <summary>
    /// It contains the input data to Simplified Reduced Relaxation Function.
    /// </summary>
    public class SimplifiedReducedRelaxationFunctionData
    {
        /// <summary>
        /// The first viscoelastic stiffness. This variable is independent.
        /// </summary>
        public double FirstViscoelasticStiffness { get; set; }

        /// <summary>
        /// The values for each iteration to Simplified Reduced Relaxation Function.
        /// </summary>
        public IEnumerable<SimplifiedReducedRelaxationFunctionIteratorValue> IteratorValues { get; set; }

        /// <summary>
        /// This method creates a new instance of <see cref="SimplifiedReducedRelaxationFunctionData"/>.
        /// </summary>
        /// <param name="experimentalModel"></param>
        /// <returns></returns>
        public static SimplifiedReducedRelaxationFunctionData Create(ExperimentalModel experimentalModel)
        {
            double relaxationModulus;
            double firstRelaxationModulus;
            double secondRelaxationModulus;
            double thirdRelaxationModulus;
            double firstRelaxationTime;
            double secondRelaxationTime;
            double thirdRelaxationTime;
            double stress;

            switch (experimentalModel)
            {
                case ExperimentalModel.AnteriorCruciateLigamentFirstRelaxation:
                    relaxationModulus = 1.81;
                    firstRelaxationModulus = 0.27;
                    secondRelaxationModulus = 0.18;
                    thirdRelaxationModulus = 0.56;
                    firstRelaxationTime = 19.33;
                    secondRelaxationTime = 19.6;
                    thirdRelaxationTime = 370.8;
                    stress = relaxationModulus + firstRelaxationModulus + secondRelaxationModulus + thirdRelaxationModulus;
                    break;

                case ExperimentalModel.AnteriorCruciateLigamentSecondRelaxation:
                    relaxationModulus = 1.64;
                    firstRelaxationModulus = 0.063;
                    secondRelaxationModulus = 0.098;
                    thirdRelaxationModulus = 0.24;
                    firstRelaxationTime = 2.35;
                    secondRelaxationTime = 19.86;
                    thirdRelaxationTime = 213.86;
                    stress = relaxationModulus + firstRelaxationModulus + secondRelaxationModulus + thirdRelaxationModulus;
                    break;

                case ExperimentalModel.PosteriorCruciateLigamentFirstRelaxation:
                    relaxationModulus = 1.43;
                    firstRelaxationModulus = 0.03;
                    secondRelaxationModulus = 0.05;
                    thirdRelaxationModulus = 0.11;
                    firstRelaxationTime = 0.97;
                    secondRelaxationTime = 6.92;
                    thirdRelaxationTime = 53.18;
                    stress = relaxationModulus + firstRelaxationModulus + secondRelaxationModulus + thirdRelaxationModulus;
                    break;

                case ExperimentalModel.PosteriorCruciateLigamentSecondRelaxation:
                    relaxationModulus = 1.387;
                    firstRelaxationModulus = 0.028;
                    secondRelaxationModulus = 0.046;
                    thirdRelaxationModulus = 0.077;
                    firstRelaxationTime = 1.67;
                    secondRelaxationTime = 13.91;
                    thirdRelaxationTime = 134.91;
                    stress = relaxationModulus + firstRelaxationModulus + secondRelaxationModulus + thirdRelaxationModulus;
                    break;

                case ExperimentalModel.LateralCollateralLigamentFirstRelaxation:
                    relaxationModulus = 3.79;
                    firstRelaxationModulus = 0.1;
                    secondRelaxationModulus = 0.18;
                    thirdRelaxationModulus = 0.32;
                    firstRelaxationTime = 1.02;
                    secondRelaxationTime = 7.52;
                    thirdRelaxationTime = 53.38;
                    stress = relaxationModulus + firstRelaxationModulus + secondRelaxationModulus + thirdRelaxationModulus;
                    break;

                case ExperimentalModel.LateralCollateralLigamentSecondRelaxation:
                    relaxationModulus = 3.55;
                    firstRelaxationModulus = 0.11;
                    secondRelaxationModulus = 0.17;
                    thirdRelaxationModulus = 0.24;
                    firstRelaxationTime = 1.88;
                    secondRelaxationTime = 15.63;
                    thirdRelaxationTime = 150;
                    stress = relaxationModulus + firstRelaxationModulus + secondRelaxationModulus + thirdRelaxationModulus;
                    break;

                case ExperimentalModel.MedialCollateralLigamentFirstRelaxation:
                    relaxationModulus = 0.86;
                    firstRelaxationModulus = 0.018;
                    secondRelaxationModulus = 0.032;
                    thirdRelaxationModulus = 0.059;
                    firstRelaxationTime = 0.59;
                    secondRelaxationTime = 4.73;
                    thirdRelaxationTime = 32.29;
                    stress = relaxationModulus + firstRelaxationModulus + secondRelaxationModulus + thirdRelaxationModulus;
                    break;

                case ExperimentalModel.MedialCollateralLigamentSecondRelaxation:
                    relaxationModulus = 0.81;
                    firstRelaxationModulus = 0.02;
                    secondRelaxationModulus = 0.03;
                    thirdRelaxationModulus = 0.04;
                    firstRelaxationTime = 0.96;
                    secondRelaxationTime = 9.38;
                    thirdRelaxationTime = 95.36;
                    stress = relaxationModulus + firstRelaxationModulus + secondRelaxationModulus + thirdRelaxationModulus;
                    break;

                default:
                    throw new Exception($"An invalid experimental model was passed.");
            }

            return new SimplifiedReducedRelaxationFunctionData
            {
                FirstViscoelasticStiffness = relaxationModulus / stress,
                IteratorValues = new List<SimplifiedReducedRelaxationFunctionIteratorValue>
                {
                    new SimplifiedReducedRelaxationFunctionIteratorValue
                    {
                        RelaxationTime = firstRelaxationTime,
                        ViscoelasticStiffness = firstRelaxationModulus / stress
                    },
                    new SimplifiedReducedRelaxationFunctionIteratorValue
                    {
                        RelaxationTime = secondRelaxationTime,
                        ViscoelasticStiffness = secondRelaxationModulus / stress
                    },
                    new SimplifiedReducedRelaxationFunctionIteratorValue
                    {
                        RelaxationTime = thirdRelaxationTime,
                        ViscoelasticStiffness = thirdRelaxationModulus / stress
                    },
                }
            };
        }
    }
}
