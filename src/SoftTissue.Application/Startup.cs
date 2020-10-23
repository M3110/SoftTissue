using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoftTissue.Application.Extensions;
using SoftTissue.Core.ConstitutiveEquations.LinearModel.Maxwell;
using SoftTissue.Core.ConstitutiveEquations.QuasiLinearModel.Fung;
using SoftTissue.Core.NumericalMethods.Derivative;
using SoftTissue.Core.NumericalMethods.Integral.Simpson;
using SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStrain.MaxwellModel;
using SoftTissue.Core.Operations.LinearViscoelasticity.CalculateStress.MaxwellModel;
using SoftTissue.Core.Operations.QuasiLinearViscoelasticity.CalculateStress.FungModel;
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

            // Register Constitutive Equations to Viscoelastic Models
            services.AddScoped<IMaxwellModel, MaxwellModel>();
            services.AddScoped<IFungModel, FungModel>();

            // Register Operations
            services.AddScoped<ICalculateMaxwellModelStress, CalculateMaxwellModelStress>();
            services.AddScoped<ICalculateMaxwellModelStrainSensitivityAnalysis, CalculateMaxwellModelStrainSensitivityAnalysis>();
            services.AddScoped<ICalculateMaxwellModelStrain, CalculateMaxwellModelStrain>();
            services.AddScoped<ICalculateMaxwellModelStressSensitivityAnalysis, CalculateMaxwellModelStressSensitivityAnalysis>();
            services.AddScoped<ICalculateFungModelStress, CalculateFungModelStress>();
            services.AddScoped<ICalculateFungModelStressSentivityAnalysis, CalculateFungModelStressSentivityAnalysis>();
            services.AddScoped<IGenerateFungModelDomain, GenerateFungModelDomain>();

            // Register Numerical Methods
            services.AddTransient<ISimpsonRuleIntegration, SimpsonRuleIntegration>();
            services.AddTransient<IDerivative, Derivative>();

            services.AddControllers();

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
