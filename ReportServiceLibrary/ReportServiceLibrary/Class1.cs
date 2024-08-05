using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime;
using System.ServiceModel;
//using System.ServiceModel.Web;
using System.Text;

namespace ReportServiceLibrary
{
    
    public class Class1
    {
        public string SearchForPath(int ID)
        {
            string rep = "";
            string connectionString = "Data Source=FINLGVDY724;Initial Catalog=Database1;Integrated Security=True;Encrypt=False";
            string query = $"SELECT * FROM REPORT WHERE ID = @ID";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ID", ID);
            var read = cmd.ExecuteReader();
            if (read.Read())
            {
                rep += read["FILEPATH"].ToString();
            }
            return rep;
        }
        public int Add(int a, int b)
        {
            return a + b;
        }

    }
}
