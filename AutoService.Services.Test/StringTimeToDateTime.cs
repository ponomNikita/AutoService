using System;
using AutoService.Services.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace AutoService.Services.Test
{
    [TestFixture]
    public class StringTimeToDateTime
    {
        [Test]
        [TestCase("12:00", TestName = "StringTimeToDateTime - 12:00", ExpectedResult = "01.01.0001 12:00:00")]
        [TestCase("08:49", TestName = "StringTimeToDateTime - 08:49", ExpectedResult = "01.01.0001 8:49:00")]
        [TestCase("08:61", TestName = "StringTimeToDateTime - 08:61", ExpectedResult = null)]
        [TestCase("as:fg", TestName = "StringTimeToDateTime - as:fg", ExpectedResult = null)]
        [TestCase("asfa", TestName = "StringTimeToDateTime - asfa", ExpectedResult = null)]
        [TestCase("", TestName = "StringTimeToDateTime - EmptyString", ExpectedResult = null)]
        public string ConvertStringToDateTime(string time)
        {
            DateTime? Time = ApplicationEdit.StringTimeToDateTime(time);

            if (Time != null)
            {
                return Time.Value.ToString("G");
            }
            return null;
        }
    }
}
