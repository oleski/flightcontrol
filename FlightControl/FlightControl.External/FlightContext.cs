namespace FlightControl.External
{
    using System.Collections.Generic;

    using FlightControl.Model;

    public class FlightContext
    {
        private readonly FlightControlProxy _proxy;

        public FlightContext(FlightControlProxy proxy)
        {
            _proxy = proxy;
            Token = _proxy.CreateNewSession().Token;
            Boundary = _proxy.GetFlightInfo(Token).Boundary;
            Runway = _proxy.GetFlightInfo(Token).Runway;
        }

        public string Token { get; private set; }

        public Boundary Boundary { get; private set; }

        public Point Runway { get; set; }

        public List<Plane> GetPlanes()
        {
            return _proxy.GetFlightInfo(Token).Planes;
        }

        public void UpdatePlane(int id, Point waypoint)
        {
            _proxy.UpdatePlane(Token, id, waypoint);
        }
    }
}