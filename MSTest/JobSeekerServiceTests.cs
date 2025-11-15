//using BLL.DTOs;
//using BLL.Exceptions;
//using BLL.Models;
//using BLL.Services;
//using DAL.Interfaces;
//using Microsoft.VisualStudio.TestTools.UnitTesting; 
//using Moq;
//using System.Collections.Generic;
//using System.Linq;

//namespace MSTest
//{
//    [TestClass] // <-- MSTest
//    public class JobSeekerServiceTests
//    {
//        private Mock<IRepository<Unemployed>> _mockUnemployedRepo = null!;
//        private Mock<IRepository<Resume>> _mockResumeRepo = null!;
//        private JobSeekerService _service = null!;

//        [TestInitialize] // <-- MSTest
//        public void TestInitialize()
//        {
//            _mockUnemployedRepo = new Mock<IRepository<Unemployed>>();
//            _mockResumeRepo = new Mock<IRepository<Resume>>();
//            _service = new JobSeekerService(_mockUnemployedRepo.Object, _mockResumeRepo.Object);
//        }

//        [TestMethod] // <-- MSTest
//        public void AddUnemployed_WithValidData_ShouldCallAddAndSaveChanges()
//        {
//            // Arrange (Triple A)
//            var dto = new UnemployedDto("Іван", "Петренко", "0501234567");

//            // Act (Triple A)
//            _service.AddUnemployed(dto);

//            // Assert (Triple A)
//            // Перевіряємо, що викликався Add з коректно змапленою сутністю
//            _mockUnemployedRepo.Verify(r => r.Add(It.Is<Unemployed>(u =>
//                u.FirstName == dto.FirstName &&
//                u.LastName == dto.LastName)), Times.Once);

//            _mockUnemployedRepo.Verify(r => r.SaveChanges(), Times.Once);
//        }

//        [TestMethod] // <-- MSTest
//        public void AddUnemployed_WithEmptyFirstName_ShouldThrowValidationException()
//        {
//            // Arrange
//            var dto = new UnemployedDto("", "Петренко", "0501234567");

//            // Act & Assert
//            Assert.ThrowsException<ValidationException>(() => _service.AddUnemployed(dto));
//            _mockUnemployedRepo.Verify(r => r.SaveChanges(), Times.Never);
//        }

//        [TestMethod] // <-- MSTest
//        public void GetUnemployedById_WhenExists_ShouldReturnUnemployed()
//        {
//            // Arrange
//            var expectedUnemployed = new Unemployed { Id = 1, FirstName = "Тест" };
//            _mockUnemployedRepo.Setup(r => r.GetById(1)).Returns(expectedUnemployed);

//            // Act
//            var result = _service.GetUnemployedById(1);

//            // Assert (MSTest)
//            Assert.IsNotNull(result);
//            Assert.AreEqual(expectedUnemployed.Id, result.Id);
//        }

//        [TestMethod] // <-- MSTest
//        public void GetUnemployedById_WhenNotExists_ShouldThrowEntityNotFoundException()
//        {
//            // Arrange
//            _mockUnemployedRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns((Unemployed?)null);

//            // Act & Assert
//            Assert.ThrowsException<EntityNotFoundException>(() => _service.GetUnemployedById(99));
//        }

//        [TestMethod] // <-- MSTest
//        public void GetUnemployedSortedBy_LastName_ShouldReturnSortedList()
//        {
//            // Arrange
//            var list = new List<Unemployed>
//        {
//            new Unemployed { Id = 1, LastName = "Петренко" },
//            new Unemployed { Id = 2, LastName = "Іваненко" },
//            new Unemployed { Id = 3, LastName = "Авраменко" }
//        };
//            _mockUnemployedRepo.Setup(r => r.GetAll()).Returns(list);

//            // Act
//            var result = _service.GetUnemployedSortedBy("lastname").ToList();

//            // Assert
//            Assert.AreEqual(3, result.Count);
//            Assert.AreEqual("Авраменко", result[0].LastName);
//        }

//        [TestMethod] // <-- MSTest
//        public void SearchUnemployed_WithValidKeyword_ShouldReturnMatching()
//        {
//            // Arrange
//            var list = new List<Unemployed>
//        {
//            new Unemployed { Id = 1, FirstName = "Іван", LastName = "Петренко" },
//            new Unemployed { Id = 2, FirstName = "Петро", LastName = "Іваненко" },
//            new Unemployed { Id = 3, FirstName = "Іван", LastName = "Сидоренко" }
//        };
//            _mockUnemployedRepo.Setup(r => r.GetAll()).Returns(list);

//            // Act
//            var result = _service.SearchUnemployed("Іван").ToList();

//            // Assert
//            Assert.AreEqual(2, result.Count);
//            Assert.IsTrue(result.Any(u => u.LastName == "Петренко"));
//            Assert.IsTrue(result.Any(u => u.LastName == "Сидоренко"));
//        }

//        [TestMethod] // <-- MSTest
//        public void UpdateUnemployed_WithValidData_ShouldCallUpdateAndSaveChanges()
//        {
//            // Arrange
//            var dto = new UnemployedDto("Іван_новий", "Петренко_новий", "111");
//            var existing = new Unemployed { Id = 1, FirstName = "Іван", LastName = "Петренко" };
//            _mockUnemployedRepo.Setup(r => r.GetById(1)).Returns(existing);

//            // Act
//            _service.UpdateUnemployed(1, dto);

//            // Assert
//            // Перевіряємо, що Update викликався з сутністю, поля якої були оновлені
//            _mockUnemployedRepo.Verify(r => r.Update(It.Is<Unemployed>(u =>
//                u.Id == 1 &&
//                u.FirstName == dto.FirstName &&
//                u.LastName == dto.LastName)), Times.Once);

//            _mockUnemployedRepo.Verify(r => r.SaveChanges(), Times.Once);
//        }

//        [TestMethod] // <-- MSTest
//        public void UpdateUnemployed_WithInvalidId_ShouldThrowEntityNotFound()
//        {
//            // Arrange
//            var dto = new UnemployedDto("Іван_новий", "Петренко_новий", "111");
//            _mockUnemployedRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns((Unemployed?)null);

//            // Act & Assert
//            Assert.ThrowsException<EntityNotFoundException>(() => _service.UpdateUnemployed(99, dto));
//            _mockUnemployedRepo.Verify(r => r.SaveChanges(), Times.Never);
//        }
//    }
//}