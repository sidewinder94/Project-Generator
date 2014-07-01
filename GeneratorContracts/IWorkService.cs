using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace GeneratorServiceContracts
{
    [ServiceContract]
    public interface IWorkService
    {
        [OperationContract]
        void CompletedOperation(Message msg);
        [OperationContract]
        Message ServiceOperation(Message msg);

    }
}
