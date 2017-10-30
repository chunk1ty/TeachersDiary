using System;
using NUnit.Framework;

namespace TeachersDiary.Common.Extensions.Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestCase("2 1/3", 2.3333333333333335)]
        [TestCase("12 2/3", 12.666666666666666)]
        [TestCase("24", 24)]
        [TestCase("1/3", 0.33333333333333331)]
        [TestCase("2/3", 0.66666666666666663)]
        public void FractionToDouble_WithValidParam_ShouldReturnDouble(string fraction, double @double)
        {
            var actualResult = fraction.ToDoubleNumber();

            Assert.AreEqual(@double, actualResult);
        }

        [TestCase("2 222")]
        [TestCase("assds")]
        [TestCase("2 sds/2")]
        [TestCase("2 1/dsds")]
        [TestCase("2 1/2")]
        [TestCase("2 3/3")]
        [TestCase("2.2")]
        public void FractionToDouble_WithInvalidFractionNumber_ShouldThrowFormatException(string fraction)
        {
            Assert.That(() => fraction.ToDoubleNumber(),
                Throws.TypeOf<FormatException>()
                    .With.Message.EqualTo("Not a valid fraction."));
        }

        [TestCase("2.333")]
        [TestCase("2")]
        [TestCase("123")]
        [TestCase("0.666")]
        public void IsDoubleNumber_WithValidInput_ShouldReturnTrue(string input)
        {
            var actualResult = input.IsPositiveDoubleNumber();

            Assert.IsTrue(actualResult);
        }

        [TestCase("false")]
        [TestCase("2.s.2")]
        [TestCase("2.2.2")]
        [TestCase("0d.666")]
        [TestCase("-3")]
        [TestCase("-13")]
        public void IsDoubleNumber_WithInValidInput_ShouldReturnFalse(string input)
        {
            var actualResult = input.IsPositiveDoubleNumber();

            Assert.IsFalse(actualResult);
        }

        [TestCase("2 1/3")]
        [TestCase("24")]
        [TestCase("2/3")]
        [TestCase("1/3")]
        public void IsFractionNumber_WithValidInput_ShouldReturnTrue(string input)
        {
            var actualResult = input.IsPositiveFractionNumber();

            Assert.IsTrue(actualResult);
        }

        [TestCase("1/3 s")]
        [TestCase("dsdas")]
        [TestCase("2.2")]
        [TestCase("0d.666")]
        [TestCase("-1 1/3")]
        [TestCase("-15 2/3")]
        [TestCase("15 -2/3")]
        public void IsFractionNumber_WithInValidInput_ShouldReturnFalse(string input)
        {
            var actualResult = input.IsPositiveFractionNumber();

            Assert.IsFalse(actualResult);
        }
    }
}
