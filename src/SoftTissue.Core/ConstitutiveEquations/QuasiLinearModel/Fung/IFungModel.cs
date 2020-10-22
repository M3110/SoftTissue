using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear;
using static SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung.FungModel;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung
{
    public interface IFungModel : IQuasiLinearViscoelasticityModel<FungModelInput, FungModelResult>
    {
        /// <summary>
        /// This method calculates the equation I(t) where t is the time.
        /// I(t) = E1(t/tau 2) - E1(t/tau 1)
        /// E1(x) = integral(e^-x/x) from x to infinite.
        /// </summary>
        /// <param name="slowRelaxationTime"></param>
        /// <param name="fastRelaxationTime"></param>
        /// <param name="stepTime"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        double CalculateI(double slowRelaxationTime, double fastRelaxationTime, double stepTime, double time);

        /// <summary>
        /// This method calculates time when the alternative and original reduced relaxation function converge.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        double CalculateConvergenceTimeToReducedRelaxationFunction(FungModelInput input);

        /// <summary>
        /// This method calculates the stress using the equation 8.a from Fung, at page 279.
        /// That equation do not returns a satisfactory result.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        double CalculateStressByReducedRelaxationFunctionDerivative(FungModelInput input, double time);

        /// <summary>
        /// This method calculates the stress using the equation 8.b from Fung, at page 279.
        /// That equation do not returns a satisfactory result.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        double CalculateStressByIntegrationDerivative(FungModelInput input, double time);

        /// <summary>
        /// This method sets the correct Reduced Relaxation Function to be used.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FungModelEquation SetReducedRelaxationFunction(FungModelInput input);
    }
}
