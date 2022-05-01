using CDCNPM_XtraReport.Service;
using CDCNPM_XtraReport.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CDCNPM_XtraReport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportQueryController : ControllerBase
    {
        private readonly IQueryService _queryService;

        public ExportQueryController(IQueryService _queryService)
        {
            this._queryService = _queryService;
        }

        [Route("")]
        [HttpPost]
        public IActionResult ExportQuery(List<Query> data)
        {
            var connectionString = HttpContext.Session.GetString("connectionString");
            try { return new JsonResult(new { success = true, data = _queryService.GenerateQuery(data, connectionString) }); }
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
