﻿using JPV4PC_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Logic
{
    public interface IReservationsLogic
    {
        void UpdateReservationDate(int id, DateTime newDate);
        public Reservations AddNewReservation(int fanId, int artistId, DateTime dateTime);
        public void DeleteReservation(int id);
        Reservations GetReservation(int id);
        IEnumerable<Reservations> GetAllReservations();

    }
}