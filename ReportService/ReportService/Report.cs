using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using System.Messaging;
using System.Data.SqlClient;

namespace ReportService
{
    public class Report
    {
        private readonly Timer _timer;
        public Report()
        {
            _timer = new Timer(1000);
            
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            int id = 0;
            MessageQueue mq = new MessageQueue(@".\private$\Mess");
            
            mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(int) } );
            try
            {
                Message mess = mq.Receive();
                id = (int)mess.Body;
                GenerateReport(id);
            }
            
            catch (Exception ex) { }
            Console.WriteLine(id.ToString());
        }
        public void start()
        {
        _timer.Start();
        _timer.Elapsed += Timer_Elapsed;
        }
        public void stop()
        {
            _timer.Stop(); 
        }
        public void GenerateReport(int ID)
        {
            
                string reportData = GetReportDataFromDatabase(ID);
                Console.WriteLine(reportData);

                // Save report to file
                string reportPath = @"C:\practice";
                if (!Directory.Exists(reportPath))
                {
                    Directory.CreateDirectory(reportPath);
                }

                string reportFile = Path.Combine(reportPath, $"Report_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
                File.WriteAllText(reportFile, reportData);
                Console.WriteLine(reportFile);

                // Update the database
                UpdateReportStatusInDatabase(reportFile, ID);
            
            
        }
        public string GetReportDataFromDatabase(int ID)
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
                rep += read["QUANTITY"].ToString()+$"\t";
                rep += read["ID"].ToString()+$"\t";
                rep += "Thanks for Purchasing Welcome Again";
            }
            
            return rep;
        }
        public void UpdateReportStatusInDatabase(string FILEPATH, int ID)
        {
            try
            {
                string STATUS = "Ready";

                string connectionString = "Data Source=FINLGVDY724;Initial Catalog=Database1;Integrated Security=True;Encrypt=False";
                string query ="UPDATE REPORT SET STATUS = @STATUS, FILEPATH = @FILEPATH WHERE ID = @ID";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand cmd = new SqlCommand(query,connection);
                cmd.Parameters.AddWithValue("@FILEPATH", FILEPATH);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ID", ID);// Use the appropriate report ID
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); };
        }
    }
}
