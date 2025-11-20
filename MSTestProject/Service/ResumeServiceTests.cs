using BLL.Exceptions;
using BLL.Models;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Moq;


namespace MSTestProject.Services
{
    [TestClass]
    public class ResumeServiceTests
    {
        private Mock<IResumeRepository> _mockRepo;
        private ResumeService _service;

        [TestInitialize]
        public void Init()
        {
            _mockRepo = new Mock<IResumeRepository>();
            _service = new ResumeService(_mockRepo.Object);
        }


        [TestMethod]
        public void Add_Should_Call_Repository_Add()
        {
            var model = new ResumeModel { Title = "C# Dev" };

            _service.Add(model);

            _mockRepo.Verify(x => x.Add(It.IsAny<ResumeEntity>()), Times.Once);
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
        public void Update_Should_Call_Repository_Update()
        {
            var model = new ResumeModel { Title = "Updated Title" };

            _service.Update(model);

            _mockRepo.Verify(x => x.Update(It.IsAny<ResumeEntity>()), Times.Once);
        }

        [TestMethod]
        public void GetAll_Should_Return_Mapped_Models()
        {
            var entities = new List<ResumeEntity>
            {
                new ResumeEntity { Id = Guid.NewGuid() },
                new ResumeEntity { Id = Guid.NewGuid() }
            };
            _mockRepo.Setup(x => x.GetAll()).Returns(entities);

            var result = _service.GetAll();

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetById_Should_Return_Model_When_Found()
        {
            var id = Guid.NewGuid();
            var entity = new ResumeEntity { Id = id, Title = "Test" };
            _mockRepo.Setup(x => x.GetById(id)).Returns(entity);

            var result = _service.GetById(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetById_Should_Throw_When_NotFound()
        {
            _mockRepo.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((ResumeEntity)null);

            _service.GetById(Guid.NewGuid());
        }

        [TestMethod]
        public void AddCategory_Should_Update_Category()
        {
            var id = Guid.NewGuid();
            var entity = new ResumeEntity { Id = id, Category = null };

            _mockRepo.Setup(x => x.GetById(id)).Returns(entity);

            _service.AddCategory(id, "IT");

            _mockRepo.Verify(x => x.Update(It.Is<ResumeEntity>(e => e.Category == "IT")), Times.Once);
        }

        [TestMethod]
        public void RemoveCategory_Should_Set_Category_To_Null()
        {
            var id = Guid.NewGuid();
            var entity = new ResumeEntity { Id = id, Category = "Design" };
            _mockRepo.Setup(x => x.GetById(id)).Returns(entity);

            _service.RemoveCategory(id);


            _mockRepo.Verify(x => x.Update(It.Is<ResumeEntity>(e => e.Category == null)), Times.Once);
        }

        [TestMethod]
        public void GetSortedByTitle_Should_Sort()
        {
            var entities = new List<ResumeEntity>
            {
                new ResumeEntity { Title = "Zzz" },
                new ResumeEntity { Title = "Aaa" }
            };
            _mockRepo.Setup(x => x.GetAll()).Returns(entities);

            var list = _service.GetSortedByTitle().ToList();

            Assert.AreEqual("Aaa", list[0].Title);
            Assert.AreEqual("Zzz", list[1].Title);
        }

        [TestMethod]
        public void GetSortedByCategory_Should_Sort()
        {
            var entities = new List<ResumeEntity>
            {
                new ResumeEntity { Category = "Work" },
                new ResumeEntity { Category = "Admin" }
            };
            _mockRepo.Setup(x => x.GetAll()).Returns(entities);

            var list = _service.GetSortedByCategory().ToList();

            Assert.AreEqual("Admin", list[0].Category);
            Assert.AreEqual("Work", list[1].Category);
        }
    }
}