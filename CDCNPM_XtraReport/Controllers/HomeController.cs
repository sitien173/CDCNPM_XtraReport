using CDCNPM_XtraReport.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CDCNPM_XtraReport.Controllers
{
    [Route("")]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ISQLService _sqlService;

        public HomeController(ISQLService _sqlService)
        {
            this._sqlService = _sqlService;
        }
        [Route("")]
        public ActionResult Index()
        {
            // default connection
            HttpContext.Session.SetString("connectionString", _sqlService.GetConnectionString());
            return View();
        }

        [Route("QBE")]
        public ActionResult QBE(string databaseName = "")
        {
            if (HttpContext.Session.GetString("connectionString") == null || databaseName == "") RedirectToAction("Index");
            HttpContext.Session.SetString("databaseName", databaseName);
            var connectionString = _sqlService.GetConnectionString();
            var param = connectionString.Split(';');
            StringBuilder connectionStringBuilder = new();
            foreach (var item in param)
            {
                if (item.StartsWith("Initial Catalog"))
                    connectionStringBuilder.Append("Initial Catalog=").Append(databaseName).Append(';');
                else connectionStringBuilder.Append(item).Append(';');
            }
            HttpContext.Session.SetString("connectionString", connectionStringBuilder.ToString().TrimEnd(';'));
            return View();
        }
    }
}