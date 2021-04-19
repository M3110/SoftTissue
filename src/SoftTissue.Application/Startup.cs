using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoftTissue.Application.Extensions;
using SoftTissue.Core.ConstitutiveEquations.LinearModel.Maxwell;
using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung;
using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.SimplifiedFung;
using SoftTissue.Core.NumericalMethods.Derivative;
using SoftTissue.Core.NumericalMethods.Integral.Simpson;
using SoftTissue.Core.Operations.ExperimentalAnalysis.AnalyzeAndExtrapolateResults;
using SoftTissue.Core.Operations.ExperimentalAnalysis.AnalyzeResults;
using SoftTissue.Core.Operations.FileManager.SkipPoints;
using SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrain.MaxwellModel;
using SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrainSensitivityAnalysis.MaxwellModel;
using SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStress.MaxwellModel;
using SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStressSensitivityAnalysis.MaxwellModel;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateConvergenceTime;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.ConsiderRampTime.ConsiderRelaxationFunction;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.ConsiderRampTime.ConsiderSimplifiedRelaxationFunction;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.DisregardRampTime.ConsiderRelaxationFunction;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel.DisregardRampTime.ConsiderSimplifiedRelaxationFunction;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.GenerateDomain.FungModel;

namespace SoftTissue.Application
{
    /// <summary>
    /// The application startup.
    /// It configures the dependecy injection and adds all necessary configuration.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDerivative, Derivative>();

            // Register Constitutive Equations to Viscoelastic Models.
            services.AddScoped<IMaxwellModel, MaxwellModel>();
            services.AddScoped<IFungModel, FungModel>();
            services.AddScoped<ISimplifiedFungModel, SimplifiedFungModel>();

            // Register Operations for Experimental analysis.
            services.AddScoped<IAnalyzeAndExtrapolateResults, AnalyzeAndExtrapolateResults>();
            services.AddScoped<IAnalyzeResults, AnalyzeResults>();
            // Register Operations for FileManager.
            services.AddScoped<ISkipPoints, SkipPoints>();
            // Register Operations for Linear Viscoelastic Model.
            services.AddScoped<ICalculateMaxwellModelStress, CalculateMaxwellModelStress>();
            services.AddScoped<ICalculateMaxwellModelStrainSensitivityAnalysis, CalculateMaxwellModelStrainSensitivityAnalysis>();
            services.AddScoped<ICalculateMaxwellModelStrain, CalculateMaxwellModelStrain>();
            services.AddScoped<ICalculateMaxwellModelStressSensitivityAnalysis, CalculateMaxwellModelStressSensitivityAnalysis>();
            // Register Operations for Quasi-Linear Viscoelastic Model.
            services.AddScoped<ICalculateFungModelStressConsiderRampTime, CalculateFungModelStressConsiderRampTime>();
            services.AddScoped<ICalculateSimplifiedFungModelStressConsiderRampTime, CalculateSimplifiedFungModelStressConsiderRampTime>();
            services.AddScoped<ICalculateFungModelStressDisregardRampTime, CalculateFungModelStressDisregardRampTime>();
            services.AddScoped<ICalculateSimplifiedFungModelStressDisregardRampTime, CalculateSimplifiedFungModelStressDisregardRampTime>();
            services.AddScoped<IGenerateFungModelDomain, GenerateFungModelDomain>();
            services.AddScoped<ICalculateConvergenceTime, CalculateConvergenceTime>();

            // Register Numerical Methods.
            services.AddTransient<ISimpsonRuleIntegration, SimpsonRuleIntegration>();
            services.AddTransient<IDerivative, Derivative>();

            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerDocs();
        }

        /// <summary>
        /// Configures the application dependecies and web hosting environment.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerDocs();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
