namespace FlightControl.External
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using FlightControl.Model;

    public class FlightControlProxy
    {
        private readonly Uri _baseUri;

        public FlightControlProxy(string baseUrl)
        {
            _baseUri = new Uri(baseUrl);
        }

        public SessionInfo CreateNewSession()
        {
            var uri = new Uri(_baseUri, "/new-session");
            var newSession = uri.GetRequestResult<SessionInfo>();
            
            return newSession;
        }

        public FlightInfo GetFlightInfo(string token)
        {
            if (token == null)
            {
                throw new ArgumentNullException("token");
            }

            var uri = new Uri(_baseUri, "/get?token=" + token);
            var flightInfo = uri.GetRequestResult<FlightInfo>();

            return flightInfo;
        }

        public void UpdatePlane(string token, int id, Point waypoint)
        {
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var data = new PlaneInstruction
                {
                    Directions = new List<Direction>{
                        new Direction
                            {
                                PlaneId = id,
                                Waypoint = waypoint
                            }
                    },
                    Token = token
                };
                
                var result = client.UploadString(_baseUri + "post?token=" + token, "POST", JsonSerializer.ToJson(data));
            }
        }
    }
}