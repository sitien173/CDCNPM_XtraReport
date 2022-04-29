using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CDCNPM_XtraReport.Helper
{
    public class SqlHelper
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        private SqlConnection conn = new SqlConnection();

        public SqlHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = this.configuration.GetConnectionString("DefaultConnection");
        }

        public string GetConnectionString() => connectionString;

        public void openConnection(string? connectionString)
        {
            if (conn == null) conn = new SqlConnection(connectionString ?? this.connectionString);
            else if (conn.State == System.Data.ConnectionState.Open) conn.Close();
            {
                conn.ConnectionString = String.IsNullOrEmpty(connectionString) ? this.connectionString : connectionString;
            }
            conn.Open();
        }

        public DataTable QueryString(string sql, string? connectionString)
        {
            DataTable dt = new DataTable();
            /*
             SqlDataAdapter da = new SqlDataAdapter(query, conn);
             da.Fill(dt);
            */
            using (var reader = ExcuteDataReader(sql, connectionString))
            {
                dt.Load(reader);
            }
            return dt;
        }

        public SqlDataReader ExcuteDataReader(string sql, string? connectionString)
        {
            openConnection(connectionString);
            SqlCommand command = conn.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = sql;
            return command.ExecuteReader();
        }

        public string DisplaySqlErrors(SqlException exception)
        {
            StringBuilder sb = new();
            for (int i = 0; i < exception.Errors.Count; i++)
            {
                sb.Append("Index #" + i + "\n" +
                    "Error: " + exception.Errors[i].ToString() + "\n");
            }
            return sb.ToString();
        }
    }
}