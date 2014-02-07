﻿using System;
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
        private DataRowView selectedRoom;
        private DataRowView selectedReservation;
        private Random random;
        private Boolean registerEnabled;

        public MainWindow()
        {
            InitializeComponent();
            reservationController = new ReservationController();
            checkInCheckOutController = new CheckInCheckOutController();
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
            random = new Random();
            int randomNo = random.Next(000000, 999999);

            if (registerEnabled)
            {
               reservationController.AddCustomer(EmailTextbox.Text, PhoneNoTextBox.Text, PhoneCountryCodeTextBox.Text, 
                   CreditCardNoTextBox.Text, FirstNameTextBox.Text, LastnameTextbox.Text);
                Console.WriteLine("Kan registrer kund");

                reservationController.AddReservation(randomNo.ToString(), EmailTextbox.Text, selectedRoom[0].ToString(),
                    FromDateDatepicker.SelectedDate.Value, ToDateDatepicker.SelectedDate.Value, false, false);
                Console.WriteLine("ur code works u are great"); 
            }
            else
            {
                reservationController.AddReservation(randomNo.ToString(), EmailTextbox.Text, selectedRoom[0].ToString(), 
                    FromDateDatepicker.SelectedDate.Value, ToDateDatepicker.SelectedDate.Value, false, false);
                Console.WriteLine("Kan registrera bookning");
            }
            emailRecieptTextBox.Text = reservationController.GetSingleReservation(randomNo.ToString()).EMail;
            reservationNumberRecieptTextBox.Text = reservationController.GetSingleReservation(randomNo.ToString()).ReservationNo;
            roomNumberRecieptTextBox.Text = reservationController.GetSingleReservation(randomNo.ToString()).RoomNo;
            checkInDateRecieptTextBox.Text = reservationController.GetSingleReservation(randomNo.ToString()).CheckInDate.ToString();
            checkOutDateRecieptTextBox.Text = reservationController.GetSingleReservation(randomNo.ToString()).CheckOutDate.ToString();
            
            string customerEmail = reservationController.GetSingleReservation(randomNo.ToString()).EMail;
            firstNameRecieptTextBox.Text = reservationController.GetCustomer(customerEmail).FirstName;
            lastNameRecieptTextBox.Text = reservationController.GetCustomer(customerEmail).LastName;
            creditCardNoRecieptTextBox.Text = reservationController.GetCustomer(customerEmail).CreditCardNo;
            PhoneNoRecieptTextBox.Text = reservationController.GetCustomer(customerEmail).PhoneNo;
            PhoneCountryCodeRecieptTextBox.Text = reservationController.GetCustomer(customerEmail).PhoneCountryCode;

            string roomNo = reservationController.GetSingleReservation(randomNo.ToString()).RoomNo;
            roomTypeRecieptTextBox.Text = reservationController.GetRoom(roomNo).RoomType;
            pricePerDayRecieptTextBox.Text = reservationController.GetRoom(roomNo).PricePerDay.ToString();
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


    }
}
