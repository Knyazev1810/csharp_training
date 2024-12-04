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
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager) : base(manager) { }

        public List<ProjectData> GetProjectList(AccountData account)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            List<ProjectData> projects = new List<ProjectData>();
            manager.LeftPanel.GoToManagement();
            manager.ManagementPanel.GoToProjectManagement();
            client.mc_projects_get_user_accessible(account.Username, account.Password);
            return projects;
        }
    }
}
