using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ReportServiceWeb
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        Filess DownloadFile(int ID);
        //[OperationContract]
       // string SearchForPath(int ID);
        [OperationContract]
        Stream getFileChunk(int ID, long offset, int chunkSize);

    }
    [DataContract]
    public class Filess
    {
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public Byte[] content { get; set; }
    }
    
}
