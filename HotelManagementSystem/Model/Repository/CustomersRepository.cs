using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HotelManagementSystem.Model.Database;

namespace HotelManagementSystem
{
    public class CustomersRepository
    {
        private HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.customersTableAdapter customersTableAdapter;
        private DataSetHotel dataSetHotel;

        public CustomersRepository()
        {
            dataSetHotel = new DataSetHotel();
        }

        public void AddCustomer(Customer customer)
        {
            customersTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.customersTableAdapter();
            customersTableAdapter.Insert(customer.EMail, customer.PhoneNo, customer.PhoneCountryCode, customer.CreditCardNo, customer.FirstName, customer.LastName);
        }

        public void DeleteCustomer(Customer customer)
        {
            customersTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.customersTableAdapter();
            customersTableAdapter.Delete(customer.EMail, customer.PhoneNo, customer.PhoneCountryCode, customer.CreditCardNo, customer.FirstName, customer.LastName);
        }

        public void UpdateCustomer(Customer customer)
        {
            customersTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.customersTableAdapter();
            customersTableAdapter.Fill(dataSetHotel.customers);
            DataSetHotel.customersRow customersRow = dataSetHotel.customers.FindBye_mail(customer.EMail);
            customersRow.phone_no = customer.PhoneNo;
            customersRow.phone_country_code = customer.PhoneCountryCode;
            customersRow.credit_card_no = customer.CreditCardNo;
            customersRow.first_name = customer.FirstName;
            customersRow.last_name = customer.LastName;
            customersTableAdapter.Update(customersRow);
        }

        public DataView GetCustomers()
        {
            customersTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.customersTableAdapter();
            customersTableAdapter.Fill(dataSetHotel.customers);
            DataView customerDataView = new DataView(dataSetHotel.Tables["Customers"]);
            return customerDataView;
        }

        public DataSetHotel.customersRow GetCustomerByEmail(string email)
        {
            customersTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.customersTableAdapter();
            customersTableAdapter.Fill(dataSetHotel.customers);
            DataSetHotel.customersRow customerRow = dataSetHotel.customers.FindBye_mail(email);
            return customerRow;
        }

        public DataView FindCustomersByEmail(string email)
        {
            customersTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.customersTableAdapter();
            customersTableAdapter.FillByCustomerEmail(dataSetHotel.customers, email);
            DataView customerDataView = new DataView(dataSetHotel.Tables["Customers"]);
            return customerDataView;
        }

        public DataView FindCustomersByName(string firstName, string lastName)
        {
            customersTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.customersTableAdapter();
            customersTableAdapter.FillByCustomerName(dataSetHotel.customers, firstName, lastName, firstName, lastName);
            DataView customerDataView = new DataView(dataSetHotel.Tables["Customers"]);
            return customerDataView;
        }
    }
}
