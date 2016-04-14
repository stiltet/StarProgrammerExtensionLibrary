using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarProgrammerExtensionLibrary.Extensions;
using StarProgrammerExtensionLibrary.Models;

namespace StarProgrammerExtensionLibrary.Tests.ExtensionTests
{
    [TestClass]
    public class ObjectExtensionsTests
    {
        #region ObjectExtensions.AreTwoObjectsEqual()

        [TestMethod]
        public void AreTwoObjectsEqual()
        {
            var user = GetListOfUserObjects().First();

            var result = ObjectExtensions.AreTwoObjectsEqual(user, user);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AreTwoObjectsEqualWithIdAsOnlyDiffrence()
        {
            var users = GetListOfUserObjects().Where(x => x.Username == "test" && x.Email == "test@test.se").ToList();

            if (users.Count < 2)
                throw new InvalidDataException();

            var result = ObjectExtensions.AreTwoObjectsEqual(users.First(), users.Last());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AreTwoObjectsNotEqual()
        {
            var user = GetListOfUserObjects().First();
            var user2 = GetListOfUserObjects().First(x => !x.Username.Equals(user.Username));

            var result = ObjectExtensions.AreTwoObjectsEqual(user, user2);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AreTwoObjectsEqualWhenIgnoringEmail()
        {
            var user = GetListOfUserObjects().First();
            var user2 = GetListOfUserObjects().First(x => !x.Email.Equals(user.Email));

            var ignoreList = new List<string>
            {
                "Email"
            };

            var result = ObjectExtensions.AreTwoObjectsEqual(user, user2, ignoreList);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AreTwoObjectsEqualWhenNewObjectHasANullValue()
        {
            var user = GetListOfUserObjects().First(x => x.Username == null);
            var user2 = GetListOfUserObjects().First();

            var result = ObjectExtensions.AreTwoObjectsEqual(user, user2);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AreTwoObjectsEqualWhenNewObjectHasANullValueButPropertyNameExistsInListToIgnoreNullValues()
        {
            var user = GetListOfUserObjects().First(x => x.Username == null);
            var user2 = GetListOfUserObjects().First();
            var ignoreNullValuesOfTheseProperties = new List<string> {"Username"};

            var result = ObjectExtensions.AreTwoObjectsEqual(user, user2, new List<string>(),
                ignoreNullValuesOfTheseProperties);

            Assert.IsTrue(result);
        }

        #endregion

        private static IEnumerable<User> GetListOfUserObjects()
        {
            return new List<User>
            {
                new User
                {
                    Id = new Guid("5166F488-9D01-4DD6-B5F7-74F6227ECEAF"),
                    Username = "test",
                    Email = "test@test.se"
                },
                new User
                {
                    Id = new Guid("12D67613-8036-4E98-8576-5A6D77355A3A"),
                    Username = "test",
                    Email = "test@test.se"
                },
                new User
                {
                    Id = new Guid("5166F488-9D01-4DD6-B5F7-74F6227ECEAF"),
                    Username = "test2",
                    Email = "test@test.se"
                },
                new User
                {
                    Id = new Guid("7483EE44-0857-41F3-BE5E-8890600510F9"),
                    Username = "test",
                    Email = "test2@test.se"
                },
                new User
                {
                    Id = new Guid("99F69AE0-2181-40EF-82A9-A8F5CC8E7144"),
                    Username = null,
                    Email = "test@test.se"
                }
            };
        }
    }
}