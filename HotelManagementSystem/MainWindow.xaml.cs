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
            selectedRoom = AvailabilityDataGrid.SelectedItem as DataRowView;
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

        private void CustomerDetailsBackBtn_Click(object sender, RoutedEventArgs e)
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
                    AvailabilityFromDateDpr.SelectedDate.Value, AvailabilityToDateDpr.SelectedDate.Value, false, false);
                Console.WriteLine("ur code works u are great"); 
            }
            else
            {
                reservationController.AddReservation(randomNo.ToString(), CustomerDetailsEmailTbx.Text, selectedRoom[0].ToString(),
                    AvailabilityFromDateDpr.SelectedDate.Value, AvailabilityToDateDpr.SelectedDate.Value, false, false);
                Console.WriteLine("Kan registrera bookning");
            }

            ShowCustomerRecipt(randomNo.ToString());
        }

                

   

        private void RoomTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckAvailabilityButton_Click(object sender, RoutedEventArgs e)
        {
            AvailabilityDataGrid.ItemsSource = reservationController.GetAvailableRooms("single room", AvailabilityFromDateDpr.SelectedDate.Value, AvailabilityToDateDpr.SelectedDate.Value);
            Console.WriteLine(AvailabilityFromDateDpr.SelectedDate.Value.ToString());
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
            CheckInCheckOutDataGrid.ItemsSource = checkInCheckOutController.FindReservationByReservationNo(CheckInCheckOutSearchTbx.Text);
        }

        private void checkInCheckInButton_Click(object sender, RoutedEventArgs e)
        {
            selectedReservation = CheckInCheckOutDataGrid.SelectedItem as DataRowView;
            string reservationNo = selectedReservation[0].ToString();
            checkInCheckOutController.CheckInReservation(reservationNo, true);
            CheckInCheckOutDataGrid.ItemsSource = checkInCheckOutController.FindReservationByReservationNo(reservationNo);
        }

        private void checkOutCheckInButton_Click(object sender, RoutedEventArgs e)
        {
            selectedReservation = CheckInCheckOutDataGrid.SelectedItem as DataRowView;
            string reservationNo = selectedReservation[0].ToString();
            checkInCheckOutController.CheckOutReservation(reservationNo, true);
            CheckInCheckOutDataGrid.ItemsSource = checkInCheckOutController.FindReservationByReservationNo(reservationNo);
        }

        private void searchCheckInByEmailButton_Click(object sender, RoutedEventArgs e)
        {
            CheckInCheckOutDataGrid.ItemsSource = checkInCheckOutController.FindReservationByEmail(CheckInCheckOutSearchTbx.Text);
        }

        private void showAllCustomersCustomerRegistryButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerRegistryDataGrid.ItemsSource = new CustomerRegistryController().GetCustomerDataView();
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

        private void showReservationsCustomerRegistryButton_Click(object sender, RoutedEventArgs e)
        {

            selectedCustomer = CustomerRegistryDataGrid.SelectedItem as DataRowView;
            string email = selectedCustomer[0].ToString();
            CustomerRegistryDataGrid.ItemsSource = customerRegistryController.FindReservationByEmail(email);
        }

        private void showCustomerDetailsCustomerRegistryButton_Click(object sender, RoutedEventArgs e)
        {
            selectedCustomer = CustomerRegistryDataGrid.SelectedItem as DataRowView;
        }

        private void RecieptDoneBtn_Click(object sender, RoutedEventArgs e)
        {
            BookingTab.SelectedIndex = 0;
        }

        private void searchCustomerRegistryButtton_Click(object sender, RoutedEventArgs e)
        {
            CustomerRegistryDataGrid.ItemsSource = customerRegistryController.FindReservationByEmail(CustomerRegistryEmailTbx.Text);
        }
    }
}
