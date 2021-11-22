using JPV4PC_HFT_2021221.Models;
using JPV4PC_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Logic
{
    public class ArtistsLogic : IArtistsLogic
    {
        private readonly IArtistsRepository _ArtistRepository;
        private readonly IReservationsRepository _ReservationsRepository;

        public ArtistsLogic(IArtistsRepository artistRepository, IReservationsRepository reservationsRepository)
        {
            _ArtistRepository = artistRepository;
            _ReservationsRepository = reservationsRepository;
        }

        public Artists AddNewArtist(string name, int duration, int price, string category)
        {
            Artists NewArtist = new Artists() { Name = name, Duration = duration, Price = price, Category = category };
            this._ArtistRepository.Add(NewArtist);
            return NewArtist;
        }
        public void DeleteArtist(int id)
        {
            Artists ArtistToDelete = this._ArtistRepository.GetOne(id);
            if (ArtistToDelete != null)
            {
                this._ArtistRepository.Delete(ArtistToDelete);
            }
            else
            {
                throw new ArgumentException("This ID can't be found on our ArtistsDatabase.");
            }
        }
        public void UpdateArtistCost(int artistId, int cost)
        {
            this._ArtistRepository.UpdatePrice(artistId, cost);
        }
        public IEnumerable<Artists> GetAllArtists()
        {
            return this._ArtistRepository.GetAll();
        }
        public Artists GetArtist(int id)
        {
            Artists ArtistToReturn = this._ArtistRepository.GetOne(id);
            if (ArtistToReturn != null)
            {
                return ArtistToReturn;
            }
            else
            {
                throw new Exception("This ID can't be found on our ArtistsDatabase.");
            }
        }
        // 3 non-crud methods
        public IEnumerable<KeyValuePair<string, int>> ArtistEarnings()
        {
            var TotalEarning = from artists in this._ArtistRepository.GetAll()
                               join reservations in this._ReservationsRepository.GetAll()
                               on artists.Id equals reservations.ArtistId
                               group reservations by reservations.ArtistId.Value into gr
                               select new KeyValuePair<string, int>
                               (this._ArtistRepository.GetOne(gr.Key).Name, (gr.Count()) * this._ArtistRepository.GetOne(gr.Key).Price);
            return TotalEarning;
        }
        public KeyValuePair<string, int> MostPaidArtist()
        {
            var Mostpaidartist = ArtistEarnings().OrderByDescending(x => x.Value).FirstOrDefault();
            return Mostpaidartist;

        }
        public KeyValuePair<string, int> LessPaidArtist()
        {
            var Lesspaidartist = ArtistEarnings().OrderBy(x => x.Value).FirstOrDefault();
            return Lesspaidartist;
        }
    }
}
