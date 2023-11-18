using CyberBook_API.Controllers;
using CyberBook_API.Dal;
using CyberBook_API.Dal.Repositories;
using CyberBook_API.Interfaces;
using CyberBook_API.Models;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CyberBook_Test
{
    [TestClass]
    public class GenericTest
    {
        private readonly ICybersRepository _cybersRepository = new CybersRepository();
        [TestMethod]
        public async Task TestCreateCyber()
        {
            //int count = 0;
            //var fakeRecipes = A.CollectionOfDummy<Cyber>(count);
            //var dataStore = A.Fake<ICybersRepository>();
            //A.CallTo(() => dataStore.GetAll());
            //var controller = new CheckAndTestController();

            ////Act
            //var actionResult = controller.ListCyber();

            ////Assert
            //var result = actionResult.Result;

            // Arrange
            var generic = new GenericRepository<Cyber>();
            var cyber = new Cyber();
            cyber.Id = 6;
            cyber.CyberName = "Cyber C UPDATED";
            cyber.Address = "Thường Tín, Hà Nội, Việt Nam";
            cyber.PhoneNumber = "0988888888";
            cyber.CyberDescription = "Description cyber C UPDATED Checker";
            cyber.RatingPoint = 4;
            cyber.BossCyberName = "Nguyễn Đình Huynh";
            cyber.lat = "12.21111";
            cyber.lng = "11.22222";

            // Act
            var result = _cybersRepository.Create(cyber);

            //Assert
            Assert.AreEqual(result.Result, cyber);
        }

        //public void CyberGetAll_ShouldReturnList()
        //{
        //    // Arrange
        //    var generic = new GenericRepository<Cyber>();

        //    // Act
        //    var result = _cybersRepository.GetAll();

        //    // Assert
        //    //Assert.IsTrue(result.);
        //}
    }
}
