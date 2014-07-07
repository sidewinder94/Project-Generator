using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

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
