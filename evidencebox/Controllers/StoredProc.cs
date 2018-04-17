using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace evidencebox.Controllers 
{
    public class StoredProc 
    {
        private Dictionary<string, object> formatSqlData(SqlDataReader rdr, string resultName) 
        {
            var items = new Dictionary<string, object>();
            var returnData = new Dictionary<string, object>();

            while (rdr.Read()) 
            {
                var item = new Dictionary<string, object>();
                for (var i = 1; i < rdr.FieldCount; i++) 
                {
                    item.Add(rdr.GetName(i), rdr.GetValue(i));
                }
                items.Add(rdr.GetValue(0).ToString(), item);
            }
            returnData.Add(resultName, items);
            return returnData;
        }
    
        public Dictionary<string, object> RunStoredProc(string sqlcmd, string resultName, Dictionary<string, string> param) 
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

                if (param != null)
                {
                    foreach (var pair in param) // ADD PARAMETERS
                    {
                        cmd.Parameters.Add(new SqlParameter(pair.Key, pair.Value));
                    }
                }
                rdr = cmd.ExecuteReader();

                //processData
                var returnData = new Dictionary<string, object>();
                returnData = formatSqlData(rdr, resultName);

                return returnData;
            }
            finally 
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }
        }
    }
}
