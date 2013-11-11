namespace FlightControl.Services.Service.Data
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Point
    {
        [DataMember(Name = "x")]
        public double X { get; set; }

        [DataMember(Name = "y")]
        public double Y { get; set; } 
    }
}