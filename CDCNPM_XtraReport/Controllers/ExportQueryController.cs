using CDCNPM_XtraReport.Helper;
using CDCNPM_XtraReport.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CDCNPM_XtraReport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportQueryController : ControllerBase
    {
        private readonly QueryHelper queryHelper;

        public ExportQueryController(QueryHelper queryHelper)
        {
            this.queryHelper = queryHelper;
        }

        [Route("")]
        [HttpPost]
        public IActionResult ExportQuery(List<Query> data)
        {
            var connectionString = HttpContext.Session.GetString("connectionString");
            try { return new JsonResult(new { success = true, data = queryHelper.GenerateQuery(data, connectionString) }); }
            catch (Exception e)
            {
                var param = Regex.Split(e.Message, "@@");
                var query = param[0];
                var message = param[1];
                return new JsonResult(new { success = false, data = query, message = message });
            }
        }

    }
}
