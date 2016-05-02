using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCN.BL.Services;

namespace PCN.BL.Tests
{
    [TestClass]
    public class MeasureServiceTest
    {
        [TestMethod]
        public void GetRamTest()
        {
            var service = new MeasureService();
            var ram = service.GetComputerInfo();
        }
    }
}
