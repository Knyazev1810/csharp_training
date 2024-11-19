using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void AddingContactToGroupTest()
        {
            app.Groups.CheckGroupsListIsNotEmpty(0);
            app.Contacts.CheckContactsListIsNotEmpty(0);

            GroupData group = GroupData.GetAll()[0];
            List<ContactData> allContactsList = ContactData.GetAll();
            List<ContactData> oldList = group.GetContacts();
            if (allContactsList.Count == oldList.Count)
            {
                ContactData newContact = new ContactData("aa", "bb");
                app.Contacts.Create(newContact);
            }
            ContactData contact = ContactData.GetAll().Except(oldList).First();

            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
