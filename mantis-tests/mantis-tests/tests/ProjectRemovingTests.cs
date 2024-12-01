using System;
using System.Collections.Generic;
using System.IO;
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
            app.Projects.CheckProjectListIsNotEmpty();

            List<ProjectData> oldProjects = app.Projects.GetOldProjectList();

            app.Projects.Remove(0);

            List<ProjectData> newProjects = app.Projects.GetNewProjectList();

            Assert.AreEqual(oldProjects.Count - 1, newProjects.Count);
        }

    }
}
