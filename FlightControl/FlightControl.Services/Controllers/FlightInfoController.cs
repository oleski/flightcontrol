namespace FlightControl.Services.Controllers
{
    using System.Web.Http;

    using Service;
    using Service.Data;

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