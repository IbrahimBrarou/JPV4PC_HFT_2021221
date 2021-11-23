using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPV4PC_HFT_2021221.Models;
using JPV4PC_HFT_2021221.Data;
using Microsoft.EntityFrameworkCore;

namespace JPV4PC_HFT_2021221.Repository
{
    public class ReservationsRepository : Repository<Reservations>, IReservationsRepository
    {
        public ReservationsRepository(DbContext DbContext) : base(DbContext){ }
        public void UpdateDate(int id, DateTime newDate)
        {
            var reservation = this.GetOne(id);
            if (reservation == null)
            {
                throw new Exception("This id doesn't match to any order in our Database");

            }
            else
            {
                reservation.DateTime = newDate;
                this.context.SaveChanges();
            }

        }
        public override Reservations GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(reservation => reservation.Id == id);
                
        }
    }
}
