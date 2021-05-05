using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiffingAPI
{
    public class Data
    {
        public static ConcurrentDictionary<int, DiffModel> DiffData { get; set; }
    }

    public class DiffModel
    {
        public byte[] LeftData { get; set; }
        public byte[] RightData { get; set; }
    }
}
