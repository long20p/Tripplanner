using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Tripplanner.Business.Utils
{
    public class Serializer : ISerializer
    {
        public string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T Deserialize<T>(string flat)
        {
            return JsonConvert.DeserializeObject<T>(flat);
        }
    }
}
