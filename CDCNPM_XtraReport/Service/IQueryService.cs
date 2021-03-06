using CDCNPM_XtraReport.Models;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;

namespace CDCNPM_XtraReport.Service
{
    public interface IQueryService
    {
        public string GenerateQuery(List<Query> data, string? connectionString);

    }
    public class QueryService : IQueryService
    {
        private readonly ISQLService _sqlService;
        public QueryService(ISQLService _sqlService)
        {
            this._sqlService = _sqlService;
        }

        private string JoinCriteriaTalbe(HashSet<string> tables, string? connectionString)
        {
            StringBuilder joinQuery = new StringBuilder("(");
            List<ForeignObject> foreignObjects = new List<ForeignObject>();
            var query = @"EXEC sp_fkeys @pktable_name = '{0}'";
            foreach (var table in tables)
            {
                using (var reader = _sqlService.ExcuteDataReader(string.Format(query, table), connectionString))
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var tableFK = reader.GetFieldValue<string>(reader.GetOrdinal("FKTABLE_NAME"));
                            var exist = tables.Contains(tableFK);
                            if (exist)
                            {
                                ForeignObject fObj = new ForeignObject();
                                fObj.tableFK = tableFK;
                                fObj.columnFK = reader.GetFieldValue<string>(reader.GetOrdinal("FKCOLUMN_NAME"));
                                fObj.tablePK = reader.GetFieldValue<string>(reader.GetOrdinal("PKTABLE_NAME"));
                                fObj.columnPK = reader.GetFieldValue<string>(reader.GetOrdinal("PKCOLUMN_NAME"));
                                foreignObjects.Add(fObj);
                            }
                        }
                    }
            }

