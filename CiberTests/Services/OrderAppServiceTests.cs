using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ciber.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciber.Services.Tests
{
    [TestClass()]
    public class OrderAppServiceTests
    {
        private IOrderAppService _orderService;
        public OrderAppServiceTests(IOrderAppService orderService)
        {
            _orderService = orderService;
        }
        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            var items = await _orderService.GetAllAsync();
            Assert.Equals(2, items.Count);
        }
    }
}