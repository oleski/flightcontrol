namespace FlightControl.Services.Service.Data
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Boundary
    {
        [DataMember(Name = "min")]
        public Point Min { get; set; }

        [DataMember(Name = "max")]
        public Point Max { get; set; } 
    }
}