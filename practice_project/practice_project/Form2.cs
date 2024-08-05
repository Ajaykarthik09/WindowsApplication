using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using static System.Net.WebRequestMethods;
using System.Security.Policy;

namespace practice_project
{
    public partial class Form2 : Form
    {
        private Timer reportCheckTimer;
        private int ID;
        ReportServiceWeb.Service1Client Client2 = new ReportServiceWeb.Service1Client();
        ServiceReference4.Service1Client Client = new ServiceReference4.Service1Client("BasicHttpBinding_IService1");
        public Form2(string Email)
        {
            InitializeComponent();

        }
        private void InitializeTimer()
        {
            reportCheckTimer = new Timer();
            reportCheckTimer.Interval = 5000; // Check every 5 seconds
            reportCheckTimer.Tick += new EventHandler(CheckReportStatus);
            reportCheckTimer.Start();
        }
        private void CheckReportStatus(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection("Data Source=FINLGVDY724;Initial Catalog=Database1;Integrated Security=True;Encrypt=False"))
            {
                connection.Open();
                var command = new SqlCommand("SELECT STATUS, FILEPATH FROM REPORT WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID",ID); // Use the appropriate report ID
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var status = reader["STATUS"].ToString();
                        if (status == "Ready")
                        {
                            var filePath = reader["FILEPATH"].ToString();
                            downloadButton.Enabled = true;
                            //downloadButton.Tag = filePath;
                            reportCheckTimer.Stop(); // Stop the timer once the report is ready
                        }
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 forms = new Form4();
            forms.Show();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string val = textBox2.Text;
            ID = Client.storeReport(val);
            InitializeTimer();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //var filepath = downloadButton.Tag.ToString();
                //using (var client = new WebClient())
                //{
                //  client.DownloadFile(new Uri(filepath), "DownloadedReport.txt");
                // MessageBox.Show("Downloaded Successfully");
                // }

                //ReportServiceWeb.Filess file =Client2.DownloadFile(ID);
                //System.IO.File.WriteAllBytes(@"C:\DownloadsFiles\Text.txt", file.content);
                const int chunksize = 1024;
                await DownloadFile(@"C:\DownloadsFiles\Text.txt", chunksize);
                MessageBox.Show("Downloaded Successfully");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private async Task DownloadFile(string savePath, int chunkSize)
        {
         
            long offset = 0;
            bool moreData = true;
            const int end_of_file = 0;
            const int start_of_file = 0;

            using (FileStream fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
            {
                while (moreData)
                {
                    Stream chunkStream = await Client2.getFileChunkAsync(ID, offset, chunkSize);
                    byte[] buffer = new byte[chunkSize];
                    int bytesRead = chunkStream.Read(buffer,start_of_file, chunkSize);

                    if (bytesRead > end_of_file)
                    {
                        fileStream.Write(buffer, start_of_file, bytesRead);
                        offset += bytesRead;
                    }
                    else
                    {
                        moreData = false;
                    }
                }
            }
        }
        
    }
}
