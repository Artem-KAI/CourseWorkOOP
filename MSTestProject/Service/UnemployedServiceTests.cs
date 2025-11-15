using BLL.Models;
using BLL.Services;
using MSTestProject.Helper;

namespace MSTestProject.Services
{
    [TestClass]
    public class UnemployedServiceTests
    {
        private FakeUnemployedRepository _repo;
        private UnemployedService _service;

        [TestInitialize]
        public void Init()
        {
            _repo = new FakeUnemployedRepository();
            _service = new UnemployedService(_repo);
        }

        [TestMethod]
        public void AddUnemployed_Should_Add()
        {
            var model = new UnemployedModel
            {
                Id = Guid.NewGuid(),
                FirstName = "Петро"
            };

            _service.Add(model);

            Assert.AreEqual(1, _repo.Data.Count);
        }
    }
}
