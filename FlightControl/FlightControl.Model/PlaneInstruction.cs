namespace FlightControl.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class PlaneInstruction
    {
        [DataMember(Name = "directions")]
        public Direction Directions { get; set; }

        [DataMember(Name = "token")]
        public string Token { get; set; }
    }
}
