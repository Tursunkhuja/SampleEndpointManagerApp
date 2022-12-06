using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;
using Telerik.Reporting.Cache.File;
using Telerik.Reporting.Services;
using Telerik.WebReportDesigner.Services;

namespace TelerikReportingRestService
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson();

            var reportsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");

            // CORS policy that will allow any origin. We use this for the ReportsController (might not be appropriate for other controllers)
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader());
            });

            // Configure dependencies for ReportsController.
            services.TryAddSingleton<IReportServiceConfiguration>(sp =>
                new ReportServiceConfiguration
                {
                    ReportingEngineConfiguration = sp.GetService<IConfiguration>(), //uses default configuration
                    HostAppId = "TelerikReportingRestService", // host app ID for this app
                    Storage = new FileStorage(),
                    ReportSourceResolver = new TypeReportSourceResolver().AddFallbackResolver(new UriReportSourceResolver(reportsPath))
                });

            #region Web Designer Services
            services.AddScoped<IReportDesignerServiceConfiguration>(sp => new ReportDesignerServiceConfiguration
            {
                SettingsStorage = new FileSettingsStorage(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Telerik Reporting")),
                DefinitionStorage = new FileDefinitionStorage(reportsPath, new[] { "Resources" }),
                ResourceStorage = new ResourceStorage(Path.Combine(reportsPath, "Resources")),

            });
            #endregion
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(config =>
            {
                config.MapControllers();
            });
        }
    }
}
