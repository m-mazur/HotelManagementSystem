using System.Data;
using HotelManagementSystem.Model.Database;

namespace HotelManagementSystem
{
    class ReservationsRepository
    {
        private HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.reservationsTableAdapter reservationsTableAdapter;
        private DataSetHotel dataSetHotel;

        public ReservationsRepository()
        {
            dataSetHotel = new DataSetHotel();
        }

        public void AddReservation(Reservation reservation)
        {
            reservationsTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.reservationsTableAdapter();
            reservationsTableAdapter.Insert(reservation.EMail, reservation.RoomNo,
                reservation.CheckInDate, reservation.CheckOutDate, reservation.CheckIn, reservation.CheckOut);
        }

        public void DeleteReservation(Reservation reservation)
        {
            reservationsTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.reservationsTableAdapter();
            reservationsTableAdapter.Delete(int.Parse(reservation.ReservationNo), reservation.EMail, reservation.RoomNo,
                reservation.CheckInDate, reservation.CheckOutDate, reservation.CheckIn, reservation.CheckOut);
        }

        public void UpdateReservation(Reservation reservation)
        {
            reservationsTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.reservationsTableAdapter();
            reservationsTableAdapter.Fill(dataSetHotel.reservations);
            DataSetHotel.reservationsRow reservationsRow = dataSetHotel.reservations.FindByreservation_no(int.Parse(reservation.ReservationNo));
            reservationsRow.e_mail = reservation.EMail;
            reservationsRow.room_no = reservation.RoomNo;
            reservationsRow.check_in_date = reservation.CheckInDate;
            reservationsRow.check_out_date = reservation.CheckOutDate;
            reservationsRow.check_in = reservation.CheckIn;
            reservationsRow.check_out = reservation.CheckOut;
            reservationsTableAdapter.Update(reservationsRow);
        }

        public DataView GetReservations()
        {
            reservationsTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.reservationsTableAdapter();
            reservationsTableAdapter.Fill(dataSetHotel.reservations);
            DataView reservationDataView = new DataView(dataSetHotel.Tables["Reservations"]);
            return reservationDataView;
        }

        public Reservation GetSingleReservation(string reservationNo)
        {
            reservationsTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.reservationsTableAdapter();
            reservationsTableAdapter.Fill(dataSetHotel.reservations);
            DataSetHotel.reservationsRow reservationRow = dataSetHotel.reservations.FindByreservation_no(int.Parse(reservationNo));
            return new Reservation(reservationNo.ToString(), reservationRow.e_mail, reservationRow.room_no,
                reservationRow.check_in_date, reservationRow.check_out_date, reservationRow.check_in, reservationRow.check_out);
        }

        public DataView GetReservationByReservationNo(string reservationNo)
        {
            reservationsTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.reservationsTableAdapter();
            reservationsTableAdapter.FillByReservationNo(dataSetHotel.reservations, int.Parse(reservationNo));
            DataView reservationDataView = new DataView(dataSetHotel.Tables["Reservations"]);
            return reservationDataView;
        }

        public DataView GetReservationByEmail(string email)
        {
            reservationsTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.reservationsTableAdapter();
            reservationsTableAdapter.FillByReservationEmail(dataSetHotel.reservations, email);
            DataView reservationDataView = new DataView(dataSetHotel.Tables["Reservations"]);
            return reservationDataView;
        }

        public DataView GetNumberOfReservedDays(string reservationNo)
        {
                reservationsTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.reservationsTableAdapter();
                reservationsTableAdapter.FillByReservedDays(dataSetHotel.reservations, int.Parse(reservationNo));
                DataView reservationDataView = new DataView(dataSetHotel.Tables["Reservations"]);
                return reservationDataView;
        }
    }
}
