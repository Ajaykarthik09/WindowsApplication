using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practice_project
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            label2.Text = ConfigurationManager.AppSettings["StorageType"];
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string presentStorageType = ConfigurationManager.AppSettings["StorageType"];
            if (presentStorageType == "FileSystem")
            {
                string newStorageType = "DataBase";
                ChangeStorageType(newStorageType);
                label2.Text = ConfigurationManager.AppSettings["StorageType"];
                MessageBox.Show("Storage Type Changed");
            }
            else
            {
                string newStorageType = "FileSystem";
                ChangeStorageType(newStorageType);
                label2.Text = ConfigurationManager.AppSettings["StorageType"];
                MessageBox.Show("Storage Type Changed");
            }
        }
        private void ChangeStorageType(string newStorageType)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["StorageType"].Value = newStorageType;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }
    }
}
