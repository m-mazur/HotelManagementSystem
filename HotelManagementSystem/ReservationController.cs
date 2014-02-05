using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem
{
    public class ReservationController
    {
        private Customer customer;
        private CustomersRepository customerRepository;

        public ReservationController()
        {
            customerRepository = new CustomersRepository();
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
    }
}
