using Microsoft.AspNet.OData.Extensions;
using Newtonsoft.Json.Serialization;

namespace ODataService
{
    public class ODataStartup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddOData();
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).AddNewtonsoftJson(op => op.SerializerSettings.ContractResolver = new DefaultContractResolver());
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

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.EnableDependencyInjection();
                routeBuilder.Select().OrderBy().Filter();
            });
        }
    }
}
