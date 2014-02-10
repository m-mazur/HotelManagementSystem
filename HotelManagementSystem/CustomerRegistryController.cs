using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem
{
    public class CustomerRegistryController
    {
        private CustomersRepository customerRepository;
        private DataView customerDataView;

        public CustomerRegistryController()
        {
            customerRepository = new CustomersRepository();
        }

        public DataView GetCustomerDataView()
        {
            customerDataView = customerRepository.GetCustomers();
            return customerDataView;
        }

        public Customer GetCustomer(string email)
        {
            return null;
        }

        public DataView FindReservationByEmail(string email)
        {
            return new ReservationsRepository().GetReservationByEmail(email);
        }
    }
}
