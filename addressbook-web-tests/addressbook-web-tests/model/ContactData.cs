using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmail;
        private string allProperties;
        private string stringOfNames;
        private string partOfNamesAndAddress;
        private string homePhone;
        private string mobilePhone;
        private string workPhone;
        private string fax;
        private string partOfPhones;
        private string homePage;
        private string stringOfBirthday;
        private string stringOfAnniversary;

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
            return "Firstname and Lastname=" + Firstname + " " + Lastname;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Firstname.CompareTo(other.Firstname) & Lastname.CompareTo(other.Lastname);
        }

        public string Firstname { get; set; }

        public string Middlename { get; set; }

        public string Lastname { get; set; }

        public string Nickname { get; set; }

        public string Photo { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }

        public string Address { get; set; }

        public string HomePhone 
        {
            get
            {
                if (homePhone != null)
                {
                    return homePhone;
                }
                else
                {
                    return "H: " + HomePhone.Trim();
                }
            }
            set
            {
                homePhone = value;
            }
        }

        public string MobilePhone
        {
            get
            {
                if (mobilePhone != null)
                {
                    return mobilePhone;
                }
                else
                {
                    return "M: " + MobilePhone.Trim();
                }
            }
            set
            {
                mobilePhone = value;
            }
        }

        public string WorkPhone
        {
            get
            {
                if (workPhone != null)
                {
                    return workPhone;
                }
                else
                {
                    return "W: " + WorkPhone.Trim();
                }
            }
            set
            {
                workPhone = value;
            }
        }

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

        public string Fax
        {
            get
            {
                if (fax != null)
                {
                    return fax;
                }
                else
                {
                    return "F: " + Fax.Trim();
                }
            }
            set
            {
                fax = value;
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

        public string Homepage
        {
            get
            {
                if (homePage != null)
                {
                    return homePage;
                }
                else
                {
                    return CleanUpStringOfProperty(Email) + CleanUpStringOfProperty(Email2) + CleanUpStringOfProperty(Email3)
                        + CleanUpHomepage(Homepage).Trim();
                }
            }
            set
            {
                homePage = value;
            }
        }

        public string Birthday { get; set; }

        public string Birthmonth { get; set; }

        public string Birthyear { get; set; }

        public string Anniversaryday { get; set; }

        public string Anniversarymonth { get; set; }

        public string Anniversaryyear { get; set; }

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
                    return CleanUpPropertyWithSpace(Firstname) + CleanUpPropertyWithSpace(Middlename) + Lastname.Trim();
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
                         + CleanUpStringOfProperty(Address).Trim();
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
                    return CleanUpStringOfProperty(HomePhone) + CleanUpStringOfProperty(MobilePhone)
                        + CleanUpStringOfProperty(WorkPhone) + CleanUpStringOfProperty(Fax).Trim();      
                }
            }
            set
            {
                partOfPhones = value;
            }
        }

        public string PartOfEmails { get; set; }

        public string PartOfDates { get; set; }

        public string Group { get; set; }

        public string Id { get; set; }

        public string AllProperties
        {
            get
            {
                if (allProperties != null)
                {
                    return allProperties;
                }
                else
                {
                    return CleanUpAllPartsOfProperties(PartOfNamesAndAddress) + CleanUpAllPartsOfProperties(PartOfPhones)
                        + CleanUpAllPartsOfProperties(PartOfEmails) + PartOfDates.Trim();
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
                if (stringOfBirthday != null)
                {
                    return stringOfBirthday;
                }
                else
                {
                    if (Birthday != null || Birthmonth != null || Birthyear != null)
                    {
                        return "Birthday " + CleanUpPropertyWithDotAndSpase(Birthday) 
                            + CleanUpPropertyWithSpace(Birthmonth) + Birthyear.Trim();
                    }
                    else
                    {
                        return "";
                    }
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
                if (stringOfAnniversary != null)
                {
                    return stringOfAnniversary;
                }
                else
                {
                    if (Anniversaryday != null || Anniversarymonth != null || Birthyear != null)
                    {
                        return "Anniversary " + CleanUpPropertyWithDotAndSpase(Anniversaryday) 
                            + CleanUpPropertyWithSpace(Anniversarymonth) + Anniversaryyear.Trim();
                    }
                    else
                    {
                        return "";
                    }
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
            return "Homepage:" + "\r\n" + homepage;
        }

        private string CleanUpPropertyWithDotAndSpase(string property)
        {
            if (property == null || property == "")
            {
                return "";
            }
            return property + ". ";
        }
        private string CleanUpStringOfProperty(string property)
        {
            if (property == null || property == "")
            {
                return "";
            }
            return property + "\r\n";
        }

        private string CleanUpAllPartsOfProperties(string partOfProperties)
        {
            if (partOfProperties == null || partOfProperties == "")
            {
                return "";
            }
            return partOfProperties + "\r\n";
        }
    }
}
