using JPV4PC_HFT_2021221.Models;
using JPV4PC_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Logic
{
    public class ReservationsLogic : IReservationsLogic
    {

        protected  IReservationsRepository _ReservationsRepository;
        protected IFansRepository _fansrepo;
        protected IArtistsRepository _artistrepo;

        public ReservationsLogic(IReservationsRepository reservationsRepository, IFansRepository fansrepo, IArtistsRepository artistrepo)
        {
            _ReservationsRepository = reservationsRepository;
            _fansrepo = fansrepo;
            _artistrepo = artistrepo;
        }

        public void UpdateReservationDate(int id, DateTime newDate)
        {
            this._ReservationsRepository.UpdateDate(id, newDate);
        }
        public Reservations AddNewReservation(int fanId, int artistId, DateTime dateTime)
        {

            Reservations ReservationToAdd = new Reservations() { FanId = fanId, ArtistId = artistId, DateTime = dateTime };
            if (_fansrepo.GetOne(fanId)==null || _artistrepo.GetOne(artistId)==null )
            {
                throw new Exception("Invalid data");
            }
            else
            {
                this._ReservationsRepository.Add(ReservationToAdd);
                return ReservationToAdd;
            }
            
        }
        public void DeleteReservation(int id)
        {
            Reservations ReservationToDelete = this._ReservationsRepository.GetOne(id);
            if (ReservationToDelete != null)
            {
                this._ReservationsRepository.Delete(ReservationToDelete);
            }
        }
        public IEnumerable<Reservations> GetAllReservations()
        {
            return this._ReservationsRepository.GetAll();
        }
        public Reservations GetReservation(int id)
        {
            Reservations ReservationToReturn = this._ReservationsRepository.GetOne(id);
            if (ReservationToReturn != null)
            {
                return ReservationToReturn;
            }
            else
            {
                throw new Exception("This ID can't be found on our ReservationsDatabase.");
            }
        }

    }
}
