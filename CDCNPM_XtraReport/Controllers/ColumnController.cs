using CDCNPM_XtraReport.Service;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CDCNPM_XtraReport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {
        private readonly ISQLService _sqlService;

        public ColumnController(ISQLService _sqlService)
        {
            this._sqlService = _sqlService;
        }

        [Route("")]
        [HttpGet]
        public IActionResult GetListColumnInTable(string table = "")
        {
            var connectionString = HttpContext.Session.GetString("connectionString");
            string query = $"SELECT TABLE_NAME, COLUMN_NAME FROM information_schema.columns  WHERE table_name = '" + table + "'";
            var data = _sqlService.QueryString(query, connectionString);
            return new JsonResult(new { success = true, data = data });

        }
    }
}
