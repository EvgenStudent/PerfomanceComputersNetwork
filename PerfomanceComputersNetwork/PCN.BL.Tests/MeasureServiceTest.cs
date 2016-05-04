using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCN.BL.Services;
using PCN.Server.Extensions;

namespace PCN.BL.Tests
{
    [TestClass]
    public class MeasureServiceTest
    {
        [TestMethod]
        public async Task GetRamTest()
        {
            var service = new SendService(new Uri("http://localhost:21100/api"));
            await service.SendComputerInfo(new MeasureService().GetComputerInfo());
        }
    }
}
