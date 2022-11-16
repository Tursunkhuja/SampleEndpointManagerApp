// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Hosting;
using TelerikReportingRestService;
using TelerikReportingRestService.Controllers;

Console.WriteLine("Hello, World!");

var endpointManager = new EndpointManager();
var endpoint = new AppEndpoint(Startup.ConfigureServices, Startup.Configure, "");
endpointManager.AddAppEndpoint(endpoint, typeof(ReportsController).Assembly);
endpointManager.OpenCommunications(false);
