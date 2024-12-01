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
    public class ManagementPanelHelper : HelperBase
    {
        private string baseURL;

        public ManagementPanelHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }

        public void GoToProjectManagement()
        {
            if (driver.Url == baseURL + "/manage_proj_page.php"
                && IsElementPresent(By.XPath("(//form[@action='manage_proj_create_page.php'])")))
            {
                return;
            }
            driver.FindElement(By.XPath("(//a[@href='/mantisbt-2.25.0/manage_proj_page.php'])")).Click();
        }
    }
}
