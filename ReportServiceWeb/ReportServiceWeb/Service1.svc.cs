using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ReportServiceLibrary;

namespace ReportServiceWeb
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        Class1 cr = new Class1();
  
        public  Filess DownloadFile(int ID)
        {
            Filess filess = new Filess();
            string fileName = string.Empty;
            fileName = cr.SearchForPath(ID);
            filess.content = System.IO.File.ReadAllBytes(fileName);
            //filess.Name = "Report.txt";
            return filess;
        }
        
        public Stream getFileChunk(int ID, long offset, int chunkSize)
        {
            string filePath = cr.SearchForPath(ID);
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            if (offset > 0)
            {
                fileStream.Seek(offset, SeekOrigin.Begin);
            }

            byte[] buffer = new byte[chunkSize];
            int bytesRead = fileStream.Read(buffer, 0, chunkSize);

            MemoryStream memoryStream = new MemoryStream(buffer, 0, bytesRead);
            return memoryStream;
        }
    }
}
