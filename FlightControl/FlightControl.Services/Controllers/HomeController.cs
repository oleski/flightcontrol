using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightControl.Services.Controllers
{
    using System.Net;
    using FlightControl;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Run()
        {
            new FlightControlTower().Run();
            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }
    }
}
