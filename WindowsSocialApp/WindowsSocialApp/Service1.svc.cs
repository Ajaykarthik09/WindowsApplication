using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WindowsService1;
using WindowsSocialApp;
using System.Messaging;
using System.Runtime.Serialization;
//using static System.Net.WebRequestMethods;
namespace WindowsSocialApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        //string pathss = null;
        int ID = 0;
        public int ValidateUserDataBase(string password, string email)
        {
            
                string connectionString = "Data Source=FINLGVDY724;Initial Catalog=Database1;Integrated Security=True;Encrypt=False";
                string query = $"SELECT COUNT(*) FROM newUsers WHERE Password1 = @password AND Email = @email";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email);
                return (int)cmd.ExecuteScalar();
        }
        public Boolean ValidateUserFile(string email,string password,string Filepath)
        {
            string[] lines = File.ReadAllLines(Filepath);
            foreach (string line in lines)
            {
                string[] nameData = line.Split(' ');
                var emails = decrypt(nameData[0], "s3cr3tK3", "s3cr3tIV");
                var pass = decrypt(nameData[1], "s3cr3tK3", "s3cr3tIV");
                if(pass == password && emails==email)
                {
                    return true;
                }
            }
            return false;
        }
        public int Validateadmin(string password, string email)
        {
            
            string connectionString = "Data Source=FINLGVDY724;Initial Catalog=Database1;Integrated Security=True;Encrypt=False";
            string query = $"SELECT COUNT(*) FROM Admin WHERE password = @password AND email = @email";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@email", email);
            return (int)cmd.ExecuteScalar();
        }
        public string decrypt(string encryptedText, string key, string iv)
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
        public UserDetails getdet(string username, string password, string email)
        {
            UserDetails userData = new UserDetails();
            userData.name = username;
            userData.email = email;
            userData.password = password;
            return userData;
        }
        public void StoreUser(UserDetails ud)
        {
            string connectionString = "Data Source=FINLGVDY724;Initial Catalog=Database1;Integrated Security=True;Encrypt=False";
            string query = $"INSERT INTO newUsers(UserName,Email,Password1) VALUES('@ud.name','@ud.email','@ud.password')";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public int storeReport(string val)
        {
            Random rnd = new Random();
            ID = rnd.Next();
            MessageQueue mq = new MessageQueue();
            mq.Path = @".\private$\Mess";
            if(MessageQueue.Exists(@".\private$\Mess"))
            {
                mq = new MessageQueue(@".\private$\Mess");
            }
            else
            {
                MessageQueue.Create(mq.Path);
            }
            Message message = new Message();
            message.Body = ID;
            string connectionString = "Data Source=FINLGVDY724;Initial Catalog=Database1;Integrated Security=True;Encrypt=False";
            var command = "INSERT INTO REPORT(ID,QUANTITY,STATUS,FILEPATH)VALUES('" + ID + "','"+val+"','" + null + "','" + null + "')";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(command, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            //host.GenerateReport(ID.ToString());
            mq.Send(ID);
            
            //Filess fi = DownloadFile(path);
            return ID;
        }
        
    }
}
