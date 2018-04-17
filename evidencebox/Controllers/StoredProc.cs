using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace evidencebox.Controllers 
{
    public class StoredProc 
    {
        private Dictionary<int, object> formatSqlData(SqlDataReader rdr) 
        {
            var items = new Dictionary<int, object>();

            while (rdr.Read()) 
            {
                int counter = 0;
                var item = new Dictionary<string, object>();
                for (var i = 0; i < rdr.FieldCount; i++) 
                {
                    item.Add(rdr.GetName(i), rdr.GetValue(i));
                }
                items.Add(counter++, item);
            }
            return items;
        }
    
        public Dictionary<string, object> RunStoredProc(string sqlcmd, Dictionary<string, string> procParams) 
        {
            SqlConnection conn = null;
            SqlDataReader rdr = null;

            try 
            {
                // create and open a connection object
                conn = new SqlConnection("Server=den1.mssql3.gear.host;DataBase=evidencebox;Integrated Security=False;persist security info = True;user id=evidencebox;password=Ce0OQ9-O3?ed;");
                conn.Open();

                var cmd = new SqlCommand(sqlcmd, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // add parameters
                if (procParams != null)
                {
                    foreach (var pair in procParams)
                    {
                        cmd.Parameters.Add(new SqlParameter(pair.Key, pair.Value));
                    }
                }
                rdr = cmd.ExecuteReader();

                //processData
                return formatSqlData(rdr);
            }
            finally 
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }
        }
    }
}
