using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class RemovingContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void RemovingContactFromGroupTest()
        {
            app.Groups.CheckGroupsListIsNotEmpty(0);
            app.Contacts.CheckContactsListIsNotEmpty(0);

            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            if (oldList.Count == 0)
            {
                List<ContactData> allContactsList = ContactData.GetAll();
                ContactData contactForGroup = allContactsList.First();
                app.Contacts.AddContactToGroup(contactForGroup, group);
            }
            ContactData contact = group.GetContacts().First();

            app.Contacts.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
