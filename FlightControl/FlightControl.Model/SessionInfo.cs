﻿namespace FlightControl.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class SessionInfo
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }
    }
}