using System.Web.Http;

namespace FlightControl.Services.Controllers
{
    using Properties;
    using Service;
    using Service.Data;

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
