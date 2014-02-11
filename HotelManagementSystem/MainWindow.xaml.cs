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

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BookingTab.SelectedIndex = 1;
            selectedRoom = AvailabilityRoominfoDataGrid.SelectedItem as DataRowView;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(CustomerDetailsEmailTbx.Text);
            CustomerDetailsEmailTbx.Text = reservationController.GetCustomer(CustomerDetailsEmailTbx.Text).EMail;
            CustomerDetailsFirstNameTbx.Text = reservationController.GetCustomer(CustomerDetailsEmailTbx.Text).FirstName;
            CustomerDetailsLastnameTbx.Text = reservationController.GetCustomer(CustomerDetailsEmailTbx.Text).LastName;
            CustomerDetailsCreditCardNoTbx.Text = reservationController.GetCustomer(CustomerDetailsEmailTbx.Text).CreditCardNo;
            CustomerDetailsPhoneCountryCodeTbx.Text = reservationController.GetCustomer(CustomerDetailsEmailTbx.Text).PhoneCountryCode;
            CustomerDetailsPhoneNoTbx.Text = reservationController.GetCustomer(CustomerDetailsEmailTbx.Text).PhoneNo;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BookingTab.SelectedIndex = 0;
        }

        private void CustomerDetailsBookBtn_Click(object sender, RoutedEventArgs e)
        {
            BookingTab.SelectedIndex = 2;
            random = new Random();
            int randomNo = random.Next(000000, 999999);

            if (registerEnabled)
            {
                reservationController.AddCustomer(CustomerDetailsEmailTbx.Text, CustomerDetailsPhoneNoTbx.Text, CustomerDetailsPhoneCountryCodeTbx.Text,
                   CustomerDetailsCreditCardNoTbx.Text, CustomerDetailsFirstNameTbx.Text, CustomerDetailsLastnameTbx.Text);
                Console.WriteLine("Kan registrer kund");

                reservationController.AddReservation(randomNo.ToString(), CustomerDetailsEmailTbx.Text, selectedRoom[0].ToString(),
                    FromDateDatepicker.SelectedDate.Value, ToDateDatepicker.SelectedDate.Value, false, false);
                Console.WriteLine("ur code works u are great"); 
            }
            else
            {
                reservationController.AddReservation(randomNo.ToString(), CustomerDetailsEmailTbx.Text, selectedRoom[0].ToString(), 
                    FromDateDatepicker.SelectedDate.Value, ToDateDatepicker.SelectedDate.Value, false, false);
                Console.WriteLine("Kan registrera bookning");
            }

            ShowCustomerRecipt(randomNo.ToString());
        }

                

   

        private void RoomTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckAvailabilityButton_Click(object sender, RoutedEventArgs e)
        {
            AvailabilityRoominfoDataGrid.ItemsSource = reservationController.GetAvailableRooms("single room", FromDateDatepicker.SelectedDate.Value, ToDateDatepicker.SelectedDate.Value);
            Console.WriteLine(FromDateDatepicker.SelectedDate.Value.ToString());
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

        private void searchCheckInByReservationNoButton_Click(object sender, RoutedEventArgs e)
        {
            checkInDataGrid.ItemsSource = checkInCheckOutController.FindReservationByReservationNo(searchCheckInTextBox.Text);
        }

        private void checkInCheckInButton_Click(object sender, RoutedEventArgs e)
        {
            selectedReservation = checkInDataGrid.SelectedItem as DataRowView;
            string reservationNo = selectedReservation[0].ToString();
            checkInCheckOutController.CheckInReservation(reservationNo, true);
            checkInDataGrid.ItemsSource = checkInCheckOutController.FindReservationByReservationNo(reservationNo);
        }

        private void checkOutCheckInButton_Click(object sender, RoutedEventArgs e)
        {
            selectedReservation = checkInDataGrid.SelectedItem as DataRowView;
            string reservationNo = selectedReservation[0].ToString();
            checkInCheckOutController.CheckOutReservation(reservationNo, true);
            checkInDataGrid.ItemsSource = checkInCheckOutController.FindReservationByReservationNo(reservationNo);
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
