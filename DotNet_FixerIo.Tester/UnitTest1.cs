using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotNet_FixerIo;
using DotNet_FixerIo.Manage;

namespace DotNet_FixerIo.Tester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            FixerIo fixer = new FixerIo("b3dfdaf429146cbaff9074aed1db7789");

            //Act
            RateInfo info = fixer.GetLatestRates();

            //Assert
            Assert.IsTrue(info.Success = true);
        }
    }
}
