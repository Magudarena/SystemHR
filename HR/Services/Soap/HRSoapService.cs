using Microsoft.AspNetCore.Mvc;


namespace SystemHR.Services.Soap
{
    public class HRSoapService : IHRSoapService
    {
        public string Ping() => "Działa";
    }
}
