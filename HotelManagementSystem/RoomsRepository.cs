﻿using System;
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

        public void AddRoom(Rooms room)
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

        public void DeleteRoom(Rooms room)
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

        public void UpdateRoom(Rooms room)
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
                DataView roomDataView = new DataView(dataSetHotel.Tables["Room"]);
                return roomDataView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public DataSetHotel.roomsRow GetRoomByRoomNo(string roomNo)
        {
            try
            {
                roomsTableAdapter = new DataSetHotelTableAdapters.roomsTableAdapter();
                roomsTableAdapter.Fill(dataSetHotel.rooms);
                DataSetHotel.roomsRow roomRow = dataSetHotel.rooms.FindByroom_no(roomNo);
                return roomRow;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
