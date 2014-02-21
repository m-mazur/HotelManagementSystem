using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem
{
    public class RegistryController
    {
        private CustomersRepository customerRepository;
        private ReservationsRepository reservationRepository;
        private DataView customerDataView;

        public RegistryController()
        {
            customerRepository = new CustomersRepository();
            reservationRepository = new ReservationsRepository();

        }

        public DataView GetCustomerDataView()
        {
            customerDataView = customerRepository.GetCustomers();
            return customerDataView;
        }
       
        public Customer GetCustomer(string email)
        {
            Customer customer = new Customer(customerRepository.GetCustomerByEmail(email).e_mail,
                customerRepository.GetCustomerByEmail(email).phone_no,
                customerRepository.GetCustomerByEmail(email).phone_country_code,
                customerRepository.GetCustomerByEmail(email).credit_card_no,
                customerRepository.GetCustomerByEmail(email).first_name,
                customerRepository.GetCustomerByEmail(email).last_name);
            return customer;
          
        }

        public DataView FindReservationByEmail(string email)
        {
            return reservationRepository.GetReservationByEmail(email);
        }

        public DataView FindReservationByNo(string reservationNo)
        {
            return reservationRepository.GetReservationByReservationNo(reservationNo);
        }

        public DataView FindCustomerByEmail(string email)
        {
            return customerRepository.FindCustomersByEmail(email);
        }

        public DataView FindCustomersByName(string firstName, string lastName)
        {
            return customerRepository.FindCustomersByName(firstName, lastName);
        }
    }
}
