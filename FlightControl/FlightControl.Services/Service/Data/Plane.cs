namespace FlightControl.Services.Service.Data
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Plane
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "position")]
        public Point Position { get; set; }

        [DataMember(Name = "rotation")]
        public double Rotation { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "graphic")]
        public string Graphic { get; set; }
        
        [DataMember(Name = "speed")]
        public int Speed { get; set; } /* Units per second */

        [DataMember(Name = "fuel")]
        public int Fuel { get; set; }

        [DataMember(Name = "points")]
        public int Points { get; set; }

        [DataMember(Name = "penalty")]
        public int Penalty { get; set; }

        public void UpdateProperties(string type, Point position, double? rotation, int? id, string name,
            string graphic, int? speed, int? fuel, int? points, int? penalty)
        {
            Type = type ?? Type;
            Position = position ?? Position;
            Rotation = rotation ?? Rotation;
            Id = id ?? Id;
            Name = name ?? Name;
            Graphic = graphic ?? Graphic;
            Speed = speed ?? Speed;
            Fuel = fuel ?? Fuel;
            Points = points ?? Points;
            Penalty = penalty ?? Penalty;
        }
    }
}