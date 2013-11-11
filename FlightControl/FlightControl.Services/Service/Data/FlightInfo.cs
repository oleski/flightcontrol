namespace FlightControl.Services.Service.Data
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class FlightInfo
    {
        [DataMember(Name = "boundary")]
        public Boundary Boundary { get; set; }

        [DataMember(Name = "objects")]
        public List<Plane> Planes { get; set; }
    }
}