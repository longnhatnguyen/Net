using CyberBook_API.Dal.Repositories;
using CyberBook_API.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CyberBook_Test
{
    public class UsersRepository_Test
    {
        [TestClass]
        public class GenericTest
        {
            private readonly ICybersRepository _cybersRepository = new CybersRepository();

            [Fact]
            public void CheckComfirmPass_IfAllValueValid()
            {
                // Arrange
                int userId = 1;
                string confirmPass = "123456";

                // Act
                var user = new UsersRepository();
                var result = user.CheckComfirmPass(userId, confirmPass);

                // Assert 
                Assert.AreEqual(result.Result, true);
            }

            [Fact]
            public void CheckComfirmPass_IfCodeInValid()
            {
                // Arrange
                int userId = 1;
                string confirmPass = "654321";

                // Act
                var user = new UsersRepository();
                var result = user.CheckComfirmPass(userId, confirmPass);

                // Assert 
                Assert.AreEqual(result.Result, false);
            }

        }
    }
}
