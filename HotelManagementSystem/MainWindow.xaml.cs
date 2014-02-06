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
        private DataRowView selectedRoom;

        public MainWindow()
        {
            InitializeComponent();
            reservationController = new ReservationController();
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
            EmailTextbox.Text = reservationController.GetCustomer(EmailTextbox.Text).EMail;
            FirstNameTextBox.Text = reservationController.GetCustomer(EmailTextbox.Text).FirstName;
            LastnameTextbox.Text = reservationController.GetCustomer(EmailTextbox.Text).LastName;
            CreditCardNoTextBox.Text = reservationController.GetCustomer(EmailTextbox.Text).CreditCardNo;
            PhoneCountryCodeTextBox.Text = reservationController.GetCustomer(EmailTextbox.Text).PhoneCountryCode;
            PhoneNoTextBox.Text = reservationController.GetCustomer(EmailTextbox.Text).PhoneNo;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BookingTab.SelectedIndex = 0;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            BookingTab.SelectedIndex = 2;
            reservationController.AddReservation("000113", EmailTextbox.Text, selectedRoom[0].ToString(),
                FromDateDatepicker.SelectedDate.Value, ToDateDatepicker.SelectedDate.Value, false, false);
        }

        private void BackReciept_Click(object sender, RoutedEventArgs e)
        {
            BookingTab.SelectedIndex = 0;
        }

        private void RoomTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void CheckAvailabilityButton_Click(object sender, RoutedEventArgs e)
        {
            RoomInfoGrid.ItemsSource = reservationController.GetAvailableRooms("single room", FromDateDatepicker.SelectedDate.Value, ToDateDatepicker.SelectedDate.Value);
            Console.WriteLine(FromDateDatepicker.SelectedDate.Value.ToString());
        }

        private void checkBoxRegister_Checked(object sender, RoutedEventArgs e)
        {
            registerCustomerBtn.IsEnabled = true;
            SearchCustomer.IsEnabled = false;

            EmailTextbox.Text = "";
            FirstNameTextBox.Text = "";
            LastnameTextbox.Text = "";
            CreditCardNoTextBox.Text = "";
            PhoneCountryCodeTextBox.Text = "";
            PhoneNoTextBox.Text = "";

            EnableDisabletextBoxes(true);
        }

        private void checkBoxRegister_Unchecked(object sender, RoutedEventArgs e)
        {
            registerCustomerBtn.IsEnabled = false;
            SearchCustomer.IsEnabled = true;
            EnableDisabletextBoxes(false);
        }

        private void EnableDisabletextBoxes(bool enabled)
        {
            FirstNameTextBox.IsEnabled = enabled;
            LastnameTextbox.IsEnabled = enabled;
            CreditCardNoTextBox.IsEnabled = enabled;
            PhoneCountryCodeTextBox.IsEnabled = enabled;
            PhoneNoTextBox.IsEnabled = enabled;
        }
    }
}
