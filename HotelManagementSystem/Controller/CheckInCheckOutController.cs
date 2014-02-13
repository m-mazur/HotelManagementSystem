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

        public void CheckInReservation(string reservationNo, bool checkIn)
        {
            reservation = new Reservation(reservationRepository.GetSingleReservation(reservationNo).ReservationNo, 
                reservationRepository.GetSingleReservation(reservationNo).EMail,
                reservationRepository.GetSingleReservation(reservationNo).RoomNo, 
                reservationRepository.GetSingleReservation(reservationNo).CheckInDate,
                reservationRepository.GetSingleReservation(reservationNo).CheckOutDate, checkIn,
                reservationRepository.GetSingleReservation(reservationNo).CheckOut);

            reservationRepository.UpdateReservation(reservation);
        }

        public void CheckOutReservation(string reservationNo, bool checkOut)
        {
            reservation = new Reservation(reservationRepository.GetSingleReservation(reservationNo).ReservationNo, 
                reservationRepository.GetSingleReservation(reservationNo).EMail, 
                reservationRepository.GetSingleReservation(reservationNo).RoomNo, 
                reservationRepository.GetSingleReservation(reservationNo).CheckInDate, 
                reservationRepository.GetSingleReservation(reservationNo).CheckOutDate, 
                reservationRepository.GetSingleReservation(reservationNo).CheckIn, checkOut);

            reservationRepository.UpdateReservation(reservation);
        }
    }
}
