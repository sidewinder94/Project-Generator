using System;
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
        Finish = 2,
        [EnumMember]
        GetCompleted = 3,
        [EnumMember]
        GetDecrypted = 4,
    }

    [DataContract]
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
        [DataMember(IsRequired = true, Order = 0)]
        public String ApplicationToken { get; set; }
        [DataMember(IsRequired = true, Order = 1)]
        public object[] Data { get; set; }
        [DataMember(IsRequired = true, Order = 2)]
        public String Info { get; set; }
        [DataMember(IsRequired = true, Order = 3)]
        public Operations Operation { get; set; }
        [DataMember(IsRequired = true, Order = 4)]
        public Status Status { get; set; }
        [DataMember(IsRequired = true, Order = 5)]
        public String UserToken { get; set; }

        public Message() { }

        public Message(String applicationToken, object[] data, String info,
                       Operations? operation, Status? status, String userToken)
        {
            ApplicationToken = applicationToken;
            Data = data;
            Info = info;
            UserToken = userToken;
            if (operation != null)
            {
                Operation = (Operations)operation;
            }
            if (status != null)
            {
                Status = (Status)status;
            }
        }
    }
}
