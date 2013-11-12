namespace FlightControl.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Timers;
    using External;
    using Model;

    public class FlightControlTower
    {
        private readonly FlightContext _context;

        private readonly IList<Plane> _planes;

        public FlightControlTower(FlightContext context)
        {
            _context = context;
            _planes = context.GetPlanes();
        }

        public void Run()
        {
            var ticker = new Timer();
            ticker.Elapsed += UpdatePlanes;
            ticker.Elapsed += WaypointPlanes;
            ticker.Interval = 2000; // in miliseconds
            ticker.Start();
        }

        private void WaypointPlanes(object sender, EventArgs e)
        {
            foreach (var plane in _planes)
            {
                var waypoint = _context.Runway;
                plane.Waypoint = waypoint;
                _context.UpdatePlane(plane.Id, waypoint);
            }        
        }

        private void UpdatePlanes(object sender, EventArgs e)
        {
            var newPlanes = _context.GetPlanes();
            foreach (var plane in newPlanes)
            {
                var existingPlane = _planes.SingleOrDefault(x => x.Id == plane.Id);
                if (existingPlane == null)
                    _planes.Add(plane);
                else
                {
                    existingPlane.UpdateProperties(
                        plane.Type, 
                        plane.Position, 
                        plane.Rotation, 
                        plane.Id, 
                        plane.Name, 
                        plane.Graphic, 
                        plane.Speed, 
                        plane.Fuel, 
                        plane.Points, 
                        plane.Penalty);
                }
            }
        }
    }
}