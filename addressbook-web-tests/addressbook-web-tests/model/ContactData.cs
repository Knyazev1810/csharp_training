using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmail;
        private string allProperties;
        private string stringOfNames;
        private string partOfNamesAndAddress;
        private string partOfPhones;
        private string partOfEmails;
        private string stringOfBirthday;
        private string stringOfAnniversary;
        private string partOfDates;

        public ContactData()
        {
        }

        public ContactData(string firstName, string lastName)
        {
            Firstname = firstName;
            Lastname = lastName;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Firstname == other.Firstname 
                && Lastname == other.Lastname; 
        }

        public override int GetHashCode()
        {
            return Firstname.GetHashCode() + Lastname.GetHashCode();
        }

        public override string ToString()
        {
            return "\nFirstname=" + Firstname + "\nLastname=" + Lastname;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Firstname.CompareTo(other.Firstname) & Lastname.CompareTo(other.Lastname);
        }

        [Column(Name = "firstname")]
        public string Firstname { get; set; }

        public string Middlename { get; set; }

        [Column(Name = "lastname")]
        public string Lastname { get; set; }

        [Column(Name = "id"), PrimaryKey]
        public string Id { get; set; }

        public string Nickname { get; set; }

        public string Photo { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }

        public string Fax { get; set; }

        public string AllPhones 
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

        public string AllEmail
        {
            get
            {
                if (allEmail != null)
                {
                    return allEmail;
                }
                else
                {
                    return (CleanUpEmail(Email) + CleanUpEmail(Email2) + CleanUpEmail(Email3)).Trim();
                }
            }
            set
            {
                allEmail = value;
            }
        }

        public string Homepage { get; set; }

        public string Birthday { get; set; }

        public string Birthmonth { get; set; }

        public string Birthyear { get; set; }

        public string Anniversaryday { get; set; }

        public string Anniversarymonth { get; set; }

        public string Anniversaryyear { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        public string StringOfNames 
        {
            get
            {
                if (stringOfNames != null)
                {
                    return stringOfNames;
                }
                else
                {
                    return (CleanUpPropertyWithSpace(Firstname) + CleanUpPropertyWithSpace(Middlename) + Lastname.Trim()).Trim();
                }
            }
            set
            {
                stringOfNames = value;
            }
        }

        public string PartOfNamesAndAddress 
        {
            get
            {
                if (partOfNamesAndAddress != null)
                {
                    return partOfNamesAndAddress;
                }
                else
                {
                    return CleanUpStringOfProperty(StringOfNames) + CleanUpStringOfProperty(Nickname)
                        + CleanUpStringOfProperty(Title) + CleanUpStringOfProperty(Company)
                         + CleanUpStringOfProperty(Address);
                }
            }
            set
            {
                partOfNamesAndAddress = value;
            }
        }

        public string PartOfPhones
        {
            get
            {
                if (partOfPhones != null)
                {
                    return partOfPhones;
                }
                else
                {
                    return CleanUpHomePhone(HomePhone) + CleanUpMobilePhone(MobilePhone)
                        + CleanUpWorkPhone(WorkPhone) + CleanUpFax(Fax);      
                }
            }
            set
            {
                partOfPhones = value;
            }
        }

        public string PartOfEmails
        {
            get
            {
                if (partOfEmails != null)
                {
                    return partOfEmails;
                }
                else
                {
                    return CleanUpStringOfProperty(Email) + CleanUpStringOfProperty(Email2) + CleanUpStringOfProperty(Email3)
                        + CleanUpHomepage(Homepage);
                }
            }
            set
            {
                partOfEmails = value;
            }
        }

        public string PartOfDates
        {
            get
            {
                if (partOfDates != null)
                {
                    return partOfDates;
                }
                else
                {
                    return CleanUpStringOfProperty(StringOfBirthday) + StringOfAnniversary;
                }
            }
            set
            {
                partOfDates = value;
            }
        }

        public string Group { get; set; }

        public string AllProperties
        {
            get
            {
                if (allProperties != null)
                {
                    return allProperties;
                }
                {
                    return (CleanUpAllPartsOfProperties(PartOfNamesAndAddress) + CleanUpAllPartsOfProperties(PartOfPhones)
                        + CleanUpAllPartsOfProperties(PartOfEmails) + PartOfDates.Trim()).Trim();
                }
            }
            set
            {
                allProperties = value;
            }
        }

        public string StringOfBirthday
        {
            get
            {
                if (stringOfBirthday != null || stringOfBirthday != "")
                {
                    return stringOfBirthday;
                }
                else
                {
                    return "Birthday " + CleanUpPropertyWithDotAndSpase(Birthday)
                        + CleanUpMonth(Birthmonth) + Birthyear.Trim();
                }
            }
            set
            {
                stringOfBirthday = value;
            }
        }
        public string StringOfAnniversary
        {
            get
            {
                if (stringOfAnniversary != null || stringOfAnniversary != "")
                {
                    return stringOfAnniversary;
                }
                else
                {
                    return "Anniversary " + CleanUpPropertyWithDotAndSpase(Anniversaryday)
                        + CleanUpMonth(Anniversarymonth) + Anniversaryyear.Trim();
                }
            }
            set
            {
                stringOfAnniversary = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }

        private string CleanUpEmail(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            return email + "\r\n";
        }

        private string CleanUpPropertyWithSpace(string property)
        {
            if (property == null || property == "")
            {
                return "";
            }
            return property + " ";
        }

        private string CleanUpHomepage(string homepage)
        {
            if (homepage == null || homepage == "")
            {
                return "";
            }
            return "Homepage:" + "\r\n" + homepage + "\r\n";
        }

        private string CleanUpPropertyWithDotAndSpase(string property)
        {
            if (property == null || property == "" || property == "0")
            {
                return "";
            }
            return property + ". ";
        }

        private string CleanUpMonth(string month)
        {
            if (month == null || month == "" || month == "-")
            {
                return "";
            }
            return month + " ";
        }

        private string CleanUpStringOfProperty(string property)
        {
            if (property == null || property == "")
            {
                return "";
            }
            return property + "\r\n";
        }

        private string CleanUpHomePhone(string homePhone)
        {
            if (homePhone == null || homePhone == "")
            {
                return "";
            }
            return "H: " + homePhone + "\r\n";
        }

        private string CleanUpMobilePhone(string mobilePhone)
        {
            if (mobilePhone == null || mobilePhone == "")
            {
                return "";
            }
            return "M: " + mobilePhone + "\r\n";
        }

        private string CleanUpWorkPhone(string workPhone)
        {
            if (workPhone == null || workPhone == "")
            {
                return "";
            }
            return "W: " + workPhone + "\r\n";
        }

        private string CleanUpFax(string fax)
        {
            if (fax == null || fax == "")
            {
                return "";
            }
            return "F: " + fax + "\r\n";
        }

        private string CleanUpAllPartsOfProperties(string partOfProperties)
        {
            if (partOfProperties == null || partOfProperties == "")
            {
                return "";
            }
            {
                
            }
                return partOfProperties + "\r\n";
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == null) select c).ToList();
            }
        }
    }
}
