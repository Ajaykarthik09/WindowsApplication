using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WindowsSocialApp
{
    [DataContract]
    public class UserDetails
    {
        [DataMember]
        public string email
        { get; set; }
        [DataMember]
        public string name 
        { get; set; }

        [DataMember]
        public string password
        { get; set; }
        

    }
}