            // get join query
            foreignObjects.ForEach(fo => joinQuery.Append($" {fo.tablePK}.{fo.columnPK} = {fo.tableFK}.{fo.columnFK} AND"));
            joinQuery.Append(")");
            return joinQuery.Length > 2 ? Regex.Replace(joinQuery.ToString(), @"AND\)$", ")") : "";
        }
        public string GenerateQuery(List<Query> data, string? connectionString)
        {
            StringBuilder select = new("SELECT ");
            StringBuilder from = new("FROM ");
            StringBuilder criteria = new("WHERE ");
            StringBuilder groupBy = new("GROUP BY ");
            StringBuilder having = new("HAVING ");

            StringBuilder sortAsc = new();
            StringBuilder sortDesc = new();

            HashSet<string> tables = new();
            foreach (var obj in data)
            {
                // lấy select
                if (obj.IsSelect)
                {

                    if (string.IsNullOrEmpty(obj.Used))
                        select.Append(obj.Tenbang).Append('.').Append(obj.Tencot).Append(", ");
                    else if (obj.Used.ToUpper().Equals("GROUPBY"))
                    {
                        select.Append(obj.Tenbang).Append('.').Append(obj.Tencot).Append(", ");
                    }
                    else
                    {
                        select.Append(obj.Used).Append('(')
                                               .Append(obj.Tenbang).Append('.').Append(obj.Tencot)
                                                .Append(')').Append(", ");
                    }
                }

                // thêm vào tí join nhau qua khoá ngoại
                if (tables.Add(obj.Tenbang))
                    // lấy from
                    from.Append(obj.Tenbang.ToUpper()).Append(", ");

                // lấy criteria
                if (!string.IsNullOrEmpty(obj.Criteria.Trim()))
                {
                    if (!string.IsNullOrEmpty(obj.Used) && !obj.Used.ToUpper().Equals("GROUPBY"))
                    {
                        // lấy having
                        having.Append(obj.Used).Append('(')
                              .Append(obj.Tenbang).Append('.').Append(obj.Tencot)
                              .Append(')').Append(' ')
                              .Append(obj.Criteria)
                              .Append(' ')
                              .Append(obj.Toantu)
                              .Append(" ");

                    }
                    else
                    {
                        // lấy criteria
                        criteria.Append(obj.Tenbang)
                                .Append('.')
                                .Append(obj.Tencot)
                                .Append(' ')
                                .Append(obj.Criteria)
                                .Append(' ')
                                .Append(obj.Toantu)
                                .Append(" ");
                    }
                }

                // lấy group by
                if (!string.IsNullOrEmpty(obj.Used) && obj.Used.ToUpper().Equals("GROUPBY"))
                    groupBy.Append(obj.Tenbang)
                         .Append('.')
                         .Append(obj.Tencot)
                         .Append(", ");

                // lấy order by
                if (obj.IsSortAsc)
                {
                    if (!string.IsNullOrEmpty(obj.Used) && !obj.Used.ToUpper().Equals("GROUPBY"))
                    {
                        sortAsc.Append(obj.Used).Append('(')
                             .Append(obj.Tenbang).Append('.').Append(obj.Tencot)
                             .Append(')').Append(' ').Append(", ");
                    }
                    else
                    {
                        sortAsc.Append(obj.Tenbang)
                             .Append('.')
                             .Append(obj.Tencot)
                             .Append(", ");
                    }

                }

                if (obj.IsSortDesc)
                {
                    if (!string.IsNullOrEmpty(obj.Used) && !obj.Used.ToUpper().Equals("GROUPBY"))
                    {
                        sortDesc.Append(obj.Used).Append('(')
                             .Append(obj.Tenbang).Append('.').Append(obj.Tencot)
                             .Append(')').Append(' ').Append(", ");
                    }
                    else
                    {
                        sortDesc.Append(obj.Tenbang)
                             .Append('.')
                             .Append(obj.Tencot)
                             .Append(", ");
                    }
                }
            }
            // gom chung sort 
            var fOrderBy = "ORDER BY ";
            if (sortAsc.Length > 0)
            {
                fOrderBy += sortAsc.ToString().Trim().TrimEnd(',') + " ASC, ";
            }
            if (sortDesc.Length > 0)
            {
                fOrderBy += sortDesc.ToString().Trim().TrimEnd(',') + " DESC";
            }

            // format sql 
            var fSelect = Regex.Replace(select.ToString().ToUpper(), @"\s+", " ").Trim().TrimEnd(',') + "\n";
            var fFrom = Regex.Replace(from.ToString().ToUpper(), @"\s+", " ").Trim().TrimEnd(',') + "\n";
            var fCriteria = Regex.Replace(criteria.ToString().ToUpper(), @"\s+", " ").Trim().TrimEnd('A', 'N', 'D').TrimEnd('O', 'R');
            var fGroupBy = Regex.Replace(groupBy.ToString().ToUpper(), @"\s+", " ").Trim().TrimEnd(',');
            var fHaving = Regex.Replace(having.ToString().ToUpper(), @"\s+", " ").Trim().TrimEnd('A', 'N', 'D').TrimEnd('O', 'R');

            // lấy join query
            var joinTablesQuery = JoinCriteriaTalbe(tables, connectionString);

            if (!string.IsNullOrEmpty(joinTablesQuery))
            {
                if (fCriteria.Equals("WHERE")) fCriteria += " " + joinTablesQuery;
                else fCriteria += " AND " + joinTablesQuery;
            }

            if (fCriteria.Equals("WHERE")) fCriteria = string.Empty;
            else fCriteria += "\n";

            if (fHaving.Equals("HAVING")) fHaving = string.Empty;
            else fHaving += "\n";

            if (fGroupBy.Equals("GROUP BY")) fGroupBy = string.Empty;
            else fGroupBy += "\n";

            if (fOrderBy.Trim().Equals("ORDER BY")) fOrderBy = string.Empty;
            else fOrderBy = fOrderBy.ToUpper().Trim().TrimEnd(',') + "\n";

            var query = fSelect + fFrom + fCriteria + fGroupBy + fHaving + fOrderBy ;
            try
            {
                // test query
                using (var reader = _sqlService.ExcuteDataReader(query, connectionString)) ;
            }
            catch (SqlException e) { throw new Exception(query + "@@" + _sqlService.DisplaySqlErrors(e)); }
            return query;
        }
    }
}