using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.External
{
    using System.IO;
    using System.Runtime.Serialization.Json;

    public static class JsonSerializer
    {
        public static string ToJson<T>(T data)
        {
            var serializer
                        = new DataContractJsonSerializer(typeof(T));

            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, data);
                return Encoding.Default.GetString(ms.ToArray());
            }
        }
    }
}
