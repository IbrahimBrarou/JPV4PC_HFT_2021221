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

        public void UpdateReservationDate(Reservations reser)
        {
            this._ReservationsRepository.UpdateDate(reser.Id, reser.DateTime);
        }
        public Reservations AddNewReservation(Reservations reser)
        {

            
            if (_fansrepo.GetOne((int)reser.FanId)==null || _artistrepo.GetOne((int)reser.ArtistId)==null )
            {
                throw new Exception("Invalid data");
            }
            else
            {
                this._ReservationsRepository.Add(reser);
                return reser;
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
