using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem
{
    public class CalculateTotalOrderValue
    {
        public string TotalOrderValue(double roomPrice, int totalDays)
        {
            return (roomPrice * totalDays).ToString();
        }
    }
}
