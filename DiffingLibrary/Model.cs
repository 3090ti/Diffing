using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffingLibrary
{
    public class Model
    {
        public enum DiffTypes
        {
            Equals,
            ContentDoNotMatch,
            SizeDoNotMatch
        }

        public class Diffs
        {
            public int offset { get; init; }
            public int length { get; init; }
        }
    }
}
