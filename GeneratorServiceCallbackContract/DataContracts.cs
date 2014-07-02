using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using GeneratorServiceContracts;

namespace GeneratorServiceCallBackContracts
{
    [DataContract]
    public class CallBackMessage : Message
    {

        public CallBackMessage() : base() { }
        public CallBackMessage(String applicationToken = null, object[] data = null, String info = null,
                       Operations? operation = null, Status? status = null, String userToken = null)
            : base(applicationToken: applicationToken,
            data: data,
            info: info,
            operation: operation,
            status: status,
            userToken: userToken)
        { }
    }
}
