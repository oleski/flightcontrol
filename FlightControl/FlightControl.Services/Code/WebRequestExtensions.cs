using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControl.Services.Code
{
    using System.Net;
    using System.Runtime.Serialization.Json;

    public static class WebRequestExtensions
    {
        public static T GetRequestResult<T>(this Uri requestUri)
        {
            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            var serializer = new DataContractJsonSerializer(typeof(T));

            using (var responseStream = response.GetResponseStream())
            {
                if (responseStream == null)
                {
                    throw new ApplicationException("Response sream is null");
                }

                var @object = serializer.ReadObject(responseStream);

                var result = (T)@object;

                return result;
            }
        }
    }
}