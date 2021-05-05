using DiffingLibrary;
using System;
using System.Collections.Generic;

namespace Diffing.TestData
{
    public static class TestData
    {
        public static readonly byte[] X = { 0, 0, 0, 0 };
        public static readonly byte[] Y = { 1, 0, 1, 1 };
        public static readonly byte[] W = { 1, 1, 1, 0 };
        public static readonly byte[] Z = { 1, 0, 1, 1, 1 };
        public static readonly List<Model.Diffs> expectedDiffsXY = new List<Model.Diffs> {
            new Model.Diffs { offset = 0, length = 1 },
            new Model.Diffs { offset = 2, length = 2 }
        };
        public static readonly List<Model.Diffs> expectedDiffsXW = new List<Model.Diffs> {
            new Model.Diffs { offset = 0, length = 3 }
        };
        public static readonly List<Model.Diffs> expectedDiffsYW = new List<Model.Diffs> {
            new Model.Diffs { offset = 1, length = 1 },
            new Model.Diffs { offset = 3, length = 1 }
        };
    }
}
