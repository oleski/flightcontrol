namespace FlightControl.Tests
{
    using System;
    using System.Threading;

    using FlightControl.Core;
    using FlightControl.External;
    using FlightControl.Tests.Properties;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FlightControlTowerShould
    {
        public void Run()
        {
            var flightControlProxy = new FlightControlProxy(Settings.Default.BaseUrl);
            var flightContext = new FlightContext(flightControlProxy);
            var flightControlTower = new FlightControlTower(flightContext.GetPlanes);
            flightControlTower.Run();

            Thread.Sleep(TimeSpan.FromMinutes(5));
        }
    }
}
