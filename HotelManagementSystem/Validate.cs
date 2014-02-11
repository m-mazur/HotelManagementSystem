using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem
{
    public static class Validate
    {
        public static Boolean ValidString(string validate)
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
    }
}
