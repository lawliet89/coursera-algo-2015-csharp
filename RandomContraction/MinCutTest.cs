using NUnit.Framework;

namespace RandomContraction
{
    [TestFixture]
    public class MinCutTest
    {
        [TestCase("1.txt", Result = 3)]
        [TestCase("2.txt", Result = 2)]
        public long CalculatesMinCutCorrectly(string name)
        {
            var path = $"Fixtures/KargerMinCut/{name}";
            return MinCut.MinimumCutFromFile(path);
        }
    }
}
