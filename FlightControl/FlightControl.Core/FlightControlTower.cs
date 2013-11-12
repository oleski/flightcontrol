namespace FlightControl.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Timers;

    using FlightControl.Model;

    public class FlightControlTower
    {
        private readonly Func<IList<Plane>> _getPlanes;

        private readonly IList<Plane> _planes;

        public FlightControlTower(Func<IList<Plane>> getPlanes)
        {
            _getPlanes = getPlanes;
            _planes = _getPlanes.Invoke();
        }

        public void Run()
        {
            var ticker = new Timer();
            ticker.Elapsed += UpdatePlanes;
            ticker.Interval = 2000; // in miliseconds
            ticker.Start();
        }

        private void UpdatePlanes(object sender, EventArgs e)
        {
            var newPlanes = _getPlanes.Invoke();
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