namespace FlightControl.Tests
{
    using FlightControl.External;
    using FlightControl.Tests.Properties;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FlightControlProxyShould
    {
        [TestMethod]
        public void CreateNewSessionAndGetInfo()
        {
            var proxy = new FlightControlProxy(Settings.Default.BaseUrl);
            var session = proxy.CreateNewSession();
            var flightInfo = proxy.GetFlightInfo(session.Token);

            Assert.IsNotNull(flightInfo);
        }
    }
}
