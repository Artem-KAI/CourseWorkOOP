using BLL.Exceptions;
using BLL.Models;
using BLL.Services;
using DAL.Entities;
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
        public void Add_Should_Add_Unemployed()
        {
            // Arrange
            var model = new UnemployedModel
            {
                Id = Guid.NewGuid(),
                FirstName = "Петро"
            };

            // Act
            _service.Add(model);

            // Assert
            Assert.AreEqual(1, _repo.Data.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void Add_Should_Throw_When_Null()
        {
            // Act
            _service.Add(null);
        }

        [TestMethod]
        public void Delete_Should_Remove()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repo.Data.Add(new UnemployedEntity { Id = id });

            // Act
            _service.Delete(id);

            // Assert
            Assert.AreEqual(0, _repo.Data.Count);
        }

        [TestMethod]
        public void GetAll_Should_Return_All()
        {
            // Arrange
            _repo.Data.Add(new UnemployedEntity { Id = Guid.NewGuid() });
            _repo.Data.Add(new UnemployedEntity { Id = Guid.NewGuid() });

            // Act
            var list = _service.GetAll();

            // Assert
            Assert.AreEqual(2, list.Count());
        }

        [TestMethod]
        public void GetById_Should_Return_Correct()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repo.Data.Add(new UnemployedEntity { Id = id, FirstName = "Іван" });

            // Act
            var result = _service.GetById(id);

            // Assert
            Assert.AreEqual("Іван", result.FirstName);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetById_Should_Throw_When_NotFound()
        {
            // Act
            _service.GetById(Guid.NewGuid());
        }

        [TestMethod]
        public void Update_Should_Modify()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repo.Data.Add(new UnemployedEntity { Id = id, FirstName = "Old" });

            var updated = new UnemployedModel { Id = id, FirstName = "New" };

            // Act
            _service.Update(updated);

            // Assert
            Assert.AreEqual("New", _repo.Data.First().FirstName);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void Update_Should_Throw_On_Null()
        {
            _service.Update(null);
        }

        [TestMethod]
        public void Search_Should_Find_By_FirstName()
        {
            _repo.Data.Add(new UnemployedEntity { Id = Guid.NewGuid(), FirstName = "Антон" });
            _repo.Data.Add(new UnemployedEntity { Id = Guid.NewGuid(), FirstName = "Артем" });

            var result = _service.Search("Артем");

            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void Search_Should_Return_Empty_On_EmptyKeyword()
        {
            var result = _service.Search("");

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void SortByFirstName_Should_Work()
        {
            _repo.Data.Add(new UnemployedEntity { FirstName = "Петро" });
            _repo.Data.Add(new UnemployedEntity { FirstName = "Антон" });

            var result = _service.GetSortedByFirstName().ToList();

            Assert.AreEqual("Антон", result[0].FirstName);
        }

        [TestMethod]
        public void SortByLastName_Should_Work()
        {
            _repo.Data.Add(new UnemployedEntity { LastName = "Яровий" });
            _repo.Data.Add(new UnemployedEntity { LastName = "Андрух" });

            var result = _service.GetSortedByLastName().ToList();

            Assert.AreEqual("Андрух", result[0].LastName);
        }
    }

}
