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
using SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrainSensitivityAnalysis.MaxwellModel;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateConvergenceTime;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.GenerateDomain.FungModel;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.Linear.CalculateStrain.MaxwellModel;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.Linear.CalculateStress.MaxwellModel;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.Fung;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.ConsiderRampTime.SimplifiedFung;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.Fung;
using SoftTissue.Core.Operations.ViscoelasticModel.CalculateResults.QuasiLinear.DisregardRampTime.SimplifiedFung;

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
            services.AddScoped<ICalculateMaxwellModelStrain, CalculateMaxwellModelStrain>();
            services.AddScoped<ICalculateMaxwellModelResultsSensitivityAnalysis, CalculateMaxwellModelResultsSensitivityAnalysis>();
            // Register Operations for Quasi-Linear Viscoelastic Model.
            services.AddScoped<ICalculateFungModelResultsConsiderRampTime, CalculateFungModelResultsConsiderRampTime>();
            services.AddScoped<ICalculateFungModelResultsDisregardRampTime, CalculateFungModelResultsDisregardRampTime>();
            services.AddScoped<ICalculateSimplifiedFungModelResultsConsiderRampTime, CalculateSimplifiedFungModelResultsConsiderRampTime>();
            services.AddScoped<ICalculateSimplifiedFungModelResultsDisregardRampTime, CalculateSimplifiedFungModelResultsDisregardRampTime>();
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
