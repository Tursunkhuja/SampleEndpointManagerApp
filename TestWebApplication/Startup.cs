﻿using Microsoft.Extensions.DependencyInjection.Extensions;

namespace TestWebApplication
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();

            // CORS policy that will allow any origin. We use this for the ReportsController (might not be appropriate for other controllers)
            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader());
            //});

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