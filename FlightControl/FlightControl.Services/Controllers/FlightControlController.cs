using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlightControl.Services.Controllers
{
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;

    using Newtonsoft.Json.Linq;

    public class FlightControlController : ApiController
    {
        public string Get(int id)
        {
            var proxy = new FlightControlProxy("http://challenge.hacktivate.me:3000");
            var sessionId = proxy.GetNewSessionId();

            return sessionId;
        }
    }

    class FlightControlProxy
    {
        private readonly Uri _baseUri;

        public FlightControlProxy(string baseUrl)
        {
            _baseUri = new Uri(baseUrl);
        }

        public string GetNewSessionId()
        {
            var newSessionUri = new Uri(_baseUri, "/new-session");
            var request = WebRequest.Create(newSessionUri);
            var response = request.GetResponse();

            using (var responseStream = response.GetResponseStream())
            using (var reader = new StreamReader(responseStream))
            {
                var json = JObject.Parse(reader.ReadToEnd());
                var newSessionId = json["token"].ToObject<string>();

                return newSessionId;
            }
        }

        class NewSession
        {
            public string Token2 { get; set; }
        }
    }
}
