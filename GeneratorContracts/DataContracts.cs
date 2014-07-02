using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GeneratorServiceContracts
{
    [DataContract]
    public enum Operations
    {
        [EnumMember]
        Authenticate = 0,
        [EnumMember]
        Decode = 1,
        [EnumMember]
        Finish = 2
    }

    public enum Status
    {
        [EnumMember]
        Sent = 0,
        [EnumMember]
        Suceeded = 1,
        [EnumMember]
        Failed = 2
    }

    [DataContract]
    public class Message
    {
        [DataMember]
        public String ApplicationToken { get; set; }
        [DataMember]
        public object[] Data { get; set; }
        [DataMember]
        public String Info { get; set; }
        [DataMember]
        public Operations Operation { get; set; }
        [DataMember]
        public Status Status { get; set; }
        [DataMember]
        public String UserToken { get; set; }
    }
}
