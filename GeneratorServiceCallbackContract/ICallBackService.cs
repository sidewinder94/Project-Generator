using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorServiceCallBackContracts
{
    public interface ICallBackClient
    {
        [OperationContract(IsOneWay = true)]
        void NotifyClient(CallBackMessage msg);
    }


    [ServiceContract(CallbackContract = typeof(ICallBackClient))]
    public interface ICallBackService
    {
        [OperationContract(IsOneWay = true)]
        void NotifyClient(CallBackMessage msg);
        [OperationContract(IsOneWay = true)]
        void Subscribe(String userToken);
        [OperationContract(IsOneWay = true)]
        void Unsubscribe(String userToken);
    }
}
