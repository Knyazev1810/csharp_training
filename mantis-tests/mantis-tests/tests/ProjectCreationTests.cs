using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {
        public static IEnumerable<ProjectData> RandomProjectDataProvider()
        {
            List<ProjectData> projects = new List<ProjectData>();
            for (int i = 0; i < 5; i++)
            {
                projects.Add(new ProjectData(GenerateRandomString(10)));
            }
            return projects;
        }

        [Test, TestCaseSource("RandomProjectDataProvider")]
        public void ProjectCreationTest(ProjectData project)
        {
            AccountData account = new AccountData()
            {
                Username = "administrator",
                Password = "root"
            };

            List<ProjectData> oldProjects = app.Projects.GetProjectListAPI(account);

            app.Projects.Add(project);

            List<ProjectData> newProjects = app.Projects.GetProjectListAPI(account);

            Assert.AreEqual(oldProjects.Count + 1, newProjects.Count);
        }
    }
}
