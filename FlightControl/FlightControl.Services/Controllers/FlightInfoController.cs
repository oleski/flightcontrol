namespace FlightControl.Services.Controllers
{
    using System.Web.Http;

    using FlightControl.Services.Properties;
    using FlightControl.Services.Service;
    using FlightControl.Services.Service.Data;

    public class FlightInfoController : ApiController
    {
        public FlightInfo Get(string token)
        {
            var proxy = new FlightControlProxy(Settings.Default.BaseUrl);
            var flightInfo = proxy.GetResult(token);

            return flightInfo;
        }
    }
}