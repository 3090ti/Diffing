using DeepEqual.Syntax;
using Xunit;
using Diffing.TestData;

namespace Diffing.UnitTests
{
    public class UnitTests
    {
        [Fact]
        public void GetDiffType_ShouldBe_Equal()
        {
            var expected = DiffingLibrary.Model.DiffTypes.Equals;

            var actual = DiffingLibrary.Analyze.GetDiffType(TestData.TestData.X, TestData.TestData.X);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDiffType_ShouldBe_SizeDoNotMatch()
        {
            var expected = DiffingLibrary.Model.DiffTypes.SizeDoNotMatch;

            var actual = DiffingLibrary.Analyze.GetDiffType(TestData.TestData.X, TestData.TestData.Z);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDiffType_ShouldBe_ContentDoNotMatch()
        {
            var expected = DiffingLibrary.Model.DiffTypes.ContentDoNotMatch;

            var actual = DiffingLibrary.Analyze.GetDiffType(TestData.TestData.X, TestData.TestData.Y);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDiffs_ShouldEqual_PrecalculatedDiffs()
        {
            DiffingLibrary.Analyze.GetListOfDiffs(TestData.TestData.X, TestData.TestData.Y).ShouldDeepEqual(TestData.TestData.expectedDiffsXY);
            DiffingLibrary.Analyze.GetListOfDiffs(TestData.TestData.X, TestData.TestData.W).ShouldDeepEqual(TestData.TestData.expectedDiffsXW);
            DiffingLibrary.Analyze.GetListOfDiffs(TestData.TestData.Y, TestData.TestData.W).ShouldDeepEqual(TestData.TestData.expectedDiffsYW);
        }
    }
}
