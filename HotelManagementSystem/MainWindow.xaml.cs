using System;
using System.Collections.Generic;
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
            ReservationFirstnametextbox.Text = FirstNameTextBox.Text;
            ReservationLastNameTextbox.Text = LastnameTextbox.Text;
            ReservationEmailTextBox.Text = EmailTextbox.Text;
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
        }

        private void BackReciept_Click(object sender, RoutedEventArgs e)
        {
            BookingTab.SelectedIndex = 0;
        }





    }
}
