using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager) { }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn())
                {
                    return;
                }

                Logout();
            }
            driver.FindElement(By.Name("username")).SendKeys(account.Username);
            driver.FindElement(By.CssSelector("input.width-40.pull-right.btn.btn-success.btn-inverse.bigger-110")).Click();
            driver.FindElement(By.Name("password")).SendKeys(account.Password);
            driver.FindElement(By.CssSelector("input.width-40.pull-right.btn.btn-success.btn-inverse.bigger-110")).Click();
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.CssSelector("a.dropdown-toggle")).Click();
                driver.FindElement(By.XPath("(//a[@href='/mantisbt-2.25.0/logout_page.php'])")).Click();
    }
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.CssSelector("div.navbar-buttons.navbar-header.navbar-collapse.collapse"));
        }
    }
}
