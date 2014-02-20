using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem
{
    public class ReservationController
    {
        private Customer customer;
        private CustomersRepository customerRepository;
        private ReservationsRepository reservationRepository;

        public ReservationController()
        {
            customerRepository = new CustomersRepository();
            reservationRepository = new ReservationsRepository();
        }

        public Customer GetCustomer(string email)
        {
            customer = new Customer(customerRepository.GetCustomerByEmail(email).e_mail,
                customerRepository.GetCustomerByEmail(email).phone_no,
                customerRepository.GetCustomerByEmail(email).phone_country_code,
                customerRepository.GetCustomerByEmail(email).credit_card_no,
                customerRepository.GetCustomerByEmail(email).first_name,
                customerRepository.GetCustomerByEmail(email).last_name);
            return customer;
        }

        public int GetLatestReservationNo()
        {
            return reservationRepository.GetLatestReservationNo();
        }

        public void AddReservation(string eMail, string roomNo,
            DateTime checkInDate, DateTime checkOutDate, Boolean checkIn, Boolean checkOut)
        {
            reservationRepository.AddReservation(new Reservation(eMail, roomNo, 
                checkInDate, checkOutDate, checkIn, checkOut));
        }

        public DataView GetAvailableRooms(string roomType, DateTime startDate, DateTime endDate)
        {
            return new RoomsRepository().GetAvailableRooms(roomType, startDate, endDate);
        }

        public void AddCustomer(string eMail, string phoneNo, string phoneCountryCode,
            string creditCardNo, string firstName, string lastName)
        {
            customerRepository.AddCustomer(new Customer(eMail, phoneNo, phoneCountryCode,
                creditCardNo, firstName, lastName));
        }

        public Reservation GetSingleReservation(string reservationNo)
        {
            return reservationRepository.GetSingleReservation(reservationNo);
        }

        public Room GetRoom(string roomNo)
        {
            return new RoomsRepository().GetRoomByRoomNo(roomNo);
        }

        public String GetTotalOrderValue(double roomPrice, string reservationNo)
        {
            return new OrderUtility().TotalOrderValue(roomPrice, reservationNo);
        }

        public DataView GetReservations()
        {
            return new ReservationsRepository().GetReservations();
        }
    }
}
