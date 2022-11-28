// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Hosting;
using TelerikReportingRestService;
using TelerikReportingRestService.Controllers;
using TestWebApplication;

Console.WriteLine("Hello, World!");

var endpointManager = new EndpointManager();
var endpointTelerikReporting = new AppEndpoint(TelerikReportingRestService.Startup.ConfigureServices, TelerikReportingRestService.Startup.Configure, "");
endpointManager.AddAppEndpoint(endpointTelerikReporting, typeof(ReportsController).Assembly);

var endpointTest = new AppEndpoint(TestWebApplication.Startup.ConfigureServices, TestWebApplication.Startup.Configure, "/test");
endpointManager.AddAppEndpoint(endpointTest, typeof(TestWebApplication.Controllers.WeatherForecastController).Assembly);

endpointManager.OpenCommunications(false);
