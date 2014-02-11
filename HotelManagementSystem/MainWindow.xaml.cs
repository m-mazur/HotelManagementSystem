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
            try
            {
                reservationController = new ReservationController();
                checkInCheckOutController = new CheckInCheckOutController();
                customerRegistryController = new CustomerRegistryController();
            }
            catch (Exception)
            {
                //Felmeddelande "Can't connect to database"
                Close();
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BookingTab.SelectedIndex = 1;
            selectedRoom = RoomInfoGrid.SelectedItem as DataRowView;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(EmailTextbox.Text);
            try
            {
                EmailTextbox.Text = reservationController.GetCustomer(EmailTextbox.Text).EMail;
                FirstNameTextBox.Text = reservationController.GetCustomer(EmailTextbox.Text).FirstName;
                LastnameTextbox.Text = reservationController.GetCustomer(EmailTextbox.Text).LastName;
                CreditCardNoTextBox.Text = reservationController.GetCustomer(EmailTextbox.Text).CreditCardNo;
                PhoneCountryCodeTextBox.Text = reservationController.GetCustomer(EmailTextbox.Text).PhoneCountryCode;
                PhoneNoTextBox.Text = reservationController.GetCustomer(EmailTextbox.Text).PhoneNo;
            }
            catch (Exception)
            {
                //Felmeddelande
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BookingTab.SelectedIndex = 0;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            BookingTab.SelectedIndex = 2;
            random = new Random();
            int randomNo = random.Next(000000, 999999);

            if (registerEnabled)
            {
                try
                {
                    reservationController.AddCustomer(EmailTextbox.Text, PhoneNoTextBox.Text, PhoneCountryCodeTextBox.Text,
                        CreditCardNoTextBox.Text, FirstNameTextBox.Text, LastnameTextbox.Text);
                    Console.WriteLine("Kan registrer kund");
                }
                catch (Exception)
                {
                    //Felmeddelande
                }

                try 
                {
                reservationController.AddReservation(randomNo.ToString(), EmailTextbox.Text, selectedRoom[0].ToString(),
                    FromDateDatepicker.SelectedDate.Value, ToDateDatepicker.SelectedDate.Value, false, false);
                Console.WriteLine("ur code works u are great");
                }
                catch (Exception)
                {
                    //Felmeddelande
                }
            }
            else
            {
                try
                {
                reservationController.AddReservation(randomNo.ToString(), EmailTextbox.Text, selectedRoom[0].ToString(), 
                    FromDateDatepicker.SelectedDate.Value, ToDateDatepicker.SelectedDate.Value, false, false);
                Console.WriteLine("Kan registrera bookning");
                }
                catch (Exception)
                {
                    //Felmeddelande
                }
            }
            ShowCustomerRecipt(randomNo.ToString());
        }

                

   

        private void RoomTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void CheckAvailabilityButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RoomInfoGrid.ItemsSource = reservationController.GetAvailableRooms("single room", FromDateDatepicker.SelectedDate.Value, ToDateDatepicker.SelectedDate.Value);
                Console.WriteLine(FromDateDatepicker.SelectedDate.Value.ToString());
            }
            catch (Exception)
            {
                //Felmeddelande
            }
        }

        private void checkBoxRegister_Checked(object sender, RoutedEventArgs e)
        {
            SearchCustomer.IsEnabled = false;

            EmailTextbox.Text = "";
            FirstNameTextBox.Text = "";
            LastnameTextbox.Text = "";
            CreditCardNoTextBox.Text = "";
            PhoneCountryCodeTextBox.Text = "";
            PhoneNoTextBox.Text = "";

            EnableDisabletextBoxes(true);
            registerEnabled = true;
        }

        private void checkBoxRegister_Unchecked(object sender, RoutedEventArgs e)
        {
            SearchCustomer.IsEnabled = true;
            EnableDisabletextBoxes(false);
            registerEnabled = false;
        }

        private void EnableDisabletextBoxes(bool enabled)
        {
            FirstNameTextBox.IsEnabled = enabled;
            LastnameTextbox.IsEnabled = enabled;
            CreditCardNoTextBox.IsEnabled = enabled;
            PhoneCountryCodeTextBox.IsEnabled = enabled;
            PhoneNoTextBox.IsEnabled = enabled;
        }

        private void searchCheckInByReservationNoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            checkInDataGrid.ItemsSource = checkInCheckOutController.FindReservationByReservationNo(searchCheckInTextBox.Text);
            }
            catch (Exception)
            {
                //Felmeddelande
            }
        }

        private void checkInCheckInButton_Click(object sender, RoutedEventArgs e)
        {
            selectedReservation = checkInDataGrid.SelectedItem as DataRowView;
            string reservationNo = selectedReservation[0].ToString();
            try
            {
            checkInCheckOutController.CheckInReservation(reservationNo, true);
            checkInDataGrid.ItemsSource = checkInCheckOutController.FindReservationByReservationNo(reservationNo);
            }
            catch (Exception)
            {
                //Felmeddelande
            }
        }

        private void checkOutCheckInButton_Click(object sender, RoutedEventArgs e)
        {
            selectedReservation = checkInDataGrid.SelectedItem as DataRowView;
            string reservationNo = selectedReservation[0].ToString();
            try 
            {
            checkInCheckOutController.CheckOutReservation(reservationNo, true);
            checkInDataGrid.ItemsSource = checkInCheckOutController.FindReservationByReservationNo(reservationNo);
            }
            catch (Exception)
            {
                //Felmeddelande
            }
        }

        private void searchCheckInByEmailButton_Click(object sender, RoutedEventArgs e)
        {
            checkInDataGrid.ItemsSource = checkInCheckOutController.FindReservationByEmail(searchCheckInTextBox.Text);
        }

        private void showAllCustomersCustomerRegistryButton_Click(object sender, RoutedEventArgs e)
        {
            customerRegistryDataGrid.ItemsSource = new CustomerRegistryController().GetCustomerDataView();
        }

        private void ShowCustomerRecipt(string reservationNo)
        {
            emailRecieptTextBox.Text = reservationController.GetSingleReservation(reservationNo).EMail;
            reservationNumberRecieptTextBox.Text = reservationController.GetSingleReservation(reservationNo).ReservationNo;
            roomNumberRecieptTextBox.Text = reservationController.GetSingleReservation(reservationNo).RoomNo;
            checkInDateRecieptTextBox.Text = reservationController.GetSingleReservation(reservationNo).CheckInDate.ToString();
            checkOutDateRecieptTextBox.Text = reservationController.GetSingleReservation(reservationNo).CheckOutDate.ToString();

            string customerEmail = reservationController.GetSingleReservation(reservationNo).EMail;
            firstNameRecieptTextBox.Text = reservationController.GetCustomer(customerEmail).FirstName;
            lastNameRecieptTextBox.Text = reservationController.GetCustomer(customerEmail).LastName;
            creditCardNoRecieptTextBox.Text = reservationController.GetCustomer(customerEmail).CreditCardNo;
            PhoneNoRecieptTextBox.Text = reservationController.GetCustomer(customerEmail).PhoneNo;
            PhoneCountryCodeRecieptTextBox.Text = reservationController.GetCustomer(customerEmail).PhoneCountryCode;

            string roomNo = reservationController.GetSingleReservation(reservationNo).RoomNo;
            roomTypeRecieptTextBox.Text = reservationController.GetRoom(roomNo).RoomType;
            pricePerDayRecieptTextBox.Text = reservationController.GetRoom(roomNo).PricePerDay.ToString();
            nightsRecieptTextBox.Text = new OrderUtility().TotalNights(reservationNo).ToString();
            totalPriceRecieptTextBox.Text = new OrderUtility().TotalOrderValue(reservationController.GetRoom(roomNo).PricePerDay, reservationNo);
        }

        private void showReservationsCustomerRegistryButton_Click(object sender, RoutedEventArgs e)
        {

            selectedCustomer = customerRegistryDataGrid.SelectedItem as DataRowView;
            string email = selectedCustomer[0].ToString();
            customerRegistryDataGrid.ItemsSource = customerRegistryController.FindReservationByEmail(email);
        }

        private void showCustomerDetailsCustomerRegistryButton_Click(object sender, RoutedEventArgs e)
        {
            selectedCustomer = customerRegistryDataGrid.SelectedItem as DataRowView;
        }

        private void DoneReciept_Click(object sender, RoutedEventArgs e)
        {
            BookingTab.SelectedIndex = 0;
        }

        private void searchCustomerRegistryButtton_Click(object sender, RoutedEventArgs e)
        {
            customerRegistryDataGrid.ItemsSource = customerRegistryController.FindReservationByEmail(emailCustomerRegistryTextBox.Text);
        }
    }
}
