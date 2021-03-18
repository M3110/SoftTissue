using SoftTissue.Core.Models.Viscoelasticity.QuasiLinear.Fung;
using SoftTissue.DataContract.Models;

namespace SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung
{
    /// <summary>
    /// It represents the viscoelastic Fung Model.
    /// </summary>
    public interface IFungModel : IQuasiLinearViscoelasticityModel<FungModelInput, FungModelResult, ReducedRelaxationFunctionData>
    {
        /// <summary>
        /// This method calculates the equation I(t) where t is the time.
        /// I(t) = E1(t/tau 2) - E1(t/tau 1)
        /// E1(x) = integral(e^-x/x) from x to infinite.
        /// </summary>
        /// <param name="slowRelaxationTime"></param>
        /// <param name="fastRelaxationTime"></param>
        /// <param name="timeStep"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        double CalculateI(double slowRelaxationTime, double fastRelaxationTime, double timeStep, double time);

        /// <summary>
        /// This method calculates time when the alternative and original reduced relaxation function converge.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        double CalculateConvergenceTimeToReducedRelaxationFunction(FungModelInput input);
    }
}
