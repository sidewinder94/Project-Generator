
using GeneratorServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorService
{
    public class WorkService : IWorkService
    {

        #region connection to Java Service

        #endregion

        private List<Guid> authenticatedClients = new List<Guid>();

        public Message ServiceOperation(Message msg)
        {
            switch (msg.Operation)
            {
                case Operations.Authenticate:
                    return Authenticate(msg);
                case Operations.Decode:
                    return Decode(msg);
                case Operations.Finish:
                    Finish(msg);
                    break;
                case Operations.GetCompleted:
                    return GetCompleted();
                case Operations.GetDecrypted:
                    return GetDecrypted(msg);
                default:
                    throw new NotImplementedException();
                //break;
            }
            return null;
        }

        private void Finish(Message msg)
        {
            throw new NotImplementedException();
        }

        private Message GetDecrypted(Message msg)
        {
            DataGenConnexion database = new DataGenConnexion();
            DataSet set = database.DataSets.FirstOrDefault(x => x.Id == (int)msg.Data[0]);
            if (set != null)
            {
                return new Message(operation: Operations.GetDecrypted, status: Status.Suceeded, data: new Object[] { set.DecodedText, set.TrustLevelPdf });
            }
            else
            {
                return new Message(operation: Operations.GetDecrypted, status: Status.Failed, info: "No such file");
            }

        }


        private Message GetCompleted()
        {
            var list = new List<Tuple<int, string, string>>();
            DataGenConnexion database = new DataGenConnexion();
            foreach (DataSet set in database.DataSets)
            {
                var tmp = new Tuple<int, string, string>(set.Id, set.FileName, set.Mail);
                list.Add(tmp);
            }
            return new Message(data: new Object[] { list });
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
    }
}
