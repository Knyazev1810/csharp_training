using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        protected LoginHelper loginHelper;
        protected ManagementPanelHelper managementPanelHelper;
        protected LeftPanelHelper leftPanelHelper;
        protected ProjectManagementHelper projectManagementHelper;

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost/mantisbt-2.25.0";

            loginHelper = new LoginHelper(this);
            managementPanelHelper = new ManagementPanelHelper(this, baseURL);
            leftPanelHelper = new LeftPanelHelper(this, baseURL);
            projectManagementHelper = new ProjectManagementHelper(this);
            API = new APIHelper(this);
        }

        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                 // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance()
        {
            if (! app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.LeftPanel.GoToAccountPage();
                app.Value = newInstance;
            }
            return app.Value;
        }

        public IWebDriver Driver 
        {
            get 
            {
                return driver; 
            }
        }

        public LoginHelper Auth
        {
            get
            {
                return loginHelper;
            }
        }
        public ManagementPanelHelper ManagementPanel
        {
            get
            {
                return managementPanelHelper;
            }
        }
        public LeftPanelHelper LeftPanel
        {
            get
            {
                return leftPanelHelper;
            }
        }
        public ProjectManagementHelper Projects
        {
            get
            {
                return projectManagementHelper;
            }
        }
        public APIHelper API { get; set; }
    }
}
