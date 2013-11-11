namespace FlightControl.Services.FlightControl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Timers;
    using Properties;
    using Service;
    using Service.Data;

    public class FlightControlTower
    {
        private List<Plane> Planes { get; set; } 

        public FlightControlTower()
        {
            Planes = FlightContext.GetPlanes();
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
            var newPlanes = FlightContext.GetPlanes();
            foreach (var plane in newPlanes)
            {
                var existingPlane = Planes.SingleOrDefault(x => x.Id == plane.Id);
                if (existingPlane == null)
                    Planes.Add(plane);
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