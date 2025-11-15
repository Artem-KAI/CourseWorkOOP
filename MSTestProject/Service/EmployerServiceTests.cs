using BLL.Models;
using BLL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestProject.Helper;
using System;

namespace MSTestProject.Services
{
    [TestClass]
    public class EmployerServiceTests
    {
        private FakeEmployerRepository _repo;
        private EmployerService _service;

        [TestInitialize]
        public void Init()
        {
            _repo = new FakeEmployerRepository();
            _service = new EmployerService(_repo);
        }

        [TestMethod]
        public void AddEmployer_Should_Add()
        {
            var model = new EmployerModel
            {
                Id = Guid.NewGuid(),
                FirstName = "Олег",
                LastName = "Ткачук",
                CompanyName = "IT Company"
            };

            _service.Add(model);

            Assert.AreEqual(1, _repo.Data.Count);
        }
    }
}
