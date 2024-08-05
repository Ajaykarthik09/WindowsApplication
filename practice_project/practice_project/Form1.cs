using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Data.SqlClient;
using System.Configuration;

namespace practice_project
{
    public partial class Form1 : Form
    {
        ServiceReference4.Service1Client Client = new ServiceReference4.Service1Client("BasicHttpBinding_IService1"); 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = null;
            string email = textBox1.Text;
            string password = textBox2.Text;
            string FilePath = @"C:\practice\file.txt";
            if (ConfigurationManager.AppSettings["StorageType"] == "DataBase")
            {
                int x = Client.ValidateUserDataBase(password,email);
                if (x > 0)
                {
                    form = new Form2(email);
                    form.Show();
                }
            }
            else 
            {
                    if (Client.ValidateUserFile(email,password,FilePath))
                    {
                        form = new Form2(email);
                        form.Show();
                    }
                
            }
            if (form == null)
            {
                MessageBox.Show("Enter Valid Password or email");
            }
           // Client.Close();
            //MessageBox.Show(decryptedEmail);
        }
        public static string decrypt(string encryptedText,string key,string iv)
        {
            using (DES des = DES.Create())
            {
                des.Key = Encoding.UTF8.GetBytes(key);
                des.IV = Encoding.UTF8.GetBytes(iv);

                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(encryptedBytes, 0, encryptedBytes.Length);
                        cs.FlushFinalBlock();

                        byte[] decryptedBytes = ms.ToArray();
                        return Encoding.UTF8.GetString(decryptedBytes);
                    }
                }
            }
        }
    
        private void button2_Click(object sender, EventArgs e)
        {
            Form3 forms = new Form3();
            forms.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '\0';
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 form = new Form5();
            form.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

            Form7 form = null;
            string email = textBox1.Text;
            string password = textBox2.Text;
            //string FilePath = @"C:\practice\file.txt";
            int x = Client.Validateadmin(password,email);
            if (x > 0)
            {
                form = new Form7();
                form.Show();
            }
          
            if (form == null)
            {
                MessageBox.Show("ENter Correct name or password");
            }
        }
    }
}
