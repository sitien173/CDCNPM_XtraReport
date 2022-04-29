using CDCNPM_XtraReport.Helper;
using CDCNPM_XtraReport.Models;
using CDCNPM_XtraReport.Reports;
using Microsoft.AspNetCore.Mvc;

namespace CDCNPM_XtraReport.Controllers
{
    [Route("[controller]")]
    public class PrintReportController : Controller
    {
        private readonly SqlHelper sqlHelper;

        public PrintReportController(SqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }

        // GET: PrintReport
        [Route("")]
        [HttpPost]
        public ActionResult Index(PrintReport printReport)
        {
            var connectionString = HttpContext.Session.GetString("connectionString");
            var dt = sqlHelper.QueryString(printReport.query, connectionString);
            var xtraRP = new MyReport(dt, printReport.title);
            ViewBag.Report = xtraRP;
            return View();
        }

    }
}