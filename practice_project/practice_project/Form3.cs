using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace practice_project
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Name = textBox1.Text;
            string key = "s3cr3tK3";
            string iv = "s3cr3tIV";
            string encryptedName = encrypt(Name, key, iv);
            string Email = maskedTextBox1.Text;
            string encryptedEmail = encrypt(Email, key, iv);
            string Password = maskedTextBox2.Text;
            string encryptedPassword = encrypt(Password, key, iv);
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            string FilePath = @"C:\practice\file.txt";
            if (ConfigurationManager.AppSettings["StorageType"] == "FileSystem")
            {

                if (Password != maskedTextBox3.Text)
                {
                    maskedTextBox3.Clear();
                    MessageBox.Show("Please Enter Correct Password");
                }
                else if (!Regex.IsMatch(Email, pattern))
                {
                    maskedTextBox1.Clear();
                    MessageBox.Show("Enter Valid Email");
                }

                else
                {
                    string Data = $"{(encryptedEmail)} {encryptedPassword} {encryptedName}\n";
                    try
                    {
                        File.AppendAllText(ConfigurationManager.AppSettings["FilePath"], Data);
                        MessageBox.Show("New User Added Successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An Error occured");
                    }
                }
            }
            else if(ConfigurationManager.AppSettings["StorageType"] == "DataBase")
            {
                string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
                string query = "INSERT INTO newUsers(UserName,Email,Password1) VALUES('"+Name+"','"+Email+"','"+Password+"')";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show($"Saved successfully");
            }
        }
        public static string encrypt(string plainText, string key,string iv)
        {
            using (DES des = DES.Create())
            {
                des.Key = Encoding.UTF8.GetBytes(key);
                des.IV = Encoding.UTF8.GetBytes(iv);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                        cs.Write(plainBytes, 0, plainBytes.Length);
                        cs.FlushFinalBlock();

                        byte[] encryptedBytes = ms.ToArray();
                        return Convert.ToBase64String(encryptedBytes);
                    }
                }
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }



        private void Storage_options_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
