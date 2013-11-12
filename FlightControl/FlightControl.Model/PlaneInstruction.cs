namespace FlightControl.Model
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class PlaneInstruction
    {
        [DataMember(Name = "directions")]
        public List<Direction> Directions { get; set; }

        [DataMember(Name = "token")]
        public string Token { get; set; }
    }
}
