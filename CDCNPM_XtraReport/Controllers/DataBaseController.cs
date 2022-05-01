using CDCNPM_XtraReport.Service;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CDCNPM_XtraReportlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataBaseController : ControllerBase
    {
        private readonly ISQLService _sqlService;

        public DataBaseController(ISQLService _sqlService)
        {
            this._sqlService = _sqlService;
        }

        [Route("")]
        [HttpGet]
        public IActionResult GetAllDataBase()
        {
            var databaseName = HttpContext.Session.GetString("databaseName") ?? "";
            string query = $@"SELECT [name] database_name, CASE WHEN name='{databaseName}' THEN 'checked' ELSE '' END AS checked
                                FROM master.sys.databases
                                WHERE state = 0 AND database_id>4 AND HAS_DBACCESS([name])=1";
            var data = _sqlService.QueryString(query, null);
            return new JsonResult(new { success = true, data = data });
        }


    }
}
