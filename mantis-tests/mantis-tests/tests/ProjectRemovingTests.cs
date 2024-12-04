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
                Password = "password"
            };

            app.Projects.CheckProjectListIsNotEmpty();

            List<ProjectData> oldProjects = app.Projects.GetProjectListAPI(account);
            //List<ProjectData> oldProjects = app.API.GetProjectList(account);
            //List<ProjectData> oldProjects = app.Projects.GetOldProjectList();

            app.Projects.Remove(0);

            List<ProjectData> newProjects = app.Projects.GetProjectListAPI(account);
            //List<ProjectData> newProjects = app.API.GetProjectList(account);
            //List<ProjectData> newProjects = app.Projects.GetNewProjectList();

            Assert.AreEqual(oldProjects.Count - 1, newProjects.Count);
        }

    }
}
