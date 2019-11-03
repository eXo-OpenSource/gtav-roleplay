using System.Collections.Generic;
using AltV.Net.Data;
using Newtonsoft.Json;

namespace server.Extensions
{
    internal static class VectorExtensions
    {
        public static string Serialize(this Position vector)
        {
            var array = new List<float> {vector.X, vector.Y, vector.Z};
            return JsonConvert.SerializeObject(array);
        }

        public static Position DeserializeVector(this string array)
        {
            var obj = JsonConvert.DeserializeObject<List<float>>(array);
            return new Position(obj[0], obj[1], obj[2]);
        }
    }
}