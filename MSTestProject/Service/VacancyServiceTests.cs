using BLL.Exceptions;
using BLL.Models;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Moq;

namespace MSTestProject.Services
{
    [TestClass]
    public class VacancyServiceTests
    {
        private Mock<IVacancyRepository> _mockRepo;
        private VacancyService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IVacancyRepository>();
            _service = new VacancyService(_mockRepo.Object);
        }

        [TestMethod]
        public void AddVacancy_Should_Call_Repository_Add()
        {
            var vacancy = new VacancyModel
            {
                Id = Guid.NewGuid(),
                Title = "Програміст",
                Description = "C# Developer"
            };

            _service.Add(vacancy);

            _mockRepo.Verify(x => x.Add(It.IsAny<VacancyEntity>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void AddVacancy_Should_Throw_When_Null()
        {
            _service.Add(null);
        }

        [TestMethod]
        public void GetById_Should_Return_Correct_Vacancy()
        {
            var id = Guid.NewGuid();
            var entity = new VacancyEntity { Id = id, Title = "Test" };

            _mockRepo.Setup(x => x.GetById(id)).Returns(entity);

            var result = _service.GetById(id);

            Assert.IsNotNull(result);
            Assert.AreEqual("Test", result.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetById_Should_Throw_When_NotFound()
        {
            _mockRepo.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((VacancyEntity)null);

            _service.GetById(Guid.NewGuid());
        }

        [TestMethod]
        public void Update_Should_Call_Repository_Update()
        {
            var updated = new VacancyModel { Id = Guid.NewGuid(), Title = "New" };

            _service.Update(updated);

            _mockRepo.Verify(x => x.Update(It.IsAny<VacancyEntity>()), Times.Once);
        }

        [TestMethod]
        public void Delete_Should_Call_Repository_Delete()
        {
            var id = Guid.NewGuid();

            _service.Delete(id);

            _mockRepo.Verify(x => x.Delete(id), Times.Once);
        }

        [TestMethod]
        public void AddCategory_Should_Update_Category()
        {
            var id = Guid.NewGuid();
            var entity = new VacancyEntity { Id = id, Category = null };
            _mockRepo.Setup(x => x.GetById(id)).Returns(entity);

            _service.AddCategory(id, "IT");

            _mockRepo.Verify(x => x.Update(It.Is<VacancyEntity>(v => v.Category == "IT")), Times.Once);
        }

        [TestMethod]
        public void RemoveCategory_Should_Set_Category_To_Null()
        {
            var id = Guid.NewGuid();
            var entity = new VacancyEntity { Id = id, Category = "Sales" };
            _mockRepo.Setup(x => x.GetById(id)).Returns(entity);

            _service.RemoveCategory(id);

            _mockRepo.Verify(x => x.Update(It.Is<VacancyEntity>(v => v.Category == null)), Times.Once);
        }

        [TestMethod]
        public void GetSortedByCategory_Should_Sort()
        {
            var entities = new List<VacancyEntity>
            {
                new VacancyEntity { Category = "Z" },
                new VacancyEntity { Category = "A" }
            };
            _mockRepo.Setup(x => x.GetAll()).Returns(entities);

            var sorted = _service.GetSortedByCategory().ToList();

            Assert.AreEqual("A", sorted[0].Category);
            Assert.AreEqual("Z", sorted[1].Category);
        }

        [TestMethod]
        public void GetSortedByTitle_Should_Sort()
        {
            var entities = new List<VacancyEntity>
            {
                new VacancyEntity { Title = "Java" },
                new VacancyEntity { Title = "C#" }
            };
            _mockRepo.Setup(x => x.GetAll()).Returns(entities);

            var sorted = _service.GetSortedByTitle().ToList();

            Assert.AreEqual("C#", sorted[0].Title);
            Assert.AreEqual("Java", sorted[1].Title);
        }

        [TestMethod]
        public void GetByCategory_Should_Call_Repository_GetByCategory()
        {
            var category = "IT";
            var entities = new List<VacancyEntity>
            {
                new VacancyEntity { Title = "Dev", Category = "IT" }
            };

            _mockRepo.Setup(x => x.GetByCategory(category)).Returns(entities);

            var result = _service.GetByCategory(category).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Dev", result[0].Title);
            _mockRepo.Verify(x => x.GetByCategory(category), Times.Once);
        }
    }
}