namespace FlightControl.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Plane
    {
        /*{
            "position":{
            "x":135.16695992449604,
            "y":575.8072120196498
            },
            "rotation":-17.812591521651484,
            "id":1003,
            "type":"plane",
            "fuel":100,
            "waypoint":null,
            "name":"777-300ER",
            "graphic":"plane2.png",
            "speed":36,
            "points":10,
            "penalty":5,
            "turn_speed":30,
            "collision_radius":50,
            "graphic_full_path":"player1/plane2.png"
        }*/

        [DataMember(Name = "position")]
        public Point Position { get; set; }

        [DataMember(Name = "rotation")]
        public double Rotation { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "fuel")]
        public int Fuel { get; set; }

        [DataMember(Name = "waypoint")]
        public Point Waypoint { get; set; }
        
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "graphic")]
        public string Graphic { get; set; }
        
        [DataMember(Name = "speed")]
        public int Speed { get; set; } /* Units per second */

        [DataMember(Name = "points")]
        public int Points { get; set; }

        [DataMember(Name = "penalty")]
        public int Penalty { get; set; }

        [DataMember(Name = "turn_speed")]
        public int TurnSpeed { get; set; }

        [DataMember(Name = "collision_radius")]
        public int CollisionRadius { get; set; }

        [DataMember(Name = "graphic_full_path")]
        public string GraphicFullPath { get; set; }

        public void UpdateProperties(
            string type, 
            Point position, 
            double? rotation, 
            int? id, 
            string name,
            string graphic, 
            int? speed, 
            int? fuel, 
            int? points, 
            int? penalty)
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