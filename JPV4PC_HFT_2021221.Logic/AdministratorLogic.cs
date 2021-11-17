using JPV4PC_HFT_2021221.Models;
using JPV4PC_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Logic
{
    public class AdministratorLogic : IAdministratorLogic
    {
        private readonly IServicesRepository _ServicesRepository;
        private readonly IReservationsRepository _ReservationsRepository;
        private readonly IFansRepository _FansRepository;
        private readonly IArtistsRepository _ArtistRepository;
        private readonly IReservationsServicesRepository _ReservationsServicesConnectionRepository;
        public AdministratorLogic(IServicesRepository servicesRepository, IReservationsRepository reservationsRepository, IFansRepository fansRepository, IArtistsRepository artistRepository, IReservationsServicesRepository reservationsServicesConnectionRepository)
        {
            _ServicesRepository = servicesRepository;
            _ReservationsRepository = reservationsRepository;
            _FansRepository = fansRepository;
            _ArtistRepository = artistRepository;
            _ReservationsServicesConnectionRepository = reservationsServicesConnectionRepository;
        }
        public void UpdateArtistCost(int artistId, int cost)
        {
            this._ArtistRepository.UpdatePrice(artistId, cost);
        }
        public void UpdateServiceCost(int serviceId, int cost)
        {
            this._ServicesRepository.UpdatePrice(serviceId, cost);
        }




        public IEnumerable<Fans> GetAllFans()
        {
            return this._FansRepository.GetAll();
        }
        public IEnumerable<Reservations> GetAllReservations()
        {
            return this._ReservationsRepository.GetAll();
        }
        public IEnumerable<Services> GetAllServices()
        {
            return this._ServicesRepository.GetAll();
        }
        public IEnumerable<Artists> GetAllArtists()
        {
            return this._ArtistRepository.GetAll();
        }




        public Services GetService(int id)
        {
            Services ServiceToReturn = this._ServicesRepository.GetOne(id);
            if (ServiceToReturn != null)
            {
                return ServiceToReturn;
            }
            else
            {
                throw new Exception("This ID can't be found on our ServicesDatabase.");
            }
        }
        public Artists GetArtist(int id)
        {
            Artists  ArtistToReturn = this._ArtistRepository.GetOne(id);
            if (ArtistToReturn !=null)
            {
                return ArtistToReturn;
            }
            else
            {
                throw new Exception("This ID can't be found on our ArtistsDatabase.");
            }
        }
        public Fans GetFan(int id)
        {
            Fans fanToReturn = this._FansRepository.GetOne(id);
            if (fanToReturn!=null)
            {
                return fanToReturn;
            }
            else
            {
                throw new Exception("This ID can't be found on our FansDatabase.");
            }
        }
        public Reservations GetReservation(int id)
        {
            Reservations ReservationToReturn = this._ReservationsRepository.GetOne(id);
            if (ReservationToReturn!=null)
            {
                return ReservationToReturn;
            }
            else
            {
                throw new Exception("This ID can't be found on our ReservationsDatabase.");
            }
        }


        
        public Artists AddNewArtist(string name, int duration, int price,string category)
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
                throw new Exception("This ID can't be found on our ArtistsDatabase.");
            }
        }
        public Services AddNewService(string name,int price,int rating)
        {
            Services ServiceToAdd = new Services() { Name = name, Price = price, Rating = rating };
            this._ServicesRepository.Add(ServiceToAdd);
            return ServiceToAdd;
        }
        public void DeleteService(int id)
        {
            Services ServiceToDelete = this._ServicesRepository.GetOne(id);
            if (ServiceToDelete !=null)
            {
                this._ServicesRepository.Delete(ServiceToDelete);
            }
            else
            {
                throw new Exception("This ID can't be found on our ServicesDatabase.");
            }
        }



        public IEnumerable<ArtistEarnings> ArtistEarnings()
        {
            var TotalEarning = from artists in this._ArtistRepository.GetAll()
                               join reservations in this._ReservationsRepository.GetAll()
                               on artists.Id equals reservations.ArtistId
                               group reservations by reservations.ArtistId.Value into gr
                               select new ArtistEarnings()
                               {
                                   ArtistId = gr.Key,
                                   ArtistName = this._ArtistRepository.GetOne(gr.Key).Name,
                                   FinishedJobs = gr.Count(),
                                   OverallEarnings = (gr.Count()) * this._ArtistRepository.GetOne(gr.Key).Price,
                               };
            return TotalEarning;
        }
        public IEnumerable<FanTotalSpending> BestFan()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<FanTotalSpending> GetFanTotalSpendings()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ArtistEarnings> MostPaidArtist()
        {
            var Mostpaidartist = ArtistEarnings().OrderBy(x => x.OverallEarnings);
            return Mostpaidartist;
        }

        

        

        

        
    }
}
