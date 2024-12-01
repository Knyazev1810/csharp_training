using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) : base(manager) { }

        public ProjectManagementHelper Add(ProjectData project)
        {
            manager.LeftPanel.GoToManagement();
            manager.ManagementPanel.GoToProjectManagement();
            InitProjectCreation();
            FillProjectForm(project);
            SubmitProjectCreation();
            WaitForProjectList();
            return this;
        }

        public ProjectManagementHelper Remove(int p)
        {
            manager.LeftPanel.GoToManagement();
            manager.ManagementPanel.GoToProjectManagement();
            SelectProject(p);
            RemoveProject();
            SubmiRemovingProject();
            WaitForProjectList();
            return this;
        }

        public void CheckProjectListIsNotEmpty()
        {
            manager.LeftPanel.GoToManagement();
            manager.ManagementPanel.GoToProjectManagement();
            if (IsElementPresent(By.CssSelector("i.fa.fa-check.fa-lg")))
            {
                return;
            }
            ProjectData project = new ProjectData("test");
            Add(project);
        }

        public ProjectManagementHelper SubmiRemovingProject()
        {
            driver.FindElement(By.XPath("//input[@value='Delete Project']")).Click();
            return this;
        }

        public ProjectManagementHelper RemoveProject()
        {
            driver.FindElement(By.XPath("(//form[@action='manage_proj_delete.php'])")).Click();
            return this;
        }

        public ProjectManagementHelper SelectProject(int index)
        {
            driver.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"))[index]
                .FindElements(By.TagName("td"))[0].FindElement(By.TagName("a")).Click();
            return this;
        }

        public ProjectManagementHelper SubmitProjectCreation()
        {
            driver.FindElement(By.CssSelector("input.btn.btn-primary.btn-white.btn-round")).Click();
            return this;
        }

        public ProjectManagementHelper FillProjectForm(ProjectData project)
        {
            Type(By.Name("name"), project.Name);
            return this;
        }

        public ProjectManagementHelper InitProjectCreation()
        {
            driver.FindElement(By.XPath("(//form[@action='manage_proj_create_page.php'])")).Click();
            return this;
        }

        public List<ProjectData> GetOldProjectList()
        {
            List<ProjectData> projects = new List<ProjectData>();
            manager.LeftPanel.GoToManagement();
            manager.ManagementPanel.GoToProjectManagement();
            ICollection<IWebElement> elements = driver.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
            foreach (IWebElement element in elements)
            {
                IList<IWebElement> cells = element.FindElements(By.TagName("td"));
                IWebElement Name = cells[0];
                for (int i = 1; i < 2; i++)
                {
                    projects.Add(new ProjectData(Name.Text));
                }
            }
            return projects;
        }
        public List<ProjectData> GetNewProjectList()
        {
            List<ProjectData> projects = new List<ProjectData>();
            manager.ManagementPanel.GoToProjectManagement();
            ICollection<IWebElement> elements = driver.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
            foreach (IWebElement element in elements)
            {
                IList<IWebElement> cells = element.FindElements(By.TagName("td"));
                IWebElement Name = cells[0];
                for (int i = 1; i < 2; i++)
                {
                    projects.Add(new ProjectData(Name.Text));
                }
            }
            return projects;
        }

        public void WaitForProjectList()
        {
            WebDriverWait waitForElement = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            waitForElement.Until(d => driver.FindElements(By.XPath("(//form[@action='manage_proj_create_page.php'])")).Count > 0);
        }
    }
}
