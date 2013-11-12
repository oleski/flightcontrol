namespace FlightControl.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Boundary
    {
        /*{
            "min":{
                "x":50,
                "y":50
            },
            "max":{
                "x":1050,
                "y":1050
            }
        }*/

        [DataMember(Name = "min")]
        public Point Min { get; set; }

        [DataMember(Name = "max")]
        public Point Max { get; set; } 
    }
}