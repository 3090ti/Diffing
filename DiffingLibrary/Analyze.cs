using System.Collections.Generic;
using System.Linq;

namespace DiffingLibrary
{
    public class Analyze
    {
        /// <summary>
        /// Compare 2 byte arrays & return diff type
        /// </summary>
        /// <param name="x">>First byte array</param>
        /// <param name="y">Second byte array</param>
        /// <returns>Returns type of diff</returns>
        public static Model.DiffTypes GetDiffType(byte[] x, byte[] y)
        {
            if (x.SequenceEqual(y))
                return Model.DiffTypes.Equals;
            if (x.Length != y.Length)
                return Model.DiffTypes.SizeDoNotMatch;

            return Model.DiffTypes.ContentDoNotMatch;
        }


        /// <summary>
        /// Calculate diffs between 2 byte arrays
        /// </summary>
        /// <param name="x">First byte array</param>
        /// <param name="y">Second byte array</param>
        /// <returns>Returns a list of diffs with offset & length</returns>
        public static List<Model.Diffs> GetListOfDiffs(byte[] x, byte[] y)
        {
            List<Model.Diffs> diffList = new List<Model.Diffs>();

            int offset = -1;
            int length = 0;

            for (int i = 0; i < x.Length + 1; i++)
            {
                if (i < x.Length && x[i] != y[i])
                {
                    if (offset == -1)
                        offset = i;
                    length += 1;
                }
                else
                {
                    if (offset != -1)
                    {
                        diffList.Add(new Model.Diffs
                        {
                            offset = offset,
                            length = length
                        });
                    }
                    offset = -1;
                    length = 0;
                }
            }
            return diffList;
        }
    }
}