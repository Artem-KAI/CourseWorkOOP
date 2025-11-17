using BLL.Models;
using BLL.Services;
using DAL.Entities;
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
        public void Add_Should_Add()
        {
            var resume = new ResumeModel
            {
                Id = Guid.NewGuid(),
                Title = "C# Dev"
            };

            _service.Add(resume);

            Assert.AreEqual(1, _repo.Data.Count);
        }

        [TestMethod]
        public void AddCategory_Should_Set_Category()
        {
            var id = Guid.NewGuid();
            _repo.Data.Add(new ResumeEntity { Id = id });

            _service.AddCategory(id, "IT");

            Assert.AreEqual("IT", _repo.Data[0].Category);
        }

        [TestMethod]
        public void GetSortedByTitle_Should_Sort()
        {
            _repo.Data.Add(new ResumeEntity { Title = "Zzz" });
            _repo.Data.Add(new ResumeEntity { Title = "Aaa" });

            var list = _service.GetSortedByTitle().ToList();

            Assert.AreEqual("Aaa", list[0].Title);
        }
    }

}

