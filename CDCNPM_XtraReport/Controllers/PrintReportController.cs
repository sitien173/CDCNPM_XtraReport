using CDCNPM_XtraReport.Service;
using CDCNPM_XtraReport.Models;
using CDCNPM_XtraReport.Reports;
using Microsoft.AspNetCore.Mvc;

namespace CDCNPM_XtraReport.Controllers
{
    [Route("[controller]")]
    public class PrintReportController : Controller
    {
        private readonly ISQLService _sqlService;

        public PrintReportController(ISQLService _sqlService)
        {
            this._sqlService = _sqlService;
        }

        // GET: PrintReport
        [Route("")]
        [HttpPost]
        public ActionResult Index(PrintReport printReport)
        {
            var connectionString = HttpContext.Session.GetString("connectionString");
            var dt = _sqlService.QueryString(printReport.query, connectionString);
            var xtraRP = new MyReport(dt, printReport.title);
            ViewBag.Report = xtraRP;
            return View();
        }

    }
}