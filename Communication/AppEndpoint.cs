﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace SampleServerApp.Communication
{
    public class AppEndpoint
    {
        public AppEndpoint(Action<IServiceCollection> configureServices, Action<IApplicationBuilder> map, PathString path)
        {
            ConfigureServices = configureServices;
            Map = map;
            Path = path;
        }

        public Action<IServiceCollection> ConfigureServices { get; }
        public bool AbsolutePath { get; }
        public PathString Path { get; }
        public Action<IApplicationBuilder> Map { get; }

        internal Assembly ExternalAssembly { get; set; }

        public PathString GetPath(PathString pathBase) => AbsolutePath ? Path : pathBase + Path;
    }

    public class EagleHost
    {
        public static void Configure(IWebHostBuilder webBuilder, string baseUrls, AppEndpoint endpoint)
        {

            webBuilder.ConfigureServices(services =>
            {
                if (endpoint.ExternalAssembly != null)
                    services.AddControllers().AddApplicationPart(endpoint.ExternalAssembly);

                endpoint.ConfigureServices(services);
            });

            webBuilder.UseUrls(baseUrls);
        }
    }
}
