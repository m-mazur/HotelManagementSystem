using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem
{
    public class CheckInCheckOutController
    {
        private ReservationsRepository reservationRepository;
        private Reservation reservation;

        public CheckInCheckOutController()
        {
            reservationRepository = new ReservationsRepository();
        }

        public DataView FindReservationByReservationNo(string reservationNo)
        {
            return reservationRepository.GetReservationByReservationNo(reservationNo);
        }

        public DataView FindReservationByEmail(string email)
        {
            return reservationRepository.GetReservationByEmail(email);
        }

        public DataView FindActiveReservationByReservationNo(string reservationNo)
        {
            return reservationRepository.GetActiveReservationsByNo(int.Parse(reservationNo));
        }

        public DataView FindActiveReservationByEmail(string email)
        {
            return reservationRepository.GetActiveReservationsByEmail(email);
        }

        public void CheckInReservation(string reservationNo, bool checkIn)
        {
            var tempReservation = reservationRepository.GetSingleReservation(int.Parse(reservationNo));
            reservation = new Reservation(tempReservation.ReservationNo, 
                tempReservation.EMail, tempReservation.RoomNo, 
                tempReservation.CheckInDate, tempReservation.CheckOutDate, 
                checkIn, tempReservation.CheckOut);
            reservationRepository.UpdateReservation(reservation);
        }

        public void CheckOutReservation(string reservationNo, bool checkOut)
        {
            var tempReservation = reservationRepository.GetSingleReservation(int.Parse(reservationNo));
            reservation = new Reservation(tempReservation.ReservationNo, 
                tempReservation.EMail, tempReservation.RoomNo, 
                tempReservation.CheckInDate, tempReservation.CheckOutDate, 
                tempReservation.CheckIn, checkOut);
            reservationRepository.UpdateReservation(reservation);
        }
    }
}
