using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HotelManagementSystem.Model.Database;

namespace HotelManagementSystem
{
    class RoomsRepository
    {
        private HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.roomsTableAdapter roomsTableAdapter;
        private DataSetHotel dataSetHotel;

        public RoomsRepository()
        {
            dataSetHotel = new DataSetHotel();
        }

        public void AddRoom(Room room)
        {
            roomsTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.roomsTableAdapter();
            roomsTableAdapter.Insert(room.RoomNo, room.MaxPersons, room.PricePerDay, room.RoomType);
        }

        public void DeleteRoom(Room room)
        {
            roomsTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.roomsTableAdapter();
            roomsTableAdapter.Delete(room.RoomNo, room.MaxPersons, room.PricePerDay, room.RoomType);
        }

        public void UpdateRoom(Room room)
        {
            roomsTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.roomsTableAdapter();
            roomsTableAdapter.Fill(dataSetHotel.rooms);
            DataSetHotel.roomsRow roomsRow = dataSetHotel.rooms.FindByroom_no(room.RoomNo);
            roomsRow.max_persons = room.MaxPersons;
            roomsRow.price_per_day = room.PricePerDay;
            roomsRow.room_type = room.RoomType;
            roomsTableAdapter.Update(roomsRow);
        }

        public DataView GetRooms()
        {
            roomsTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.roomsTableAdapter();
            roomsTableAdapter.Fill(dataSetHotel.rooms);
            DataView roomDataView = new DataView(dataSetHotel.Tables["Rooms"]);
            return roomDataView;
        }

        public Room GetRoomByRoomNo(string roomNo)
        {
            roomsTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.roomsTableAdapter();
            roomsTableAdapter.Fill(dataSetHotel.rooms);
            DataSetHotel.roomsRow roomRow = dataSetHotel.rooms.FindByroom_no(roomNo);
            return new Room(roomRow.room_no, roomRow.max_persons, roomRow.price_per_day, roomRow.room_type);
        }

        public DataView GetAvailableRooms(string roomType, DateTime startDate, DateTime endDate)
        {
            string room_type = "single room";
            roomsTableAdapter = new HotelManagementSystem.Model.Database.DataSetHotelTableAdapters.roomsTableAdapter();
            roomsTableAdapter.FillByAvailableRooms(dataSetHotel.rooms, startDate, endDate, startDate, endDate);
            DataView roomDataView = new DataView(dataSetHotel.Tables["Rooms"]);
            return roomDataView;
        }
    }
}
