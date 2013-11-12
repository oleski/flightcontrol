namespace FlightControl.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Direction
    {
        [DataMember(Name = "plane_id")]
        public int PlaneId { get; set; }

        [DataMember(Name = "waypoint")]
        public Point Waypoint { get; set; }
    }
}
