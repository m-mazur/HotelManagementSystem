using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem
{
    public class OrderUtility
    {
        private ReservationsRepository reservationReposistory;

        public string TotalOrderValue(double roomPrice, string reservationNo)
        {
            return (roomPrice * TotalNights(reservationNo)).ToString();
        }

        public int TotalNights(string reservationNo)
        {
            reservationReposistory = new ReservationsRepository();
            var tempReservation = reservationReposistory.GetSingleReservation(int.Parse(reservationNo));
            TimeSpan timeSpan = tempReservation.CheckOutDate - tempReservation.CheckInDate;
            int totalDays = timeSpan.Days;
            return totalDays;
        }
    }
}
