using BLL.Models;
using BLL.Services;
using MSTestProject.Helper;

namespace MSTestProject.Services
{
    [TestClass]
    public class VacancyServiceTests
    {
        private FakeVacancyRepository _repo;
        private VacancyService _service;

        [TestInitialize]
        public void Setup()
        {
            _repo = new FakeVacancyRepository();
            _service = new VacancyService(_repo);
        }

        [TestMethod]
        public void AddVacancy_Should_Add_New_Vacancy()
        {
            // Arrange
            var vacancy = new VacancyModel
            {
                Id = Guid.NewGuid(),
                Title = "Програміст",
                Description = "C# Developer"
            };

            // Act
            _service.Add(vacancy);

            // Assert
            Assert.AreEqual(1, _repo.Data.Count);
            Assert.AreEqual("Програміст", _repo.Data[0].Title);
        }

        [TestMethod]
        public void GetById_Should_Return_Correct_Vacancy()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repo.Data.Add(new DAL.Entities.VacancyEntity { Id = id, Title = "Test" });

            // Act
            var result = _service.GetById(id);

            // Assert
            Assert.AreEqual("Test", result.Title);
        }

        [TestMethod]
        public void Update_Should_Modify_Existing()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repo.Data.Add(new DAL.Entities.VacancyEntity { Id = id, Title = "Old" });

            var updated = new VacancyModel { Id = id, Title = "New" };

            // Act
            _service.Update(updated);

            // Assert
            Assert.AreEqual("New", _repo.Data[0].Title);
        }

        [TestMethod]
        public void Delete_Should_Remove_Record()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repo.Data.Add(new DAL.Entities.VacancyEntity { Id = id });

            // Act
            _service.Delete(id);

            // Assert
            Assert.AreEqual(0, _repo.Data.Count);
        }
    }
}
