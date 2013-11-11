namespace FlightControl.Services.Service
{
    using System.Collections.Generic;
    using Data;
    using Properties;

    public static class FlightContext
    {
        private static FlightControlProxy FlightControlProxy { get; set; }

        private static string Token { get; set; }

        public static Boundary Boundary { get; private set; }

        static FlightContext()
        {
            FlightControlProxy = new FlightControlProxy(Settings.Default.BaseUrl);
            Token = FlightControlProxy.GetNewSession().Token;
            var info = FlightControlProxy.GetResult(Token);
            Boundary = info.Boundary;
        }

        public static List<Plane> GetPlanes()
        {
            return FlightControlProxy.GetResult(Token).Planes;
        }

        public static void UpdatePlane(int id, Point waypoint)
        {
            FlightControlProxy.UpdatePlane(Token);
        }
    }
}