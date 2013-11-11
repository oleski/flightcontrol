namespace FlightControl.Services.FlightControl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Timers;
    using Service;
    using Service.Data;

    public class FlightControlTower
    {
        private FlightControlProxy FlightControlProxy { get; set; }

        private string Token { get; set; }

        private Boundary Boundary { get; set; }

        private List<Plane> Planes { get; set; } 

        public FlightControlTower()
        {
            FlightControlProxy =  new FlightControlProxy("http://challenge.hacktivate.me:3000");
            Token = FlightControlProxy.GetNewSession().Token;
            var info = FlightControlProxy.GetResult(Token);
            Boundary = info.Boundary;
            Planes = info.Planes;
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
            var newPlanes = FlightControlProxy.GetResult(Token).Planes;
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