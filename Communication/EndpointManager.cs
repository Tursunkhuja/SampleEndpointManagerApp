using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SampleServerApp.Communication
{
    public class EndpointManager
    {
        public IDisposable ApiHost { get; private set; }
        public void OpenCommunications(bool startupMode, int retryAttempts = 3)
        {            
            ApiHost = StartApiHost(AppEndpoint);
        }

        #region Endpoints
        AppEndpoint AppEndpoint { get; set; } 

        void AddAppEndpoint(AppEndpoint endpoint, Assembly assembly = null)
        {
            endpoint.ExternalAssembly = assembly;
            AppEndpoint = endpoint;
        }

        #endregion

        IDisposable StartApiHost(AppEndpoint appEndpoint)
        {
            var urls = "http://localhost:49152";
            
            try
            {

                var host = Host.CreateDefaultBuilder()
                    .ConfigureWebHostDefaults(webBuilder => EagleHost.Configure(webBuilder, urls, appEndpoint))
                    .Build();

                try
                {
                    if (ApiHost != null)
                    {
                        // If the host was already started in startup mode, close it before starting the regular host.
                        ApiHost.Dispose();
                        ApiHost = null;
                    }
                    host.Start();                    
                    return host;
                }
                catch
                {
                    host.Dispose();
                    throw;
                }
            }
            catch
            {
               
            }
        }
    }
}