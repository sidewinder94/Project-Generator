using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace GeneratorService
{

    public interface IClientCallback
    {
        //Callback function
        [OperationContract(IsOneWay = true)]
        void NotifyClients();
    }

    [ServiceContract(CallbackContract = typeof(IClientCallback))]
    public interface ISubService
    {
        [OperationContract(IsOneWay = true)]
        void SubscribeClient(String userToken);

        [OperationContract(IsOneWay = true)]
        void UnsubscribeClient(String userToken);

        [OperationContract(IsOneWay = true)]
        void NotifyClients();
    }
}
