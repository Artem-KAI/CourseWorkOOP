using BLL.Exceptions;
using BLL.Models;
using BLL.Services;
using DAL.Entities;
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
        public void Add_Should_Add_Employer()
        {
            var model = new EmployerModel
            {
                Id = Guid.NewGuid(),
                FirstName = "Олег",
                LastName = "Ткачук"
            };

            _service.Add(model);

            Assert.AreEqual(1, _repo.Data.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessException))]
        public void Add_Should_Throw_When_Null()
        {
            _service.Add(null);
        }

        [TestMethod]
        public void Delete_Should_Remove()
        {
            var id = Guid.NewGuid();
            _repo.Data.Add(new EmployerEntity { Id = id });

            _service.Delete(id);

            Assert.AreEqual(0, _repo.Data.Count);
        }

        [TestMethod]
        public void GetAll_Should_Return_All()
        {
            _repo.Data.Add(new EmployerEntity());
            _repo.Data.Add(new EmployerEntity());

            var list = _service.GetAll();

            Assert.AreEqual(2, list.Count());
        }

        [TestMethod]
        public void GetSortedByFirstName_Should_Sort()
        {
            _repo.Data.Add(new EmployerEntity { ContactPerson = "Петро" });
            _repo.Data.Add(new EmployerEntity { ContactPerson = "Андрій" });

            var sorted = _service.GetSortedByFirstName().ToList();

            Assert.AreEqual("Андрій", sorted[0].FirstName);
        }
    }
}
    //[TestClass]
    //public class EmployerServiceTests
    //{
    //    private FakeEmployerRepository _repo;
    //    private EmployerService _service;

    //    [TestInitialize]
    //    public void Init()
    //    {
    //        _repo = new FakeEmployerRepository();
    //        _service = new EmployerService(_repo);
    //    }

    //    [TestMethod]
    //    public void AddEmployer_Should_Add()
    //    {
    //        var model = new EmployerModel
    //        {
    //            Id = Guid.NewGuid(),
    //            FirstName = "Олег",
    //            LastName = "Ткачук",
    //            CompanyName = "IT Company"
    //        };

    //        _service.Add(model);

    //        Assert.AreEqual(1, _repo.Data.Count);
    //    }

