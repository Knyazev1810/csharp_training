using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace addressbook_tests_autoit
{
    [TestFixture]
    public class GroupRemovedTests : TestBase
    {
        [Test]
        public void GroupRemovedTest()
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();


            app.Groups.Remove("#0|#0");

            List<GroupData> newGroups = app.Groups.GetGroupList();

            Assert.AreEqual(oldGroups.Count - 1, newGroups.Count);
        }
    }
}
