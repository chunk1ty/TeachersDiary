using NUnit.Framework;

namespace TeachersDiary.Common.Extensions.Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestCase("2 1/3", 2.3333333333333335)]
        [TestCase("12 2/3", 12.666666666666666)]
        [TestCase("24", 24)]
        public void FractionToDouble_WithValidParam_ShouldReturnDouble(string fraction, double @double)
        {
            var actualResult = fraction.FractionToDoubleNumber();

            Assert.AreEqual(@double, actualResult);
        }
    }
}
