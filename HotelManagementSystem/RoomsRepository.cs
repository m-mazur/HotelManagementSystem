using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HotelManagementSystem
{
    class RoomsRepository
    {
        private DataSetHotelTableAdapters.roomsTableAdapter roomsTableAdapter;
        private DataSetHotel dataSetHotel;

        public RoomsRepository()
        {
            dataSetHotel = new DataSetHotel();
        }

        public void AddRoom(Room room)
        {
            roomsTableAdapter = new DataSetHotelTableAdapters.roomsTableAdapter();
            try
            {
                roomsTableAdapter.Insert(room.RoomNo, room.MaxPersons, room.PricePerDay, room.RoomType);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteRoom(Room room)
        {
            roomsTableAdapter = new DataSetHotelTableAdapters.roomsTableAdapter();
            try
            {
                roomsTableAdapter.Delete(room.RoomNo, room.MaxPersons, room.PricePerDay, room.RoomType);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void UpdateRoom(Room room)
        {
            roomsTableAdapter = new DataSetHotelTableAdapters.roomsTableAdapter();
            roomsTableAdapter.Fill(dataSetHotel.rooms);
            DataSetHotel.roomsRow roomsRow = dataSetHotel.rooms.FindByroom_no(room.RoomNo);
            try
            {
                roomsRow.max_persons = room.MaxPersons;
                roomsRow.price_per_day = room.PricePerDay;
                roomsRow.room_type = room.RoomType;
                roomsTableAdapter.Update(roomsRow);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public DataView GetRooms()
        {
            try
            {
                roomsTableAdapter = new DataSetHotelTableAdapters.roomsTableAdapter();
                roomsTableAdapter.Fill(dataSetHotel.rooms);
                DataView roomDataView = new DataView(dataSetHotel.Tables["Rooms"]);
                return roomDataView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public Room GetRoomByRoomNo(string roomNo)
        {
            try
            {
                roomsTableAdapter = new DataSetHotelTableAdapters.roomsTableAdapter();
                roomsTableAdapter.Fill(dataSetHotel.rooms);
                DataSetHotel.roomsRow roomRow = dataSetHotel.rooms.FindByroom_no(roomNo);
                return new Room(roomRow.room_no, roomRow.max_persons, roomRow.price_per_day, roomRow.room_type);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        
        public DataView GetAvailableRooms(string roomType, DateTime startDate, DateTime endDate)
        {
            try
            {
                roomsTableAdapter = new DataSetHotelTableAdapters.roomsTableAdapter();
                roomsTableAdapter.FillByAvailableRooms(dataSetHotel.rooms, startDate, endDate, startDate, endDate);
                DataView roomDataView = new DataView(dataSetHotel.Tables["Rooms"]);
                return roomDataView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
