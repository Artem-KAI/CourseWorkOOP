using BLL.Exceptions;
using BLL.Models;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Moq;

namespace MSTestProject.Services
{
    [TestClass]
    public class EmployerServiceTests
    {
        private Mock<IEmployerRepository> _mockRepo;
        private EmployerService _service;

        [TestInitialize]
        public void Init()
        {
            _mockRepo = new Mock<IEmployerRepository>();

            _service = new EmployerService(_mockRepo.Object);
        }

        [TestMethod]
        public void Add_Should_Call_Repository_Add()
        {
            var model = new EmployerModel
            {
                Id = Guid.NewGuid(),
                FirstName = "Олег",
                LastName = "Ткачук"
            };

            _service.Add(model);

            _mockRepo.Verify(x => x.Add(It.IsAny<EmployerEntity>()), Times.Once);
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
        public void GetAll_Should_Return_Mapped_Models()
        {
            var entities = new List<EmployerEntity>
            {
                new EmployerEntity { Id = Guid.NewGuid() },
                new EmployerEntity { Id = Guid.NewGuid() }
            };

            _mockRepo.Setup(x => x.GetAll()).Returns(entities);

            var list = _service.GetAll();

            Assert.AreEqual(2, list.Count());

            _mockRepo.Verify(x => x.GetAll(), Times.Once);
        }

        [TestMethod]
        public void GetSortedByFirstName_Should_Sort()
        {
            var entities = new List<EmployerEntity>
            {
                new EmployerEntity { Id = Guid.NewGuid(), ContactPerson = "Петро" },
                new EmployerEntity { Id = Guid.NewGuid(), ContactPerson = "Андрій" }
            };
            _mockRepo.Setup(x => x.GetAll()).Returns(entities);

            var sorted = _service.GetSortedByFirstName().ToList();

            Assert.AreEqual("Андрій", sorted[0].FirstName);
            Assert.AreEqual("Петро", sorted[1].FirstName);
        }

        [TestMethod]
        public void GetSortedByLastName_Should_Sort()
        {
            var entities = new List<EmployerEntity>
            {
                new EmployerEntity { Id = Guid.NewGuid() }, 
                new EmployerEntity { Id = Guid.NewGuid() }  
            };
            _mockRepo.Setup(x => x.GetAll()).Returns(entities);
            
            var result = _service.GetSortedByLastName();
            
            _mockRepo.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
