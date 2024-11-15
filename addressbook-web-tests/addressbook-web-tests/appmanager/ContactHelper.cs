using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToContactCreationPage();

            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
            return this;
        }
        public ContactHelper Modify(int p, ContactData newData)
        {
            InitContactModification(p);
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

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
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
            contactCache = null;
            return this;
        }
        public ContactHelper InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }
        public void CheckContactAvailibility(int index)
        {
            manager.Navigator.GoToHomePage();

            if (IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")))
            {
                return;
            }
            ContactData contact = new ContactData("aa", "bb");
            Create(contact);
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));
                foreach (IWebElement element in elements)
                {
                    IList<IWebElement> cells = element.FindElements(By.TagName("td"));
                    IWebElement lastName = cells[1];
                    IWebElement firstName = cells[2];
                    for (int i = 1; i < 2; i++)
                    {                       
                        contactCache.Add(new ContactData(firstName.Text, lastName.Text)
                        {
                            Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                        });
                    }
                }
            }
            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.Name("entry")).Count;
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmail = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmail = allEmail
            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(0);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string middleName = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string nickName = driver.FindElement(By.Name("nickname")).GetAttribute("value");

            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string fax = driver.FindElement(By.Name("fax")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            string homePage = driver.FindElement(By.Name("homepage")).GetAttribute("value");

            string birthDay = driver.FindElement(By.Name("bday")).GetAttribute("value");
            string birthMonth = driver.FindElement(By.Name("bmonth")).GetAttribute("value");
            string birthYear = driver.FindElement(By.Name("byear")).GetAttribute("value");

            string anniversaryDay = driver.FindElement(By.Name("aday")).GetAttribute("value");
            string anniversaryMonth = driver.FindElement(By.Name("amonth")).GetAttribute("value");
            string anniversaryYear = driver.FindElement(By.Name("ayear")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Middlename = middleName,
                Nickname = nickName,
                Company = company,
                Title = title,
                Address = address, 
                HomePhone = homePhone, 
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Fax = fax,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                Homepage = homePage,
                Birthday = birthDay,
                Birthmonth = birthMonth,
                Birthyear = birthYear,
                Anniversaryday = anniversaryDay,
                Anniversarymonth = anniversaryMonth.Substring(0, 1).ToUpper() + anniversaryMonth.Substring(1).ToLower(),
                Anniversaryyear = anniversaryYear
            };
        }

        public ContactData GetContactInformationFromPropertiesPage(int index)
        {
            manager.Navigator.GoToHomePage();
            GoToContactDetailsPage(index);
            string firstName = driver.FindElement(By.Id("content")).Text;
            string lastName = driver.FindElement(By.Id("content")).Text;
            string stringOfNames = driver.FindElement(By.Id("content")).Text;
            string partOfNamesAndAddress = driver.FindElement(By.Id("content")).Text;

            string homePhone = driver.FindElement(By.Id("content")).Text;
            string mobilePhone = driver.FindElement(By.Id("content")).Text;
            string workPhone = driver.FindElement(By.Id("content")).Text;
            string fax = driver.FindElement(By.Id("content")).Text;
            string partOfPhones = driver.FindElement(By.Id("content")).Text;

            string homePage = driver.FindElement(By.Id("content")).Text;
            string partOfEmails = driver.FindElement(By.Id("content")).Text;

            string birthDay = driver.FindElement(By.Id("content")).Text;
            string birthMonth = driver.FindElement(By.Id("content")).Text;
            string birthYear = driver.FindElement(By.Id("content")).Text;
            string anniversaryDay = driver.FindElement(By.Id("content")).Text;
            string anniversaryMonth = driver.FindElement(By.Id("content")).Text;
            string anniversaryYear = driver.FindElement(By.Id("content")).Text;
            string stringOfBirthday = driver.FindElement(By.Id("content")).Text;
            string stringOfAnniversary = driver.FindElement(By.Id("content")).Text;
            string partOfDates = driver.FindElement(By.Id("content")).Text;

            string allProperties = driver.FindElement(By.Id("content")).Text;

            return new ContactData(firstName, lastName)
            {
                StringOfNames = stringOfNames,
                PartOfNamesAndAddress = partOfNamesAndAddress,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Fax = fax,
                PartOfPhones = partOfPhones,
                Homepage = homePage,
                PartOfEmails = partOfEmails,
                Birthday = birthDay,
                Birthmonth = birthMonth,
                Birthyear = birthYear,
                Anniversaryday = anniversaryDay,
                Anniversarymonth = anniversaryMonth,
                Anniversaryyear = anniversaryYear,
                StringOfBirthday = stringOfBirthday,
                StringOfAnniversary = stringOfAnniversary,
                PartOfDates = partOfDates,
                AllProperties = allProperties
            };
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.Id("search_count")).Text;
            return Int32.Parse(text);
        }

        public ContactHelper GoToContactDetailsPage(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click();
            return this;
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        public void SelectContact(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }
    }
}
