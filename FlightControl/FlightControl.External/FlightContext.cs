namespace FlightControl.External
{
    using System.Collections.Generic;

    using FlightControl.Model;

    public class FlightContext
    {
        private readonly FlightControlProxy _proxy;

        // Existing session
        public FlightContext(FlightControlProxy proxy, SessionInfo session)
        {
            _proxy = proxy;
            Session = session;
            Initialize();
        }

        // New sesssion
        public FlightContext(FlightControlProxy proxy) : this(proxy, proxy.CreateNewSession())
        {
        }

        private void Initialize()
        {
            var flightInfo = _proxy.GetFlightInfo(Session.Token);
            Boundary = flightInfo.Boundary;
            Runway = flightInfo.Runway;
        }

        

        public SessionInfo Session { get; private set; }

        public Boundary Boundary { get; private set; }

        public Point Runway { get; set; }

        public List<Plane> GetPlanes()
        {
            return _proxy.GetFlightInfo(Session.Token).Planes;
        }

        public void UpdatePlane(int id, Point waypoint)
        {
            _proxy.UpdatePlane(Session.Token, id, waypoint);
        }
    }
}