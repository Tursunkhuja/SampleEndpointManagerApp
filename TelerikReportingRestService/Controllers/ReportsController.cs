using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Telerik.Reporting.Services;
using Telerik.Reporting.Services.AspNetCore;

namespace TelerikReportingRestService.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController: ReportsControllerBase
    {
        public ReportsController(IReportServiceConfiguration reportServiceConfiguration)
       : base(reportServiceConfiguration)
        {
        }
    }
}
