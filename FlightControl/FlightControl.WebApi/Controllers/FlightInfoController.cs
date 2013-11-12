namespace FlightControl.WebApi.Controllers
{
    using System.Web.Http;

    using FlightControl.External;
    using FlightControl.Model;

    using global::FlightControl.WebApi.Properties;

    public class FlightInfoController : ApiController
    {
        public FlightInfo Get(string token)
        {
            var proxy = new FlightControlProxy(Settings.Default.BaseUrl);
            var flightInfo = proxy.GetFlightInfo(token);

            return flightInfo;
        }
    }
}