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

        private readonly List<Plane> _planes;

        public FlightControlTower(FlightContext context)
        {
            _context = context;
            _planes = context.GetPlanes();
            SetInitialWaypoints(_planes);

        }

        public void Run()
        {
            var ticker = new Timer();
            ticker.Elapsed += UpdatePlanes;
            ticker.Interval = 50; // in miliseconds
            ticker.Start();
        }

        private void UpdatePlanes(object sender, EventArgs e)
        {
            lock (_planes)
            {
                SyncCurrentPlanes();
                WaypointPlanes();
            }
        }

        private void SyncCurrentPlanes()
        {
            var newPlanes = _context.GetPlanes();
            var oldPlanes = _planes.Where(p => !newPlanes.Select(x => x.Id).Contains(p.Id)).ToList();
            foreach (var plane in oldPlanes)
            {
                _planes.Remove(plane);
            }
            foreach (var plane in newPlanes)
            {
                var existingPlane = _planes.SingleOrDefault(x => x.Id == plane.Id);
                if (existingPlane == null)
                {
                    SetInitialPlaneWaypoints(plane);
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

        private void WaypointPlanes()
        {
            foreach (var plane in _planes)
            {
                if (CheckNearbyPlanes(plane))
                {
                    plane.Waypoint = plane.Position;
                }
                else
                {
                    SetNewWaypoints(plane);
                    if (plane.RemainingWaypoints.Any())
                    {
                        plane.Waypoint = plane.RemainingWaypoints.Last();
                    }
                }
                _context.UpdatePlane(plane.Id, plane.Waypoint);
            }
            /*var currentPlanes = _planes.ToList();
           /* var landingPlane = currentPlanes.SingleOrDefault(x => x.Landing) ?? currentPlanes.OrderByDescending(x => x.Fuel).FirstOrDefault();
            if (landingPlane != null)
            {
                landingPlane.Landing = true;
                SetNewWaypoints(landingPlane);
                if (landingPlane.RemainingWaypoints.Any())
                {
                    landingPlane.Waypoint = landingPlane.RemainingWaypoints.Last();
                }
                _context.UpdatePlane(landingPlane.Id, landingPlane.Waypoint);
            }#1#
            foreach (var plane in currentPlanes)
            {
                /*if (!plane.Landing)
                {
                    plane.Waypoint = plane.Position;
                    _context.UpdatePlane(plane.Id, plane.Waypoint);
                }#1#
                // Waypoint update code.
                SetNewWaypoints(plane);
                if (plane.RemainingWaypoints.Any())
                {
                    plane.Waypoint = plane.RemainingWaypoints.Last();
                }
                _context.UpdatePlane(plane.Id, plane.Waypoint);
            }*/
        }

        private bool CheckNearbyPlanes(Plane plane)
        {
            var position = plane.Position;
            var shift = 100;
            var point1 = new Point(position.X - shift, position.Y + shift);
            var point2 = new Point(position.X + shift, position.Y + shift);
            var point3 = new Point(position.X + shift, position.Y - shift);
            var nearbyPlanes = false;
            var otherPlanes = _planes.Where(e => e.Id != plane.Id);
            foreach (var plane2 in otherPlanes)
            {
                if (plane2.Position.X > point1.X &&
                    plane2.Position.X < point2.X &&
                    plane2.Position.Y > point3.Y &&
                    plane2.Position.Y < point2.Y)
                {
                    if (plane.Fuel > plane2.Fuel)
                    {
                        nearbyPlanes = true;
                        break;
                    }
                }
            }
            return nearbyPlanes;
        }

        private void SetInitialWaypoints(List<Plane> planes)
        {
            planes.ForEach(SetInitialPlaneWaypoints);
        }

        /*
         * This returns a list of waypoints, based on the plane's original position
         * 
         * It isn't really tested - just wanted to hack some kind of waypoint mechanism together. Seems to work for most planes.
         * It's a very wide curve at the moment - you probably want to make it a bit tighter.
         */
        private void SetInitialPlaneWaypoints(Plane plane)
        {
            var waypoints = new List<Point>();
            
            // First point is the runway
            var waypoint0 = _context.Runway;
            var runwayIsInTopHalf = (_context.Runway.Y -_context.Boundary.Min.Y > 500);
            
            // Second point is 100 pixels back from the runway
            var waypoint1 = waypoint0 + (runwayIsInTopHalf ? new Point(0, 100) : new Point(0, -100));
            
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

            plane.RemainingWaypoints = waypoints;
        }

        // This method should probably belong to the plane or the Directions object
        /*
         * Returns true if it finds a waypoint, otherwise returns false
         */
        private void SetNewWaypoints(Plane plane)
        {
            if (plane.RemainingWaypoints.Any())
            {
                var lastWaypoint = plane.RemainingWaypoints.Last();
                // Detection just uses a square box at the moment - a bit crude
                if ((plane.Position.X - lastWaypoint.X < 30) && (plane.Position.Y - lastWaypoint.Y < 30))
                {
                    System.Diagnostics.Debug.WriteLine("Waypoint Reached! Coords: " + lastWaypoint.X + "," + lastWaypoint.Y);
                    plane.RemainingWaypoints.Remove(lastWaypoint);
                }
            }
        }
    }
}