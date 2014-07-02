using GeneratorServiceContracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorService
{
    public class CallBackService : ICallBackService
    {
        public static readonly Hashtable CallbackChannels = new Hashtable();
        public static readonly List<Guid> Clients = new List<Guid>();
        public static readonly Random rdm = new Random(DateTime.Now.Millisecond);

        public void Subscribe(string userToken)
        {
            Console.WriteLine("{0} tried to connect", userToken);
            var channel = OperationContext.Current.GetCallbackChannel<ICallBackClient>();
            Guid userGuid = Guid.Parse(userToken);

            if (!Clients.Contains(userGuid))
            {
                Clients.Add(userGuid);
            }

            if (!CallbackChannels.Contains(channel))
            {
                CallbackChannels.Add(userGuid, channel);
            }
            Console.WriteLine("{0} connected", userToken);
        }

        public void Unsubscribe(string userToken)
        {
            Guid userGuid = Guid.Parse(userToken);
            if (Clients.Contains(userGuid))
            {
                Clients.Remove(userGuid);
                CallbackChannels.Remove(userGuid);
                Console.WriteLine("{0} disconnected", userToken);

            }
        }

        public void NotifyClient(Message msg)
        {
            Guid userGuid = Guid.Parse(msg.UserToken);
            ICallBackClient channel = CallbackChannels[userGuid] as ICallBackClient;
            if (channel != null)
            {
                Console.WriteLine("Message sent back to client {O}", userGuid);
                channel.NotifyClient(msg);
            }
        }
    }
}
