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
            TimeSpan timeSpan = reservationReposistory.GetSingleReservation(reservationNo).CheckOutDate -
                reservationReposistory.GetSingleReservation(reservationNo).CheckInDate;
            int totalDays = timeSpan.Days;
            return totalDays;
        }
    }
}
