using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem
{
    public static class Validate
    {
        public static Boolean ValidateSearchForCustomer(string validate)
        {
            if(string.IsNullOrEmpty(validate))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static Boolean ValidateRegisterCustomer(string email, string phoneNo, string phoneCountryCode, 
            string creditCardNo, string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phoneNo) || string.IsNullOrEmpty(phoneCountryCode) ||
                string.IsNullOrEmpty(creditCardNo) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
