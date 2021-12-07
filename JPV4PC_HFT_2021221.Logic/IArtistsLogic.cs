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
        public Artists AddNewArtist(Artists newartist);
        public void DeleteArtist(int id);
        Artists GetArtist(int id);
        IEnumerable<Artists> GetAllArtists();
        void UpdateArtistCost(int artistId, int cost);

        IEnumerable<KeyValuePair<string, int>> ArtistEarnings();
        List<KeyValuePair<string, int>> MostPaidArtist();
        List<KeyValuePair<string, int>> LessPaidArtist();

    }
}
