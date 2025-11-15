using BLL.Models;
using BLL.Services;
using MSTestProject.Helper;

namespace MSTestProject.Services
{
    [TestClass]
    public class ResumeServiceTests
    {
        private FakeResumeRepository _repo;
        private ResumeService _service;

        [TestInitialize]
        public void Init()
        {
            _repo = new FakeResumeRepository();
            _service = new ResumeService(_repo);
        }

        [TestMethod]
        public void AddResume_ShouldAddResumeToRepository()
        {
            // Arrange
            var resume = new ResumeModel
            {
                Id = Guid.NewGuid(),
                UnemployedId = Guid.NewGuid(),
                Title = "Junior C# Developer",
                Skills = new List<string> { "C#", ".NET" },
                ExperienceYears = 1,
                Education = "КПІ",
                CreatedDate = DateTime.Now
            };

            // Act
            _service.Add(resume);

            // Assert
            Assert.AreEqual(1, _repo.Data.Count);
            Assert.IsNotNull(_repo.Data.Find(r => r.Id == resume.Id));
        }
    }
}

