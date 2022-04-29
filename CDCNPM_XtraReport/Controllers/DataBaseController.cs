using CDCNPM_XtraReport.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CDCNPM_XtraReportlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataBaseController : ControllerBase
    {
        private readonly SqlHelper sqlHelper;

        public DataBaseController(SqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }

        [Route("")]
        [HttpGet]
        public IActionResult GetAllDataBase()
        {
            var databaseName = HttpContext.Session.GetString("databaseName") ?? "";
            string query = $@"SELECT [name] database_name, CASE WHEN name='{databaseName}' THEN 'checked' ELSE '' END AS checked
                                FROM master.sys.databases
                                WHERE state = 0 AND database_id>4 AND HAS_DBACCESS([name])=1";
            var data = sqlHelper.QueryString(query, null);
            return new JsonResult(new { success = true, data = data });
        }


    }
}
