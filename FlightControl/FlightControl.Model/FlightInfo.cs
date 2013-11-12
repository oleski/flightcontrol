namespace FlightControl.Model
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class FlightInfo
    {
        [DataMember(Name = "boundary")]
        public Boundary Boundary { get; set; }

        /*"runway":{
              "x":550,
              "y":450
           }*/

        [DataMember(Name = "runway")]
        public Point Runway { get; set; }

        [DataMember(Name = "objects")]
        public List<Plane> Planes { get; set; }
    }
}