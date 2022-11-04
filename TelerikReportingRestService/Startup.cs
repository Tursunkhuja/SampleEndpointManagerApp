using Microsoft.Extensions.DependencyInjection.Extensions;
using Telerik.Reporting.Cache.File;
using Telerik.Reporting.Services;
using Telerik.WebReportDesigner.Services;

namespace TelerikReportingRestService
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            var reportsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");

            // CORS policy that will allow any origin. We use this for the ReportsController (might not be appropriate for other controllers)
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
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
            services.TryAddSingleton<IReportDesignerServiceConfiguration>(sp => new ReportDesignerServiceConfiguration
            {
                DefinitionStorage = new FileDefinitionStorage(reportsPath),
                ResourceStorage = new ResourceStorage(Path.Combine(reportsPath, "Resources")),
                SettingsStorage = new FileSettingsStorage(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Telerik Reporting"))
            });
            #endregion
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app
                .UseRouting()
                .UseCors("AllowOrigin")
                .UseEndpoints(config =>
                {
                    config.MapControllers();
                });
        }
    }
}
