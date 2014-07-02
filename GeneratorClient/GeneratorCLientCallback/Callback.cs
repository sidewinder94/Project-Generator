using GeneratorServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorClientCallback
{
    class Callback : ICallBackClient
    {
        Main Parent;
        public delegate void ReceivedMessage(Message msg);
        public event ReceivedMessage receivedEvent;
        public Callback(Main parent)
        {
            this.Parent = parent;
        }


        public void NotifyClient(Message msg)
        {
            throw new NotImplementedException();
        }
    }
}
