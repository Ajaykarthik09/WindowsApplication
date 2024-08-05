using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practice_project
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form7 form = null;
            string name = textBox1.Text;
            string password = textBox2.Text;
            string FilePath = @"C:\practice\file.txt";
            string connectionString = "Data Source=FINLGVDY724;Initial Catalog=Database1;Integrated Security=True;Encrypt=False";
            string query = $"SELECT COUNT(*) FROM Admin WHERE password = @password AND name = @name";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@name", name);
            int x = (int)cmd.ExecuteScalar();
            if (x > 0)
            {
                form = new Form7();
                form.Show();
            }
            connection.Close();
            if(form == null)
            {
                MessageBox.Show("ENter Correct name or password");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '\0';
        }
    }
}
