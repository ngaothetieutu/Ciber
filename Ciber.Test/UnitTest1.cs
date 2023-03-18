using Ciber.Services;

namespace Ciber.Test
{
    [TestClass]
    public class UnitTest1
    {
        private IOrderAppService _appService;
        public UnitTest1(IOrderAppService appService)
        {
            _appService = appService;
        }
        [TestMethod]
        public async Task GetAllOrderTestAsync()
        {
            var items = await _appService.GetAllAsync();
            Assert.Equals(2, items.Count);
        }
    }
}