using NUnit.Framework;
using Ciber.Services;
using NSubstitute;

namespace UnitTest
{
    [TestFixture]
    public class Tests
    {
        private  IOrderAppService _orderAppService;
        
        public Tests()
        {
            
            _orderAppService = Substitute.For<IOrderAppService>();
        }
        [SetUp]
        public void Setup()
        {
            _orderAppService = Substitute.For<IOrderAppService>();
        }

        [Test]
        public async Task GetAllOrdersTest()
        {
            var items = await _orderAppService.GetAllAsync();
            Assert.IsTrue(items.Count == 2, "Test Success");
        }
    }
}