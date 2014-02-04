using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HotelManagementSystem
{
    public class CustomersRepository
    {
        private DataSetHotelTableAdapters.customersTableAdapter customersTableAdapter;
        private DataSetHotel dataSetHotel;

        public CustomersRepository()
        {
            dataSetHotel = new DataSetHotel();
        }

        public void AddCustomer(Customers customer)
        {
            customersTableAdapter = new DataSetHotelTableAdapters.customersTableAdapter();
            try
            {
                customersTableAdapter.Insert(customer.EMail, customer.PhoneNo, customer.PhoneCountryCode, customer.CreditCardNo, customer.FirstName, customer.LastName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteCustomer(Customers customer)
        {
            customersTableAdapter = new DataSetHotelTableAdapters.customersTableAdapter();
            try
            {
                customersTableAdapter.Delete(customer.EMail, customer.PhoneNo, customer.PhoneCountryCode, customer.CreditCardNo, customer.FirstName, customer.LastName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void UpdateCustomer(Customers customer)
        {
            customersTableAdapter = new DataSetHotelTableAdapters.customersTableAdapter();
            customersTableAdapter.Fill(dataSetHotel.customers);
            DataSetHotel.customersRow customersRow = dataSetHotel.customers.FindBye_mail(customer.EMail);
            try
            {
                customersRow.phone_no = customer.PhoneNo;
                customersRow.phone_country_code = customer.PhoneCountryCode;
                customersRow.credit_card_no = customer.CreditCardNo;
                customersRow.first_name = customer.FirstName;
                customersRow.last_name = customer.LastName;
                customersTableAdapter.Update(customersRow);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public DataView GetCustomers()
        {
            try
            {
                customersTableAdapter = new DataSetHotelTableAdapters.customersTableAdapter();
                customersTableAdapter.Fill(dataSetHotel.customers);
                DataView customerDataView = new DataView(dataSetHotel.Tables["Customer"]);
                return customerDataView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public DataSetHotel.customersRow GetCustomerByEmail(string email)
        {
            try
            {
                customersTableAdapter = new DataSetHotelTableAdapters.customersTableAdapter();
                customersTableAdapter.Fill(dataSetHotel.customers);
                DataSetHotel.customersRow customerRow = dataSetHotel.customers.FindBye_mail(email);
                return customerRow;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
