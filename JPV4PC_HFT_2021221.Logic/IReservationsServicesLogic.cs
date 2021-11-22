using JPV4PC_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Logic
{
    public interface IReservationsServicesLogic
    {
        public ReservationsServices AddNewConnection(int reservationId, int serviceId);

        public void DeleteConnection(int id);
        public ReservationsServices GetConnection(int id);
        public IEnumerable<ReservationsServices> GetAllConnections();
    }
}
