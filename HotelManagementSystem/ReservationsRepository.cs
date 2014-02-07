using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HotelManagementSystem
{
    class ReservationsRepository
    {
        private DataSetHotelTableAdapters.reservationsTableAdapter reservationsTableAdapter;
        private DataSetHotel dataSetHotel;

        public ReservationsRepository()
        {
            dataSetHotel = new DataSetHotel();
        }

        public void AddReservation(Reservation reservation)
        {
            reservationsTableAdapter = new DataSetHotelTableAdapters.reservationsTableAdapter();
            try
            {
                reservationsTableAdapter.Insert(reservation.ReservationNo, reservation.EMail, reservation.RoomNo,
                    reservation.CheckInDate, reservation.CheckOutDate, reservation.CheckIn, reservation.CheckOut);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteReservation(Reservation reservation)
        {
            reservationsTableAdapter = new DataSetHotelTableAdapters.reservationsTableAdapter();
            try
            {
                reservationsTableAdapter.Delete(reservation.ReservationNo, reservation.EMail, reservation.RoomNo,
                    reservation.CheckInDate, reservation.CheckOutDate, reservation.CheckIn, reservation.CheckOut);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void UpdateReservation(Reservation reservation)
        {
            reservationsTableAdapter = new DataSetHotelTableAdapters.reservationsTableAdapter();
            reservationsTableAdapter.Fill(dataSetHotel.reservations);
            DataSetHotel.reservationsRow reservationsRow = dataSetHotel.reservations.FindByreservation_no(reservation.ReservationNo);
            try
            {
                reservationsRow.e_mail = reservation.EMail;
                reservationsRow.room_no = reservation.RoomNo;
                reservationsRow.check_in_date = reservation.CheckInDate;
                reservationsRow.check_out_date = reservation.CheckOutDate;
                reservationsRow.check_in = reservation.CheckIn;
                reservationsRow.check_out = reservation.CheckOut;
                reservationsTableAdapter.Update(reservationsRow);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public DataView GetReservations()
        {
            try
            {
                reservationsTableAdapter = new DataSetHotelTableAdapters.reservationsTableAdapter();
                reservationsTableAdapter.Fill(dataSetHotel.reservations);
                DataView reservationDataView = new DataView(dataSetHotel.Tables["Reservations"]);
                return reservationDataView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public Reservation GetSingleReservation(string reservationNo)
        {
            try
            {
                reservationsTableAdapter = new DataSetHotelTableAdapters.reservationsTableAdapter();
                reservationsTableAdapter.Fill(dataSetHotel.reservations);
                DataSetHotel.reservationsRow reservationRow = dataSetHotel.reservations.FindByreservation_no(reservationNo);
                return new Reservation(reservationRow.reservation_no, reservationRow.e_mail, reservationRow.room_no, 
                    reservationRow.check_in_date,reservationRow.check_out_date, reservationRow.check_in, reservationRow.check_out);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public DataView GetReservationByReservationNo(string reservationNo)
        {
            try
            {
                reservationsTableAdapter = new DataSetHotelTableAdapters.reservationsTableAdapter();
                reservationsTableAdapter.FillByReservationNo(dataSetHotel.reservations, reservationNo);
                DataView reservationDataView = new DataView(dataSetHotel.Tables["Reservations"]);
                return reservationDataView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
