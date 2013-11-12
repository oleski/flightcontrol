namespace FlightControl.WebApi.Controllers
{
    using System.Net;
    using System.Web.Mvc;

    using FlightControl.Core;
    using FlightControl.External;
    using FlightControl.WebApi.Properties;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Run()
        {
            var flightControlProxy = new FlightControlProxy(Settings.Default.BaseUrl);
            var flightContext = new FlightContext(flightControlProxy);
            var flightControlTower = new FlightControlTower(flightContext.GetPlanes);
            flightControlTower.Run();

            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }
    }
}
