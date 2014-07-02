using GeneratorServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorService
{
    public class CallBackService : ICallBackService
    {
        public void Subscribe(string userToken)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(string userToken)
        {
            throw new NotImplementedException();
        }

        public void NotifyClient(Message msg)
        {
            throw new NotImplementedException();
        }
    }
}
