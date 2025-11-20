using BLL.Exceptions;
using BLL.Models;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Moq;

namespace MSTestProject.Services
{
    [TestClass]
    public class UnemployedServiceTests
    {
        private Mock<IUnemployedRepository> _mockRepo;
        private UnemployedService _service;

        [TestInitialize]
        public void Init()
        {
            _mockRepo = new Mock<IUnemployedRepository>();
            _service = new UnemployedService(_mockRepo.Object);
        }

        [TestMethod]
        public void Add_Should_Call_Repository_Add()
        {
            var model = new UnemployedModel
            {
                Id = Guid.NewGuid(),
                FirstName = "Петро"
            };

            _service.Add(model);

            _mockRepo.Verify(x => x.Add(It.IsAny<UnemployedEntity>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void Add_Should_Throw_When_Null()
        {
            _service.Add(null);
        }

        [TestMethod]
        public void Delete_Should_Call_Repository_Delete()
        {
            var id = Guid.NewGuid();

            _service.Delete(id);

            _mockRepo.Verify(x => x.Delete(id), Times.Once);
        }

        [TestMethod]
        public void GetAll_Should_Return_All_Mapped()
        {
            var entities = new List<UnemployedEntity>
            {
                new UnemployedEntity { Id = Guid.NewGuid() },
                new UnemployedEntity { Id = Guid.NewGuid() }
            };
            _mockRepo.Setup(x => x.GetAll()).Returns(entities);

            var list = _service.GetAll();

            Assert.AreEqual(2, list.Count());
            _mockRepo.Verify(x => x.GetAll(), Times.Once);
        }

        [TestMethod]
        public void GetById_Should_Return_Correct_Model()
        {
            var id = Guid.NewGuid();
            var entity = new UnemployedEntity { Id = id, FirstName = "Іван" };

            _mockRepo.Setup(x => x.GetById(id)).Returns(entity);

            var result = _service.GetById(id);

            Assert.IsNotNull(result);
            Assert.AreEqual("Іван", result.FirstName);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetById_Should_Throw_When_NotFound()
        {
            _mockRepo.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((UnemployedEntity)null);

            _service.GetById(Guid.NewGuid());
        }

        [TestMethod]
        public void Update_Should_Call_Repository_Update()
        {
            var updated = new UnemployedModel { Id = Guid.NewGuid(), FirstName = "New" };

            _service.Update(updated);

            _mockRepo.Verify(x => x.Update(It.IsAny<UnemployedEntity>()), Times.Once);
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
            var entities = new List<UnemployedEntity>
            {
                new UnemployedEntity { Id = Guid.NewGuid(), FirstName = "Антон" },
                new UnemployedEntity { Id = Guid.NewGuid(), FirstName = "Артем" }
            };
            _mockRepo.Setup(x => x.GetAll()).Returns(entities);

            var result = _service.Search("Артем");

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Артем", result.First().FirstName);
        }

        [TestMethod]
        public void Search_Should_Return_Empty_On_EmptyKeyword()
        {
            var result = _service.Search("");

            Assert.AreEqual(0, result.Count());

            _mockRepo.Verify(x => x.GetAll(), Times.Never);
        }

        [TestMethod]
        public void SortByFirstName_Should_Work()
        {
            var entities = new List<UnemployedEntity>
            {
                new UnemployedEntity { FirstName = "Петро" },
                new UnemployedEntity { FirstName = "Антон" }
            };
            _mockRepo.Setup(x => x.GetAll()).Returns(entities);

            var result = _service.GetSortedByFirstName().ToList();

            Assert.AreEqual("Антон", result[0].FirstName);
            Assert.AreEqual("Петро", result[1].FirstName);
        }

        [TestMethod]
        public void SortByLastName_Should_Work()
        {
            var entities = new List<UnemployedEntity>
            {
                new UnemployedEntity { LastName = "Яровий" },
                new UnemployedEntity { LastName = "Андрух" }
            };
            _mockRepo.Setup(x => x.GetAll()).Returns(entities);

            var result = _service.GetSortedByLastName().ToList();

            Assert.AreEqual("Андрух", result[0].LastName);
            Assert.AreEqual("Яровий", result[1].LastName);
        }
    }
}
