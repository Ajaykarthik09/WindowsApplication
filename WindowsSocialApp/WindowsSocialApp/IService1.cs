using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WindowsSocialApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        int ValidateUserDataBase(string password, string email);

        [OperationContract]
        Boolean ValidateUserFile(string email, string password, string FilePath);

        [OperationContract]
        string decrypt(string encryptedText, string key, string iv);

        [OperationContract]
        int Validateadmin(string password, string email);
        // TODO: Add your service operations here
        [OperationContract]
        void StoreUser(UserDetails userdetails);
        [OperationContract]
        UserDetails getdet(string username, string password,  string email);

        [OperationContract]
        int storeReport(string val);

       

    }
   
}
