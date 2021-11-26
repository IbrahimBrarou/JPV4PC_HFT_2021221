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
    public class ReservationsServicesRepository : Repository<ReservationsServices>, IReservationsServicesRepository
    {
        public ReservationsServicesRepository(TalkWithYourFavoriteArtistDbContext DbContext) : base(DbContext) { }
        public override ReservationsServices GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(connection => connection.Id == id);
               
        }
        

    }
}
