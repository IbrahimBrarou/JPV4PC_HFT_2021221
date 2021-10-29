using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPV4PC_HFT_2021221.Models;
using JPV4PC_HFT_2021221.Data;

namespace JPV4PC_HFT_2021221.Repository
{
    public class ReservationsServicesRepository : Repository<ReservationsServices>, IReservationsServicesRepository
    {
        public ReservationsServicesRepository(TalkWithYourFavoriteArtistDbContext DbContext) : base(DbContext) { }
        public override ReservationsServices GetOne(int id)
        {
            return context
                .ConnectorTable
                .SingleOrDefault(connection => connection.Id == id);
        }
    }
}
