using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Security;
using System.Text;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemovingTests : AuthTestBase
    {
        [Test]
        public void ProjectRemovingTest()
        {
            AccountData account = new AccountData()
            {
                Username = "administrator",
                Password = "root"
            };
            app.Projects.CheckProjectListIsNotEmptyAPI(account);

            List<ProjectData> oldProjects = app.Projects.GetProjectListAPI(account);

            app.Projects.Remove(0);

            List<ProjectData> newProjects = app.Projects.GetProjectListAPI(account);

            Assert.AreEqual(oldProjects.Count - 1, newProjects.Count);
        }

    }
}
