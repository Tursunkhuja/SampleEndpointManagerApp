using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Telerik.Reporting.Services;
using Telerik.WebReportDesigner.Services;
using Telerik.WebReportDesigner.Services.Controllers;

namespace TelerikReportingRestService.Controllers
{
    [ApiController]
    [Route("api/reportdesigner")]
    //[EnableCors("AllowOrigin")]
    public class ReportDesignerController : ReportDesignerControllerBase
    {
        public ReportDesignerController(IReportDesignerServiceConfiguration reportDesignerServiceConfiguration, IReportServiceConfiguration reportServiceConfiguration)
               : base(reportDesignerServiceConfiguration, reportServiceConfiguration)
        {
        }
    }
}