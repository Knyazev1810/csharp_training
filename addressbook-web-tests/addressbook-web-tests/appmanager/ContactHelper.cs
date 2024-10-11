using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToContactCreationPage();

            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
            return this;
        }
        public ContactHelper Modify(ContactData newData)
        {
            InitContactModification();
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(int p)
        {
            manager.Navigator.GoToHomePage();

            SelectContact(p);
            RemoveContact();
            manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }
        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }
        public ContactHelper SelectContact(int index)
        {
             driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index+1) + "]")).Click();
             return this;
        }
        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }
        public ContactHelper InitContactModification()
        {
            driver.FindElement(By.XPath("//img[@alt='Edit']")).Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }
        public void CheckContactAvailibility(int index)
        {
            if (IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")))
            {
                return;
            }
            ContactData contact = new ContactData("aa", "bb");
            Create(contact);
        }

        public void CheckContactAvailibilityForMod()
        {
            if (IsElementPresent(By.XPath("//img[@alt='Edit']")))
            {
                return;
            }
            ContactData contact = new ContactData("aa", "bb");
            Create(contact);
        }
    }
}
