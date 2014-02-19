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
        }//!!!Doesn't work!!!

        private void CheckAvailabilityButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (AvailabilityFromDateDpr.SelectedDate.Value >= AvailabilityToDateDpr.SelectedDate.Value)
            {
                MessageBox.Show("Please select a check in date before the checkout date");
            }
            else
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
                var customer = reservationController.GetCustomer(CustomerDetailsEmailTbx.Text);
                CustomerDetailsEmailTbx.Text = customer.EMail;
                CustomerDetailsFirstNameTbx.Text = customer.FirstName;
                CustomerDetailsLastnameTbx.Text = customer.LastName;
                CustomerDetailsCreditCardNoTbx.Text = customer.CreditCardNo;
                CustomerDetailsPhoneCountryCodeTbx.Text = customer.PhoneCountryCode;
                CustomerDetailsPhoneNoTbx.Text = customer.PhoneNo;
            }
            catch (Exception)
            {
                MessageBox.Show("Can't find customer. Try search with another e-mail!");
            }
        }

        private void CustomerDetailsBookBtn_Click(object sender, RoutedEventArgs e)
        {
            if (registerEnabled)
            {
                try
                {
                    if (Validate.ValidateRegisterCustomer(CustomerDetailsEmailTbx.Text, CustomerDetailsPhoneNoTbx.Text, CustomerDetailsPhoneCountryCodeTbx.Text,
                   CustomerDetailsCreditCardNoTbx.Text, CustomerDetailsFirstNameTbx.Text, CustomerDetailsLastnameTbx.Text))
                    {
                        reservationController.AddCustomer(CustomerDetailsEmailTbx.Text, CustomerDetailsPhoneNoTbx.Text, CustomerDetailsPhoneCountryCodeTbx.Text,
                            CustomerDetailsCreditCardNoTbx.Text, CustomerDetailsFirstNameTbx.Text, CustomerDetailsLastnameTbx.Text);

                        reservationController.AddReservation(CustomerDetailsEmailTbx.Text, selectedRoom[0].ToString(),
                            AvailabilityFromDateDpr.SelectedDate.Value, AvailabilityToDateDpr.SelectedDate.Value, false, false);
                        ShowCustomerRecipt(reservationController.GetLatestReservationNo().ToString());

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
                    reservationController.AddReservation(CustomerDetailsEmailTbx.Text, selectedRoom[0].ToString(),
                        AvailabilityFromDateDpr.SelectedDate.Value, AvailabilityToDateDpr.SelectedDate.Value, false, false);
                    ShowCustomerRecipt(reservationController.GetLatestReservationNo().ToString());
                    
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
            var reservation = reservationController.GetSingleReservation(reservationNo);
            RecieptEmailTbx.Text = reservation.EMail;
            RecieptReservationNumberTbx.Text = reservation.ReservationNo;
            RecieptRoomNumberTbx.Text = reservation.RoomNo;
            RecieptCheckInDateTbx.Text = reservation.CheckInDate.ToString();
            RecieptCheckOutDateTbx.Text = reservation.CheckOutDate.ToString();

            string customerEmail = reservation.EMail;
            var customer = reservationController.GetCustomer(customerEmail);
            RecieptFirstNameTbx.Text = customer.FirstName;
            RecieptLastNameTbx.Text = customer.LastName;
            RecieptCreditCardNoTbx.Text = customer.CreditCardNo;
            RecieptPhoneNoTbx.Text = customer.PhoneNo;
            RecieptPhoneCountryCodeTbx.Text = customer.PhoneCountryCode;

            string roomNo = reservationController.GetSingleReservation(reservationNo).RoomNo;
            var room = reservationController.GetRoom(roomNo);
            RecieptRoomTypeTbx.Text = room.RoomType;
            RecieptPricePerDayTbx.Text = room.PricePerDay.ToString();
            RecieptNightsTbx.Text = new OrderUtility().TotalNights(reservationNo).ToString();
            RecieptTotalPriceTbx.Text = new OrderUtility().TotalOrderValue(room.PricePerDay, reservationNo);
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
        }//!!!Show empty reservation if search with a reservation number that doesn't exists!!!

        private void searchCheckInByEmailButton_Click(object sender, RoutedEventArgs e)
        {
            CheckInCheckOutDataGrid.ItemsSource = checkInCheckOutController.FindReservationByEmail(CheckInCheckOutSearchTbx.Text);
        }//!!!Show empty reservation if search with a email that doesn't exists!!!

        //Customer Registry
        private void showReservationsCustomerRegistryButton_Click(object sender, RoutedEventArgs e)
        {
            
            selectedCustomer = CustomerRegistryDataGrid.SelectedItem as DataRowView;
            string email = selectedCustomer[0].ToString();
            ReservationRegistryDataGrid.ItemsSource = customerRegistryController.FindReservationByEmail(email);
            RegistryTab.SelectedIndex = 1;
        }//!!!Should maybe only show this button if a customer is selected??!!!

        private void showCustomerDetailsCustomerRegistryButton_Click(object sender, RoutedEventArgs e)
        {
            selectedCustomer = CustomerRegistryDataGrid.SelectedItem as DataRowView;
        }//!!!Doesn't work!!!

        private void CustomerRegistrySearchEmailBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomerRegistryDataGrid.ItemsSource = customerRegistryController.FindCustomerByEmail(CustomerRegistryEmailTbx.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Felmeddelande här!");
            }
        }//!!!Shows empty line even if search with an non existing email!!!

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

        private void CustomerRegistryNameSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerRegistryDataGrid.ItemsSource = new CustomerRegistryController().FindCustomersByName(CustomerRegistryFirstNameTbx.Text, CustomerRegistryLastNameTbx.Text);
        }

        //Room Registry
        //Empty

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }///Empty

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ReservationRegistryShowReservationsBtn_Click(object sender, RoutedEventArgs e)
        {
            RegistryTab.SelectedIndex = 0;
        }

        private void ReservationRegistrySearchEmailBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            ReservationRegistryDataGrid.ItemsSource = customerRegistryController.FindCustomerByEmail(ReservationRegistryEmailTbx.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Felmeddelande här!");
            }// shows empty line if nothing clicked
        
            
        }

        private void ReservationRegistryNameSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            ReservationRegistryDataGrid.ItemsSource = new CustomerRegistryController().FindCustomersByName(ReservationRegistryFirstNameTbx.Text, ReservationRegistryLastNameTbx.Text);
        }

        private void ReservationRegistryShowAllReservationsBtn_Click(object sender, RoutedEventArgs e)
        {
          //  ReservationRegistryDataGrid.ItemsSource = new CustomerRegistryController().GetReservationDataView();
        }///shooows empty line

    }
}
