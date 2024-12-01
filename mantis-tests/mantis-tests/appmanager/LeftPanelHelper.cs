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
    public class LeftPanelHelper : HelperBase
    {
        private string baseURL;

        public LeftPanelHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }

        public void GoToManagement()
        {
            if (driver.Url == baseURL + "/manage_overview_page.php"
                && IsElementPresent(By.XPath("(//a[@href='/mantisbt-2.25.0/manage_proj_page.php'])")))
            {
                return;
            }
            driver.FindElement(By.XPath("(//a[@href='/mantisbt-2.25.0/manage_overview_page.php'])")).Click();
        }

        public void GoToAccountPage()
        {
            if (driver.Url == baseURL + "/account_page.php")
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL + "/account_page.php");
        }
    }
}
