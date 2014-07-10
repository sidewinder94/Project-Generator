
using GeneratorService.JavaServer;
using GeneratorServiceContracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneratorService
{


    public class WorkService : IWorkService
    {
        #region connection to callbackService
        private DuplexChannelFactory<ISubService> channel;
        private ISubService service;

        private class CallbackClass : IClientCallback
        {
            public void NotifyClients()
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        private static readonly Hashtable filesAnalyzed = new Hashtable();
        private static readonly List<Guid> authenticatedClients = new List<Guid>();

        public WorkService()
        {
            channel = new DuplexChannelFactory<ISubService>(new CallbackClass(), "callBackEndpoint");
            service = channel.CreateChannel();
        }

        ~WorkService()
        {
            channel.Close();
        }

        public Message ServiceOperation(Message msg)
        {
            switch (msg.Operation)
            {
                case Operations.Authenticate:
                    return Authenticate(msg);
                case Operations.Decode:
                    return Authenticate(Decode, msg);
                case Operations.Finish:
                    Finish(msg);
                    break;
                case Operations.GetCompleted:
                    return Authenticate(GetCompleted, msg);
                case Operations.GetDecrypted:
                    return Authenticate(GetDecrypted, msg);
                default:
                    throw new NotImplementedException();
                //break;
            }
            return null;
        }

        private void Finish(Message msg)
        {
            /**
             * [0] texte décodé validé
             * [1] nom fichier
             * [2] mail
             * [3] pdf indice confiance
             */
            Console.WriteLine("Finished called");
            WorkService.filesAnalyzed[msg.Info] = new Tuple<bool, Message>(false, msg);
            var database = new DataGenConnexion();
            var data = new DataSet();
            data.DecodedText = (string)msg.Data[0];
            data.FileID = msg.Info;
            data.FileName = msg.Data[1] as String;
            if (data.FileName == null)
            {
                data.FileName = "(unknown filename)";
            }
            data.Mail = msg.Data[2] as String;
            if (data.Mail == null)
            {
                data.Mail = "not found";
            }
            data.TrustLevelPdf = (byte[])msg.Data[3];
            database.DataSets.Add(data);
            database.SaveChanges();
            database = null;
            if (channel.State == CommunicationState.Faulted)
            {
                channel.Abort();
                service = channel.CreateChannel();
            }
            service.NotifyClients();
        }

        private Message GetDecrypted(Message msg)
        {
            DataGenConnexion database = new DataGenConnexion();
            string msgId = (string)msg.Data[0];
            DataSet set = database.DataSets.FirstOrDefault(x => x.FileID == msgId);
            database = null;
            if (set != null)
            {
                return new Message(operation: Operations.GetDecrypted, status: Status.Suceeded,
                                   data: new Object[] { set.DecodedText, set.TrustLevelPdf },
                                   applicationToken: msg.ApplicationToken,
                                   userToken: msg.UserToken, info: "");
            }
            else
            {
                return new Message(operation: Operations.GetDecrypted,
                                   status: Status.Failed,
                                   info: "No such file",
                                   data: new Object[0],
                                   applicationToken: msg.ApplicationToken,
                                   userToken: msg.UserToken);
            }

        }


        private Message GetCompleted(Message msg)
        {
            DataGenConnexion database = new DataGenConnexion();
            var objects = new Object[database.DataSets.Count() * 3];
            int i = 0;
            foreach (DataSet data in database.DataSets)
            {

                objects[i] = data.FileID;
                objects[i + 1] = data.FileName;
                objects[i + 2] = data.Mail;
                i += 3;
            }
            return new Message(data: objects, applicationToken: "server",
                               info: "", operation: Operations.GetCompleted,
                               status: Status.Suceeded, userToken: "");
        }


        string EncryptOrDecrypt(string text, string key)
        {
            var result = new StringBuilder();

            for (int c = 0; c < text.Length; c++)
            {
                result.Append((char)((uint)text[c] ^ (uint)key[c % key.Length]));
            }
            return result.ToString();
        }

        private Message Decode(Message msg)
        {
            var database = new DataGenConnexion();
            var client = new GeneratorClient("GeneratorPort");
            int count = 0;
            int counter = database.DataSets.Where(x => x.FileID == msg.Info).Count();

            if (!WorkService.filesAnalyzed.Contains(msg.Info) && (counter == 0))
            {
                WorkService.filesAnalyzed.Add(msg.Info, new Tuple<bool, Message>(true, null));

                Parallel.For(64800, 64900, (int x, ParallelLoopState loopState) =>
                    {
                        object[] finalData;
                        if (((Tuple<bool, Message>)WorkService.filesAnalyzed[msg.Info]).Item1)
                        {
                            //var key = 64897.ToString();

                            var key = x.ToString();

                            byte[] toto = Encoding.UTF8.GetBytes(EncryptOrDecrypt((String)msg.Data[0], key));
                            finalData = new Object[msg.Data.Length];
                            Array.Copy(msg.Data, finalData, msg.Data.Length);
                            finalData[0] = toto;

                            //Console.WriteLine(Encoding.UTF8.GetString(toto));

                            Interlocked.Increment(ref count);
                            Console.Write(" ");
                            Console.Write("\r {0}/100", count);

                            client.ServiceOperation(msg.ApplicationToken,
                                                    finalData,
                                                    msg.Info,
                                                    (int)msg.Operation,
                                                    (int)msg.Status,
                                                    msg.UserToken);
                        }
                        else
                        {
                            loopState.Stop();
                        }
                    });
            }
            else
            {
                var data = database.DataSets.FirstOrDefault(x => x.FileID == msg.Info);
                var mesg = new Message(msg.ApplicationToken, new Object[] { data.DecodedText }, msg.Info, msg.Operation, Status.Suceeded, msg.UserToken);
                WorkService.filesAnalyzed.Add(msg.Info, new Tuple<bool, Message>(true, mesg));
            }
            Console.WriteLine("\n Finished Loop");
            return ((Tuple<bool, Message>)WorkService.filesAnalyzed[msg.Info]).Item2;
        }

        private Message Authenticate(Message msg)
        {
            var database = new DataGenConnexion();
            String login = msg.Data[0] as String;
            String password = msg.Data[1] as String;
            Console.WriteLine("{0} asked for Authentication as {1}", msg.ApplicationToken, login);
            Guid generated = Guid.NewGuid();
            while (authenticatedClients.Contains(generated) || generated == Guid.Empty)
            {
                generated = Guid.NewGuid();
            }
            UserSet user = database.UserSets.FirstOrDefault(x => x.Login == login && x.Password == password);
            if (user != null)
            {
                msg.UserToken = generated.ToString();
                msg.Status = Status.Suceeded;
                WorkService.authenticatedClients.Add(generated);
                Console.WriteLine("{0} authenticated and got {1} as userToken", msg.ApplicationToken, msg.UserToken);
            }
            else
            {
                msg.Status = Status.Failed;
                msg.Info = "User not found";
                Console.WriteLine("{0} failed to authenticate", msg.ApplicationToken, msg.UserToken);
            }
            return msg;
        }

        /// <summary>
        /// Check if client is authenticated
        /// </summary>
        /// <param name="func"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private Message Authenticate(Func<Message, Message> func, Message msg)
        {
            if (!WorkService.authenticatedClients.Contains(Guid.Parse(msg.UserToken)))
            {
                Console.WriteLine("Client {0} tried to call {1} without being authenticated",
                                  msg.ApplicationToken,
                                  func.Method.ToString());
                return new Message(operation: Operations.GetDecrypted,
                      status: Status.Failed,
                      info: "User not authenticated",
                      data: new Object[0],
                      applicationToken: msg.ApplicationToken,
                      userToken: msg.UserToken);
            }
            else
            {
                return func(msg);
            }
        }
    }
}
