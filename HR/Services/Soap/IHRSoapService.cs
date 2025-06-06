using System.ServiceModel;

namespace SystemHR.Services.Soap
{
    [ServiceContract]
    public interface IHRSoapService
    {
        [OperationContract]
        string Ping();
    }
}
