// See https://aka.ms/new-console-template for more information
using SampleServerApp.Communication;
using TelerikReportingRestService.Controllers;

Console.WriteLine("Starting Reporting and OData endpoints!");

var endpointManager = new EndpointManager();
var endpointTelerikReporting = new AppEndpoint(TelerikReportingRestService.Startup.ConfigureServices, TelerikReportingRestService.Startup.Configure,  "/Reporting");
endpointManager.AddAppEndpoint(endpointTelerikReporting, typeof(ReportsController).Assembly);

var endpointTest = new AppEndpoint(ODataService.ODataStartup.ConfigureServices, ODataService.ODataStartup.Configure, path: "/OData");
endpointManager.AddAppEndpoint(endpointTest, typeof(ODataService.Controllers.WeatherForecastController).Assembly);

endpointManager.OpenCommunications(false);
