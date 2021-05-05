using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace DiffingLibrary
{
    public class HttpModel
    {
        public class PutData
        {
            public string data { get; set; }

            public PutData() { }
            public PutData(byte[] bytes)
            {
                data  = Convert.ToBase64String(bytes);
            }

            public byte[] GetDecoded() => Convert.FromBase64String(data);
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class GetData
        {
            [JsonConverter(typeof(StringEnumConverter))]

            public Model.DiffTypes diffResultType { get; set; }
            public List<Model.Diffs> diffs { get; set; }

        }
    }
}
