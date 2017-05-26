using NUnit.Framework;

namespace TeachersDiary.Common.Extensions.Tests
{
    [TestFixture]
    public class DoubleExtensionsTests
    {
        [TestCase(2.3333333333333335, "2 1/3")]
        [TestCase(12.666666666666666, "12 2/3")]
        [TestCase(24, "24")]
        [TestCase(0.3333333333333335, "1/3")]
        public void FromDoubleToFractionNumber_WithValidParam_ShouldReturnFraction(double @double, string fraction)
        {
            var actualResult = @double.ToFractionNumber();

            Assert.AreEqual(fraction, actualResult);
        }
    }
}
