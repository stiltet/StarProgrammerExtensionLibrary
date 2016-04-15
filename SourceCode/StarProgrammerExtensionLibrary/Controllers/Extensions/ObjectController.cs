using System;
using System.Collections.Generic;
using System.Web.Mvc;
using StarProgrammerExtensionLibrary.Extensions;
using StarProgrammerExtensionLibrary.Models;

namespace StarProgrammerExtensionLibrary.Controllers.Extensions
{
    public class ObjectController : ExtensionController
    {
        public ActionResult AreTwoObjectsEqual()
        {
            var users = new List<User>
            {
                new User
                {
                    Id = new Guid("57F4BC65-D219-4491-88EE-3367436147AA"),
                    Email = "test@test.com",
                    Username = "Test"
                },
                new User
                {
                    Id = new Guid("57F4BC65-D219-4491-88EE-3367436147BB"),
                    Email = "test@test.com",
                    Username = "Test2"
                },
                new User
                {
                    Id = new Guid("99F69AE0-2181-40EF-82A9-A8F5CC8E7144"),
                    Email = "test@test.com",
                    Username = "Test"
                },
                new User
                {
                    Id = new Guid("99F69AE0-2181-40EF-82A9-A8F5CC8E7144"),
                    Email = null,
                    Username = "Test"
                }
            };

            var ignoreList = new List<string> {"Username"};
            var ignoreNullValuesList = new List<string> {"Email"};

            var areTwoObjectsEqual = ObjectExtensions.AreTwoObjectsEqual(users[0], users[2]);
            var areTwoDifferentObjectsEqual = ObjectExtensions.AreTwoObjectsEqual(users[0], users[1]);
            var areTwoObjectsEqualWhenUsingIgnoreList = ObjectExtensions.AreTwoObjectsEqual(users[0], users[1],
                ignoreList);
            var areTwoObjectsEqualWhenNotUsingIgnoreNullValuesList = ObjectExtensions.AreTwoObjectsEqual(users[0],
                users[3]);
            var areTwoObjectsEqualWhenUsingIgnoreNullValuesList = ObjectExtensions.AreTwoObjectsEqual(users[0], users[3],
                null, ignoreNullValuesList);

            return View(new Tuple<List<User>, bool, bool>(users, areTwoObjectsEqual, areTwoDifferentObjectsEqual));
        }

        public ActionResult ConvertToHistoryObject()
        {
            var user = new User
            {
                Id = new Guid("57F4BC65-D219-4491-88EE-3367436147AA"),
                Email = "test@test.com",
                Username = "Test"
            };

            var userHistory = (UserHistory) ObjectExtensions.ConvertToHistoryObject(user, user.ToString());

            //Update/Delete in database

            return View(new Tuple<User, UserHistory>(user, userHistory));
        }
    }
}