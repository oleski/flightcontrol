namespace FlightControl.Services.Service
{
    using System;

    using FlightControl.Services.Code;
    using FlightControl.Services.Service.Data;

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
            var newSession = uri.GerRequestResult<SessionInfo>();
            
            return newSession;
        }

        public FlightInfo GetResult(string token)
        {
            if (token == null)
            {
                throw new ArgumentNullException("token");
            }

            var uri = new Uri(_baseUri, "/get?token=" + token);
            var flightInfo = uri.GerRequestResult<FlightInfo>();

            return flightInfo;
        }
    }
}