using System.Net;
using TelerikReportingRestService.Controllers;
using TelerikReportingRestService;

public class Program
{
    static void Main(string[] args)
    {
        //throw new InvalidOperationException("You should not run this App.");

        var endpointManager = new EndpointManager();
        var endpoint = new AppEndpoint(Startup.ConfigureServices, Startup.Configure, "");
        endpointManager.AddAppEndpoint(endpoint, typeof(ReportsController).Assembly);
        endpointManager.OpenCommunications(false);
    }
}