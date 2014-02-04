using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem
{
    public class Customer
    {
        string eMail;
        string phoneNo;
        string phoneCountryCode;
        string creditCardNo;
        string firstName;
        string lastName;

        public Customer(string eMail, string phoneNo, string phoneCountryCode,
            string creditCardNo, string firstName, string lastName)
        {
            this.eMail = eMail;
            this.phoneNo = phoneNo;
            this.phoneCountryCode = phoneCountryCode;
            this.creditCardNo = creditCardNo;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public string EMail
        {
            get { return eMail; }
            set { eMail = value; }
        }

        public string PhoneNo
        {
            get { return phoneNo; }
            set { phoneNo = value; }
        }

        public string PhoneCountryCode
        {
            get { return phoneCountryCode; }
            set { phoneCountryCode = value; }
        }

        public string CreditCardNo
        {
            get { return creditCardNo; }
            set { creditCardNo = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
    }
}
