using JPV4PC_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Logic
{
    public interface IArtistsLogic
    {
        public Artists AddNewArtist(string name, int duration, int price, string category);
        public void DeleteArtist(int id);
        Artists GetArtist(int id);
        IEnumerable<Artists> GetAllArtists();
        void UpdateArtistCost(int artistId, int cost);

        IEnumerable<KeyValuePair<string, int>> ArtistEarnings();
        KeyValuePair<string, int> MostPaidArtist();
        KeyValuePair<string, int> LessPaidArtist();

    }
}
