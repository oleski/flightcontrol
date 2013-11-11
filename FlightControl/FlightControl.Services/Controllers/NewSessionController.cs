using System.Web.Http;

namespace FlightControl.Services.Controllers
{
    using FlightControl.Services.Properties;
    using FlightControl.Services.Service;
    using FlightControl.Services.Service.Data;

    public class NewSessionController : ApiController
    {
        public SessionInfo Get()
        {
            var proxy = new FlightControlProxy(Settings.Default.BaseUrl);
            var session = proxy.GetNewSession();

            return session;
        }
    }
}
