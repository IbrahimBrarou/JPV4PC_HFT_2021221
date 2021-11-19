using JPV4PC_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Logic
{
    public interface IAdministratorLogic
    {
        Services GetService(int id);
        IEnumerable<Services> GetAllServices();
        Reservations GetReservation(int id);
        IEnumerable<Reservations> GetAllReservations();
        Artists GetArtist(int id);
        IEnumerable<Artists> GetAllArtists();
        Fans GetFan(int id);
        IEnumerable<Fans> GetAllFans();
        void UpdateServiceCost(int serviceId, int cost);
        void UpdateArtistCost(int artistId, int cost);
        //////// non-crud ops should be added
        


        IEnumerable<KeyValuePair<string,int>> ArtistEarnings();
        IEnumerable<KeyValuePair<string,int>> MostPaidArtist();
        IEnumerable<KeyValuePair<string, int>> LessPaidArtist();
        IEnumerable<KeyValuePair<string, int>> BestFan();
        IEnumerable<KeyValuePair<string, int>> WorstFan();

        





    }
}
