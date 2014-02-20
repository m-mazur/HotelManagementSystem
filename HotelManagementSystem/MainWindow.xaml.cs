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
            CenterWindowOnScreen();
            reservationController = new ReservationController();
            checkInCheckOutController = new CheckInCheckOutController();
            customerRegistryController = new CustomerRegistryController();
        }

        //Center MainWindow
        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        //New Reservation Tab

        //New Reservation Tab / Availibility
        private void CheckAvailabilityBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AvailabilityFromDateDpr.SelectedDate.Value >= AvailabilityToDateDpr.SelectedDate.Value)
                {
                    MessageBox.Show("Please select a check in date before the checkout date");
                }
                else if (AvailabilityFromDateDpr.SelectedDate < DateTime.Today)
                {
                    MessageBox.Show("Please select a date after todays date");
                }
                else
                {
                    AvailabilityDataGrid.ItemsSource = reservationController.GetAvailableRooms("single room", AvailabilityFromDateDpr.SelectedDate.Value, AvailabilityToDateDpr.SelectedDate.Value);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("You need to choose a room, check in date and check out date first!");
            }
        }

        private void GoToCustomerDetailsBtn_Click(object sender, RoutedEventArgs e)
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
        private void SearchBtn_Click(object sender, RoutedEventArgs e)
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

        private void GoToReceiptBtn_Click(object sender, RoutedEventArgs e)
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

        private void BackToAvailibiltyBtn_Click(object sender, RoutedEventArgs e)
        {
            BookingTab.SelectedIndex = 0;
        }

        private void CheckBoxRegister_Checked(object sender, RoutedEventArgs e)
        {
            CustomerDetailsSearchBtn.IsEnabled = false;
            ClearCustomerDetails();
            EnableDisabletextBoxes(true);
            registerEnabled = true;
        }

        private void ClearCustomerDetails()
        {
            CustomerDetailsEmailTbx.Text = "";
            CustomerDetailsFirstNameTbx.Text = "";
            CustomerDetailsLastnameTbx.Text = "";
            CustomerDetailsCreditCardNoTbx.Text = "";
            CustomerDetailsPhoneCountryCodeTbx.Text = "";
            CustomerDetailsPhoneNoTbx.Text = "";
        }

        private void CheckBoxRegister_Unchecked(object sender, RoutedEventArgs e)
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
            ClearCustomerDetails();
            AvailabilityDataGrid.ItemsSource = null;
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

        //Check In/Check Out Tab
        private void CheckInBtn_Click(object sender, RoutedEventArgs e)
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

        private void CheckOutBtn_Click(object sender, RoutedEventArgs e)
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

        private void SearchByReservationNoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CheckInCheckOutSearchTbx.Text))
            {
                MessageBox.Show("You need to fill in a reservation number first!");
            }
            else
            {
                try
                {
                    CheckInCheckOutDataGrid.ItemsSource = checkInCheckOutController.FindActiveReservationByReservationNo(CheckInCheckOutSearchTbx.Text);
                    if (CheckInCheckOutDataGrid.Items.Count == 0)
                    {
                        MessageBox.Show("No reservation with that number exsists!");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Reservation number has to be digits only!");
                }
            }
        }

        private void SearchByEmailBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CheckInCheckOutSearchTbx.Text))
            {
                MessageBox.Show("You need to fill in an E-mail first!");
            }
            else
            {
                try
                {
                    CheckInCheckOutDataGrid.ItemsSource = checkInCheckOutController.FindActiveReservationByEmail(CheckInCheckOutSearchTbx.Text);
                    if (CheckInCheckOutDataGrid.Items.Count == 0)
                    {
                        MessageBox.Show("No reservation with that e-mail!");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("There is a problem with the database. If problem occurs after restart, contact admin!");
                }
            }
        }

        //Registry Tab
        
        //Registry Tab / Customer Registry Tab
        private void ShowSelectedCustomerReservationsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedCustomer = CustomerRegistryDataGrid.SelectedItem as DataRowView;
                string email = selectedCustomer[0].ToString();
                ReservationRegistryDataGrid.ItemsSource = customerRegistryController.FindReservationByEmail(email);
                RegistryTab.SelectedIndex = 1;
                if (ReservationRegistryDataGrid.Items.Count == 0)
                {
                    MessageBox.Show("Customer has no reservations!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("You need to choose a customer first!");
            }
        }

        private void SearchCustomerByEmailBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CustomerRegistryEmailTbx.Text))
            {
                MessageBox.Show("You need to fill in an e-mail first!");
            }
            else
            {
                try
                {
                    CustomerRegistryDataGrid.ItemsSource = customerRegistryController.FindCustomerByEmail(CustomerRegistryEmailTbx.Text);
                    if (CustomerRegistryDataGrid.Items.Count == 0)
                    {
                        MessageBox.Show("No customer with that e-mail exsists!");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("There is a problem with the database. If problem occurs after restart, contact admin!");
                }
            }
        }

        private void ShowAllCustomersBtn_Click(object sender, RoutedEventArgs e)
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

        private void SearchCustomersByNameBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CustomerRegistryFirstNameTbx.Text) && string.IsNullOrWhiteSpace(CustomerRegistryLastNameTbx.Text))
            {
                MessageBox.Show("You need to fill in a first name or last name first!");
            }
            else
            {
                try
                {
                    CustomerRegistryDataGrid.ItemsSource = new CustomerRegistryController().FindCustomersByName(CustomerRegistryFirstNameTbx.Text, CustomerRegistryLastNameTbx.Text);
                    if (CustomerRegistryDataGrid.Items.Count == 0)
                    {
                        MessageBox.Show("No customer with that name exsists!");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("There is a problem with the database. If problem occurs after restart, contact admin!");
                }
            }
        }
        //Registry Tab / Reservations Registry Tab

        private void ShowSelectedReservationCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedReservation = ReservationRegistryDataGrid.SelectedItem as DataRowView;
                string email = selectedReservation[1].ToString();
                CustomerRegistryDataGrid.ItemsSource = customerRegistryController.FindCustomerByEmail(email);
                RegistryTab.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("You need to choose a reservation first!");
            }
        }

        private void SearchReservationsByEmailBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ReservationRegistryEmailTbx.Text))
            {
                MessageBox.Show("You need to fill in an e-mail first!");
            }
            else
            {
                try
                {
                    ReservationRegistryDataGrid.ItemsSource = customerRegistryController.FindReservationByEmail(ReservationRegistryEmailTbx.Text);
                    if (ReservationRegistryDataGrid.Items.Count == 0)
                    {
                        MessageBox.Show("No reservation with that e-mail exsists!");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("There is a problem with the database. If problem occurs after restart, contact admin!");
                }
            }
        }

        private void SearchReservationByReservationNoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ReservationRegistryReservationNoTbx.Text))
            {
                MessageBox.Show("You need to fill in a reservation number first!");
            }
            else
            {
                try
                {
                    ReservationRegistryDataGrid.ItemsSource = customerRegistryController.FindReservationByNo(ReservationRegistryReservationNoTbx.Text);
                    if (ReservationRegistryDataGrid.Items.Count == 0)
                    {
                        MessageBox.Show("No reservation with that name exsists!");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("There is a problem with the database. If problem occurs after restart, contact admin!");
                }
            }
        }

        private void ShowAllReservationsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ReservationRegistryDataGrid.ItemsSource = new CustomerRegistryController().GetReservationDataView();
            }
            catch (Exception)
            {
                MessageBox.Show("There is a problem with the database. If problem occurs after restart, contact admin!");
            }
        }///Fixa getAllResvervations
    }
}
