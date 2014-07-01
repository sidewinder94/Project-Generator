using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace GeneratorServiceContracts
{
    public interface ICallBackClient
    {
        [OperationContract(IsOneWay = true)]
        void NotifyClient(Message msg);
    }


    [ServiceContract(CallbackContract = typeof(ICallBackClient))]
    interface IWorkService
    {
        [OperationContract(IsOneWay = true)]
        void CompletedOperation(Message msg);
        [OperationContract]
        Message Service(Message msg);
        [OperationContract(IsOneWay = true)]
        void Subscribe(String userToken);
        [OperationContract(IsOneWay = true)]
        void Unsubscribe(String userToken);
    }
}
