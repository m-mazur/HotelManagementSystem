using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace HotelManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ReservationController reservationController;
        private CheckInCheckOutController checkInCheckOutController;
        private CustomerRegistryController customerRegistryController;
        private DataRowView selectedRoom;
        private DataRowView selectedReservation;
        private DataRowView selectedCustomer;
        private Random random;
        private Boolean registerEnabled;

        public MainWindow()
        {
            InitializeComponent();
            reservationController = new ReservationController();
            checkInCheckOutController = new CheckInCheckOutController();
            customerRegistryController = new CustomerRegistryController();
        }

        //New Reservation Tab / Availibility
        private void RoomTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //!!!Doesn't work!!!
        }

        private void CheckAvailabilityButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AvailabilityDataGrid.ItemsSource = reservationController.GetAvailableRooms("single room", AvailabilityFromDateDpr.SelectedDate.Value, AvailabilityToDateDpr.SelectedDate.Value);
            }
            catch (Exception)
            {
                MessageBox.Show("You need to choose a room, check in date and check out date first!");
            }
        }

        private void AvailabilityNextBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedRoom = AvailabilityDataGrid.SelectedItem as DataRowView;
            if (selectedRoom == null)
            {
                MessageBox.Show("You need to choose a room first!");
            }
            else
            {
                BookingTab.SelectedIndex = 1;
            }
        }

        //New Reservation Tab / Customer Details
        private void CustomerDetailsSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomerDetailsEmailTbx.Text = reservationController.GetCustomer(CustomerDetailsEmailTbx.Text).EMail;
                CustomerDetailsFirstNameTbx.Text = reservationController.GetCustomer(CustomerDetailsEmailTbx.Text).FirstName;
                CustomerDetailsLastnameTbx.Text = reservationController.GetCustomer(CustomerDetailsEmailTbx.Text).LastName;
                CustomerDetailsCreditCardNoTbx.Text = reservationController.GetCustomer(CustomerDetailsEmailTbx.Text).CreditCardNo;
                CustomerDetailsPhoneCountryCodeTbx.Text = reservationController.GetCustomer(CustomerDetailsEmailTbx.Text).PhoneCountryCode;
                CustomerDetailsPhoneNoTbx.Text = reservationController.GetCustomer(CustomerDetailsEmailTbx.Text).PhoneNo;
            }
            catch (Exception)
            {
                MessageBox.Show("Can't find customer. Try search with another e-mail!");
            }
        }

        private void CustomerDetailsBookBtn_Click(object sender, RoutedEventArgs e)
        {
            random = new Random();
            int randomNo = random.Next(000000, 999999);

            if (registerEnabled)
            {
                try
                {
                    if (Validate.ValidateRegisterCustomer(CustomerDetailsEmailTbx.Text, CustomerDetailsPhoneNoTbx.Text, CustomerDetailsPhoneCountryCodeTbx.Text,
                   CustomerDetailsCreditCardNoTbx.Text, CustomerDetailsFirstNameTbx.Text, CustomerDetailsLastnameTbx.Text))
                    {
                        reservationController.AddCustomer(CustomerDetailsEmailTbx.Text, CustomerDetailsPhoneNoTbx.Text, CustomerDetailsPhoneCountryCodeTbx.Text,
                            CustomerDetailsCreditCardNoTbx.Text, CustomerDetailsFirstNameTbx.Text, CustomerDetailsLastnameTbx.Text);

                        reservationController.AddReservation(randomNo.ToString(), CustomerDetailsEmailTbx.Text, selectedRoom[0].ToString(),
                            AvailabilityFromDateDpr.SelectedDate.Value, AvailabilityToDateDpr.SelectedDate.Value, false, false);
                        ShowCustomerRecipt(randomNo.ToString());

                        BookingTab.SelectedIndex = 2;
                    }
                    else
                    {
                        MessageBox.Show("Fill in all textboxes!");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("You need to create or search a customer first!");
                }
            }
            else
            {
                try
                {
                    reservationController.AddReservation(randomNo.ToString(), CustomerDetailsEmailTbx.Text, selectedRoom[0].ToString(),
                        AvailabilityFromDateDpr.SelectedDate.Value, AvailabilityToDateDpr.SelectedDate.Value, false, false);
                    ShowCustomerRecipt(randomNo.ToString());
                    BookingTab.SelectedIndex = 2;
                }
                catch (Exception)
                {
                    MessageBox.Show("You need to create or search a customer first!");
                }
            }
        }

        private void CustomerDetailsBackBtn_Click(object sender, RoutedEventArgs e)
        {
            BookingTab.SelectedIndex = 0;
        }

        private void EnableDisabletextBoxes(bool enabled)
        {
            CustomerDetailsFirstNameTbx.IsEnabled = enabled;
            CustomerDetailsLastnameTbx.IsEnabled = enabled;
            CustomerDetailsCreditCardNoTbx.IsEnabled = enabled;
            CustomerDetailsPhoneCountryCodeTbx.IsEnabled = enabled;
            CustomerDetailsPhoneNoTbx.IsEnabled = enabled;
        }

        //New Reservation Tab / Receipt
        private void RecieptDoneBtn_Click(object sender, RoutedEventArgs e)
        {
            BookingTab.SelectedIndex = 0;
        }

        private void ShowCustomerRecipt(string reservationNo)
        {
            RecieptEmailTbx.Text = reservationController.GetSingleReservation(reservationNo).EMail;
            RecieptReservationNumberTbx.Text = reservationController.GetSingleReservation(reservationNo).ReservationNo;
            RecieptRoomNumberTbx.Text = reservationController.GetSingleReservation(reservationNo).RoomNo;
            RecieptCheckInDateTbx.Text = reservationController.GetSingleReservation(reservationNo).CheckInDate.ToString();
            RecieptCheckOutDateTbx.Text = reservationController.GetSingleReservation(reservationNo).CheckOutDate.ToString();

            string customerEmail = reservationController.GetSingleReservation(reservationNo).EMail;
            RecieptFirstNameTbx.Text = reservationController.GetCustomer(customerEmail).FirstName;
            RecieptLastNameTbx.Text = reservationController.GetCustomer(customerEmail).LastName;
            RecieptCreditCardNoTbx.Text = reservationController.GetCustomer(customerEmail).CreditCardNo;
            RecieptPhoneNoTbx.Text = reservationController.GetCustomer(customerEmail).PhoneNo;
            RecieptPhoneCountryCodeTbx.Text = reservationController.GetCustomer(customerEmail).PhoneCountryCode;

            string roomNo = reservationController.GetSingleReservation(reservationNo).RoomNo;
            RecieptRoomTypeTbx.Text = reservationController.GetRoom(roomNo).RoomType;
            RecieptPricePerDayTbx.Text = reservationController.GetRoom(roomNo).PricePerDay.ToString();
            RecieptNightsTbx.Text = new OrderUtility().TotalNights(reservationNo).ToString();
            RecieptTotalPriceTbx.Text = new OrderUtility().TotalOrderValue(reservationController.GetRoom(roomNo).PricePerDay, reservationNo);
        }

        //Check In/Check out Tab
        private void checkInCheckInButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedReservation = CheckInCheckOutDataGrid.SelectedItem as DataRowView;
                string reservationNo = selectedReservation[0].ToString();
                checkInCheckOutController.CheckInReservation(reservationNo, true);
                CheckInCheckOutDataGrid.ItemsSource = checkInCheckOutController.FindReservationByReservationNo(reservationNo);
            }
            catch (Exception)
            {
                MessageBox.Show("You need to search and choose a reservation first!");
            }
        }

        private void checkOutCheckInButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedReservation = CheckInCheckOutDataGrid.SelectedItem as DataRowView;
                string reservationNo = selectedReservation[0].ToString();
                checkInCheckOutController.CheckOutReservation(reservationNo, true);
                CheckInCheckOutDataGrid.ItemsSource = checkInCheckOutController.FindReservationByReservationNo(reservationNo);
            }
            catch (Exception)
            {
                MessageBox.Show("You need to search and choose a reservation first!");
            }
        }

        private void searchCheckInByReservationNoButton_Click(object sender, RoutedEventArgs e)
        {
            CheckInCheckOutDataGrid.ItemsSource = checkInCheckOutController.FindReservationByReservationNo(CheckInCheckOutSearchTbx.Text);
            //!!!Show empty reservation if search with a reservation number that doesn't exists!!!
        }

        private void searchCheckInByEmailButton_Click(object sender, RoutedEventArgs e)
        {
            CheckInCheckOutDataGrid.ItemsSource = checkInCheckOutController.FindReservationByEmail(CheckInCheckOutSearchTbx.Text);
            //!!!Show empty reservation if search with a email that doesn't exists!!!
        }

        private void checkBoxRegister_Checked(object sender, RoutedEventArgs e)
        {
            CustomerDetailsSearchBtn.IsEnabled = false;

            CustomerDetailsEmailTbx.Text = "";
            CustomerDetailsFirstNameTbx.Text = "";
            CustomerDetailsLastnameTbx.Text = "";
            CustomerDetailsCreditCardNoTbx.Text = "";
            CustomerDetailsPhoneCountryCodeTbx.Text = "";
            CustomerDetailsPhoneNoTbx.Text = "";

            EnableDisabletextBoxes(true);
            registerEnabled = true;
        }

        private void checkBoxRegister_Unchecked(object sender, RoutedEventArgs e)
        {
            CustomerDetailsSearchBtn.IsEnabled = true;
            EnableDisabletextBoxes(false);
            registerEnabled = false;
        }

        //Customer Registry
        private void showReservationsCustomerRegistryButton_Click(object sender, RoutedEventArgs e)
        {
            //!!!Should maybe only show this button if a customer is selected??!!!
            selectedCustomer = CustomerRegistryDataGrid.SelectedItem as DataRowView;
            string email = selectedCustomer[0].ToString();
            CustomerRegistryDataGrid.ItemsSource = customerRegistryController.FindReservationByEmail(email);
        }

        private void showCustomerDetailsCustomerRegistryButton_Click(object sender, RoutedEventArgs e)
        {
            //!!!Doesn't work!!!
            selectedCustomer = CustomerRegistryDataGrid.SelectedItem as DataRowView;
        }

        private void searchCustomerRegistryButtton_Click(object sender, RoutedEventArgs e)
        {
            //!!!Shows empty line even if search with an non existing email!!!
            try
            {
                CustomerRegistryDataGrid.ItemsSource = customerRegistryController.FindReservationByEmail(CustomerRegistryEmailTbx.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Felmeddelande här!");
            }
        }

        private void showAllCustomersCustomerRegistryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomerRegistryDataGrid.ItemsSource = new CustomerRegistryController().GetCustomerDataView();
            }
            catch (Exception)
            {
                MessageBox.Show("There is a problem with the database. If problem occurs after restart, contact admin!");
            }
        }

        //Room Registry
        //Empty

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

    }
}
