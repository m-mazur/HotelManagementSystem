using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem
{
    public class CalculateTotalOrderValue
    {
        private int roomPrice;
        private int totalDays;

        public string TotalOrderValue(string roomPriceString, string totalDaysString)
        {
            roomPrice = int.Parse(roomPriceString);
            totalDays = int.Parse(totalDaysString);
            return (roomPrice * totalDays).ToString();
        }
    }
}
