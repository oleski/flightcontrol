namespace FlightControl.Services.Service
{
    using System;
    using Code;
    using Data;

    public class FlightControlProxy
    {
        private readonly Uri _baseUri;

        public FlightControlProxy(string baseUrl)
        {
            _baseUri = new Uri(baseUrl);
        }

        public SessionInfo GetNewSession()
        {
            var uri = new Uri(_baseUri, "/new-session");
            var newSession = uri.GetRequestResult<SessionInfo>();
            
            return newSession;
        }

        public FlightInfo GetResult(string token)
        {
            if (token == null)
            {
                throw new ArgumentNullException("token");
            }

            var uri = new Uri(_baseUri, "/get?token=" + token);
            var flightInfo = uri.GetRequestResult<FlightInfo>();

            return flightInfo;
        }

        public void UpdatePlane(string token)
        {
            var uri = new Uri(_baseUri, "/post");
        }
    }
}