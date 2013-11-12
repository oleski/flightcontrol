namespace FlightControl.WebApi.Controllers
{
    using System.Web.Http;

    using FlightControl.External;
    using FlightControl.Model;

    using global::FlightControl.WebApi.Properties;

    public class NewSessionController : ApiController
    {
        public SessionInfo Get()
        {
            var proxy = new FlightControlProxy(Settings.Default.BaseUrl);
            var session = proxy.CreateNewSession();

            return session;
        }
    }
}
