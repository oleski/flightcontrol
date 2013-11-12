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
            ticker.Interval = 2000; // in miliseconds
            ticker.Start();
        }

        private void SyncCurrentPlanes()
        {
            var newPlanes = _context.GetPlanes();
            var oldPlanes = _planes.Where(p => !newPlanes.Select(x => x.Id).Contains(p.Id)).ToList();
            foreach (var plane in oldPlanes)
            {
                plane.Generation++;
                if (plane.Generation > 10)
                    _planes.Remove(plane);
            }
            foreach (var plane in newPlanes)
            {
                var existingPlane = _planes.SingleOrDefault(x => x.Id == plane.Id);
                if (existingPlane == null)
                {
                    plane.RemainingWaypoints = getInitialPlaneWaypoints(plane);
                    _planes.Add(plane);
                }
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

        /*
         * This returns a list of waypoints, based on the plane's original position
         * 
         * It isn't really tested - just wanted to hack some kind of waypoint mechanism together. Seems to work for most planes.
         * It's a very wide curve at the moment - you probably want to make it a bit tighter.
         */
        public List<Point> getInitialPlaneWaypoints(Plane plane)
        {
            List<Point> waypoints = new List<Point>();
            // First point is the runway
            Point waypoint0 = _context.Runway;
            bool runwayIsInTopHalf = (_context.Runway.Y -_context.Boundary.Min.Y > 500);
            // Second point is 100 pixels back from the runway
            Point waypoint1 = waypoint0 + (runwayIsInTopHalf ? new Point(0, 100) : new Point(0, -100));
            // Third point is 100 pixels back from the last point, and 150 pixels towards the side that the plane starts on
            Point waypoint2;
            // Fourth point is 150 pixels to the side from the last point, and on the side that the plane started on
            Point waypoint3;
            if (plane.Position.X > 500)
            {
                waypoint2 = waypoint1 + (runwayIsInTopHalf ? new Point(150, 100) : new Point(150, -100));
                waypoint3 = waypoint2 + (runwayIsInTopHalf ? new Point(150, 0) : new Point(150, 0));
            }
            else
            {
                waypoint2 = waypoint1 + (runwayIsInTopHalf ? new Point(-150, 100) : new Point(-150, -100));
                waypoint3 = waypoint2 + (runwayIsInTopHalf ? new Point(-150, 0) : new Point(-150, 0));
            }
            System.Diagnostics.Debug.WriteLine("Plane: " + plane.Id + ", waypoint0: " + waypoint0.X + "," + waypoint0.Y + ", waypoint1: " + waypoint1.X + "," + waypoint1.Y + ", waypoint2: " + waypoint2.X + "," + waypoint2.Y);
            waypoints.Add(waypoint0);
            waypoints.Add(waypoint1);
            waypoints.Add(waypoint2);
            return waypoints;
        }

        // This method should probably belong to the plane or the Directions object
        /*
         * Returns true if it finds a waypoint, otherwise returns false
         */
        private void updatePlaneWaypoints(Plane plane)
        {
            if (plane.RemainingWaypoints.Any())
            {
                Point lastWaypoint = plane.RemainingWaypoints.Last();
                // Detection just uses a square box at the moment - a bit crude
                if ((plane.Position.X - lastWaypoint.X < 30) && (plane.Position.Y - lastWaypoint.Y < 30))
                {
                    System.Diagnostics.Debug.WriteLine("Waypoint Reached! Coords: " + lastWaypoint.X + "," + lastWaypoint.Y);
                    plane.RemainingWaypoints.Remove(lastWaypoint);
                }
            }
        }

        private void WaypointPlanes()
        {
            var currentPlanes = _planes.ToList();
            foreach (var plane in currentPlanes)
            {
                // Waypoint update code.
                updatePlaneWaypoints(plane);
                if (plane.RemainingWaypoints.Any())
                {
                    plane.Waypoint = plane.RemainingWaypoints.Last();
                }
                _context.UpdatePlane(plane.Id, plane.Waypoint);
            }        
        }

        private void UpdatePlanes(object sender, EventArgs e)
        {
            SyncCurrentPlanes();
            WaypointPlanes();
        }
    }
}