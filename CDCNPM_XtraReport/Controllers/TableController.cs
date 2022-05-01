using CDCNPM_XtraReport.Service;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CDCNPM_XtraReport.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ISQLService _sqlService;

        public TableController(ISQLService _sqlService)
        {
            this._sqlService = _sqlService;
        }

        [Route("")]
        [HttpGet]
        public IActionResult GetListTable()
        {
            var connectionString = HttpContext.Session.GetString("connectionString");
            string query = @"select name FROM SYS.tables where name <> 'sysdiagrams'";
            var data = _sqlService.QueryString(query, connectionString);
            return new JsonResult(new { success = true, data = data });
        }

    }
}
