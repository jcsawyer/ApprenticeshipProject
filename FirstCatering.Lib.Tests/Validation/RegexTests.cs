using System;
using System.Text.RegularExpressions;
using FirstCatering.Lib.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FirstCatering.Lib.Tests.Validation
{
    [TestClass]
    public class RegexTests
    {
        public RegexTests() { }

        [TestMethod]
        public void EmptyEmployeeIdIsInvalid()
            => Assert.IsFalse(Regex.IsMatch("", Regexes.EmployeeId));

        [TestMethod]
        public void ShortEmployeeIdIsInvalid()
            => Assert.IsFalse(Regex.IsMatch("jsj", Regexes.EmployeeId));

        [TestMethod]
        public void LongEmployeeIdIsInvalid()
            => Assert.IsFalse(Regex.IsMatch("hjksdfhkjsdfhklsfkso87", Regexes.EmployeeId));

        [TestMethod]
        public void NonAlphaNumericEmployeeIdIsInvalid()
            => Assert.IsFalse(Regex.IsMatch("jd72k38d63jf$7s0", Regexes.EmployeeId));

        [TestMethod]
        public void ValidEmployeeIdIsValid()
            => Assert.IsTrue(Regex.IsMatch("lap293nd83yx20kr", Regexes.EmployeeId));

        [TestMethod]
        public void EmptyEmailIsInvalid()
            => Assert.IsFalse(Regex.IsMatch("", Regexes.Email));

        [TestMethod]
        public void InvalidEmailIsInvalid()
            => Assert.IsFalse(Regex.IsMatch("huisfioe", Regexes.Email));

        [TestMethod]
        public void ValidEmailIsValid()
            => Assert.IsTrue(Regex.IsMatch("test@nomail.com", Regexes.Email));

        [TestMethod]
        public void EmptyMobileIsInvalid()
            => Assert.IsFalse(Regex.IsMatch("", Regexes.Mobile));

        [TestMethod]
        public void AlphaNumericMobileIsInvalid()
            => Assert.IsFalse(Regex.IsMatch("O71318E0O1J", Regexes.Mobile));

        [TestMethod]
        public void ValidMobileIsValid()
            => Assert.IsTrue(Regex.IsMatch("07769401837", Regexes.Mobile));

        [TestMethod]
        public void CountryCodeValidIsValid()
            => Assert.IsTrue(Regex.IsMatch("+447769401837", Regexes.Mobile));

        [TestMethod]
        public void EmptyPinIsInvalid()
            => Assert.IsFalse(Regex.IsMatch("", Regexes.PIN));

        [TestMethod]
        public void ShortPinIsInvalid()
            => Assert.IsFalse(Regex.IsMatch("000", Regexes.PIN));

        [TestMethod]
        public void LongPinIsInvalid()
            => Assert.IsFalse(Regex.IsMatch("00000", Regexes.PIN));

        [TestMethod]
        public void ValidPinIsValid()
            => Assert.IsTrue(Regex.IsMatch("0000", Regexes.PIN));
    }
}
