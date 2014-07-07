using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorService
{
    public class SubService : ISubService
    {
        public static readonly Hashtable CallbackChannels = new Hashtable();
        public static readonly List<String> Clients = new List<String>();

        public void SubscribeClient(string userToken)
        {
            var channel = OperationContext.Current.GetCallbackChannel<IClientCallback>();
            if (!Clients.Contains(userToken))
            {
                Clients.Add(userToken);
                if (!CallbackChannels.Contains(channel))
                {
                    CallbackChannels.Add(userToken, channel);
                }
            }
        }

        public void UnsubscribeClient(string userToken)
        {
            if (Clients.Contains(userToken))
            {
                Clients.Remove(userToken);
                CallbackChannels.Remove(userToken);
            }
        }


        public void NotifyClients()
        {
            foreach (IClientCallback callback in CallbackChannels.Values.Cast<IClientCallback>())
            {
                callback.NotifyClients();
            }
        }
    }
}
