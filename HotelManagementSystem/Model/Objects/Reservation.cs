using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem
{
    public class Reservation
    {
        string reservationNo;
        string eMail;
        string roomNo;
        DateTime checkInDate;
        DateTime checkOutDate;
        Boolean checkIn;
        Boolean checkOut;

        public Reservation(string reservationNo, string eMail, string roomNo,
            DateTime checkInDate, DateTime checkOutDate, Boolean checkIn, Boolean checkOut)
        {
            this.reservationNo = reservationNo;
            this.eMail = eMail;
            this.roomNo = roomNo;
            this.checkInDate = checkInDate;
            this.checkOutDate = checkOutDate;
            this.checkIn = checkIn;
            this.CheckOut = checkOut;
        }

        public string ReservationNo
        {
            get { return reservationNo; }
            set { reservationNo = value; }
        }

        public string EMail
        {
            get { return eMail; }
            set { eMail = value; }
        }

        public string RoomNo
        {
            get { return roomNo; }
            set { roomNo = value; }
        }

        public DateTime CheckInDate
        {
            get { return checkInDate; }
            set { checkInDate = value; }
        }

        public DateTime CheckOutDate
        {
            get { return checkOutDate; }
            set { checkOutDate = value; }
        }

        public Boolean CheckIn
        {
            get { return checkIn; }
            set { checkIn = value; }
        }

        public Boolean CheckOut
        {
            get { return checkOut; }
            set { checkOut = value; }
        }
    }
}
