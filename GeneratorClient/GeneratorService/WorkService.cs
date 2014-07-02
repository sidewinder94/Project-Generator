using GeneratorServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorService
{
    class CallBack : ICallBackClient
    {

        public void NotifyClient(Message msg)
        {
            Console.WriteLine("Not a Client !!!!");
        }
    }

    public class WorkService : IWorkService
    {
        #region connection to callbackService
        private static readonly CallBack callback = new CallBack();
        private readonly DuplexChannelFactory<ICallBackService> channel =
            new DuplexChannelFactory<ICallBackService>(callback, "callBackEndpoint");
        private ICallBackService service;
        #endregion

        #region connection to Java Service

        #endregion

        private List<Guid> authenticatedClients = new List<Guid>();

        public WorkService()
        {
            this.service = channel.CreateChannel();
        }

        public Message ServiceOperation(Message msg)
        {
            switch (msg.Operation)
            {
                case Operations.Authenticate:
                    return Authenticate(msg);
                case Operations.Decode:
                    return Decode(msg);
                case Operations.Finish:
                    service.NotifyClient(msg);
                    break;
                default:
                    throw new NotImplementedException();
                //break;
            }
            return null;
        }

        private Message Decode(Message msg)
        {
            Parallel.For(0, 1000000, (int x, ParallelLoopState loopState) =>
                {
                    byte[] splitted = x.ToString().Select(o => Convert.ToByte(o)).ToArray<byte>();
                    byte currentIndex = 0;
                    for (int i = 0; i < msg.Data.Length; i++)
                    {
                        byte b = (byte)msg.Data[i];
                        b = (byte)(b ^ splitted[currentIndex]);
                        msg.Data[i] = b;
                        if (currentIndex == splitted.Length - 1)
                        {
                            currentIndex = 0;
                        }
                        else
                        {
                            currentIndex++;
                        }
                    }

                });


            return null;
        }

        private Message Authenticate(Message msg)
        {
            Console.WriteLine("{0} asked for Authentication", msg.ApplicationToken);
            Guid generated = Guid.NewGuid();
            while (authenticatedClients.Contains(generated) || generated == Guid.Empty)
            {
                generated = Guid.NewGuid();
            }
            msg.UserToken = generated.ToString();
            msg.Status = Status.Suceeded;
            Console.WriteLine("{0} got {1} as userToken", msg.ApplicationToken, msg.UserToken);
            return msg;
        }



        public IAsyncResult BeginServiceOperation(Message msg, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public Message EndServiceOperation(IAsyncResult result)
        {
            throw new NotImplementedException();
        }
    }
}
