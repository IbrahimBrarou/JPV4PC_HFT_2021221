using JPV4PC_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Logic
{
    public interface IReservationsLogic
    {
        void UpdateReservationDate(Reservations reser);
        public Reservations AddNewReservation(Reservations reser);
        public void DeleteReservation(int id);
        Reservations GetReservation(int id);
        IEnumerable<Reservations> GetAllReservations();

    }
}
