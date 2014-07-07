
using GeneratorServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using GeneratorService.JavaServer;
using System.Threading;
using System.Collections;
using System.Text.RegularExpressions;

namespace GeneratorService
{
    public class WorkService : IWorkService
    {

        #region connection to Java Service

        #endregion
        private static readonly Hashtable filesAnalyzed = new Hashtable();
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
            /**
             * [0] texte décodé validé
             * [1] nom fichier
             * [2] mail
             * [3] pdf indice confiance
             */
            WorkService.filesAnalyzed[msg.Info] = new Tuple<bool, Message>(false, msg);
            var database = new DataGenConnexion();
            var data = new DataSet();
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
        }

        private Message GetDecrypted(Message msg)
        {
            DataGenConnexion database = new DataGenConnexion();
            DataSet set = database.DataSets.FirstOrDefault(x => x.Id == (int)msg.Data[0]);
            database = null;
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


        string EncryptOrDecrypt(string text, string key)
        {
            var result = new StringBuilder();

            for (int c = 0; c < text.Length; c++)
                result.Append((char)((uint)text[c] ^ (uint)key[c % key.Length]));

            return result.ToString();
        }

        private Message Decode(Message msg)
        {
            var client = new GeneratorClient("GeneratorPort");
            int count = 0;

            if (!WorkService.filesAnalyzed.Contains(msg.Info))
            {
                WorkService.filesAnalyzed.Add(msg.Info, new Tuple<bool, Message>(true, null));

                Parallel.For(0, 1, (int x, ParallelLoopState loopState) =>
                    {
                        object[] finalData;
                        if (((Tuple<bool, Message>)WorkService.filesAnalyzed[msg.Info]).Item1)
                        {
                            var key = 64897.ToString();

                            byte[] toto = Encoding.UTF8.GetBytes(EncryptOrDecrypt((String)msg.Data[0], key));
                            finalData = new Object[msg.Data.Length];
                            Array.Copy(msg.Data, finalData, msg.Data.Length);
                            finalData[0] = toto;

                            Console.WriteLine(Encoding.UTF8.GetString(toto));
                            Interlocked.Increment(ref count);

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

            return ((Tuple<bool, Message>)WorkService.filesAnalyzed[msg.Info]).Item2;
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
