using CDCNPM_XtraReport.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CDCNPM_XtraReport.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly SqlHelper sqlHelper;

        public TableController(SqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }

        [Route("")]
        [HttpGet]
        public IActionResult GetListTable()
        {
            var connectionString = HttpContext.Session.GetString("connectionString");
            string query = @"select name FROM SYS.tables where name <> 'sysdiagrams'";
            var data = sqlHelper.QueryString(query, connectionString);
            return new JsonResult(new { success = true, data = data });
        }

    }
}
