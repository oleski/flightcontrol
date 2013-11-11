using System.Web.Http;

namespace FlightControl.Services.Controllers
{
    using FlightControl.Services.Service;
    using FlightControl.Services.Service.Data;

    public class NewSessionController : ApiController
    {
        public SessionInfo Get()
        {
            var proxy = new FlightControlProxy("http://challenge.hacktivate.me:3000");
            var session = proxy.GetNewSession();

            return session;
        }
    }
}
