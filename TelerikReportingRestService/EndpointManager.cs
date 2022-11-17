﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TelerikReportingRestService
{
    public class EndpointManager
    {
        public IHost ApiHost { get; private set; }
        Dictionary<string, AppEndpoint> AppEndpoints { get; } = new();

        public void OpenCommunications(bool startupMode, int retryAttempts = 3)
        {
            ApiHost = StartApiHost(AppEndpoints.Values.ToList());
        }

        #region Endpoints

        public void AddAppEndpoint(AppEndpoint endpoint, Assembly assembly)
        {
            endpoint.ExternalAssembly = assembly;
            AppEndpoints.TryAdd(endpoint.Path, endpoint);
        }

        #endregion

        IHost  StartApiHost(List<AppEndpoint> appEndpoints)
        {
            var urls = "http://localhost:49152";

            try
            {
                var host = Host.CreateDefaultBuilder().ConfigureWebHostDefaults(w => EagleHost.Configure(w, "", urls, appEndpoints)).Build();
                try
                {
                    if (ApiHost != null)
                    {
                        // If the host was already started in startup mode, close it before starting the regular host.
                        ApiHost.Dispose();
                        ///ApiHost = null;
                    }
                    host.Run();

                    return host;
                }
                catch
                {
                    //host.Dispose();
                    throw;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}