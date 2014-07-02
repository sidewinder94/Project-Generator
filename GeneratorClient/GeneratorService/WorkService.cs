using GeneratorServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorService
{
    public class WorkService : IWorkService
    {
        public void CompletedOperation(Message msg)
        {
            throw new NotImplementedException();
        }

        public Message ServiceOperation(Message msg)
        {
            switch (msg.Operation)
            {
                case Operations.Authenticate:
                    break;
                case Operations.Decode:
                    break;
                default:
                    break;
            }

            throw new NotImplementedException();
        }
    }
}
