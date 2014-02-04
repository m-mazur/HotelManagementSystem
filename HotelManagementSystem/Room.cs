using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem
{
    public class Room
    {
        string YOLO<
        string roomNo;
        int maxPersons;
        double pricePerDay;
        string roomType;

        public Room(string roomNo, int maxPersons, double pricePerDay, string roomType)
        {
            this.roomNo = roomNo;
            this.maxPersons = maxPersons;
            this.pricePerDay = pricePerDay;
            this.RoomType = roomType;
        }

        public string RoomNo
        {
            get { return roomNo; }
            set { roomNo = value; }
        }

        public int MaxPersons
        {
            get { return maxPersons; }
            set { maxPersons = value; }
        }

        public double PricePerDay
        {
            get { return pricePerDay; }
            set { pricePerDay = value; }
        }

        public string RoomType
        {
            get { return roomType; }
            set { roomType = value; }
        }
    }
}
