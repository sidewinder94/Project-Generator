using System.ServiceModel;

namespace GeneratorServiceContracts
{
    [ServiceContract]
    public interface IWorkService
    {
        [OperationContract]
        Message ServiceOperation(Message msg);

    }
}
