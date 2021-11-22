﻿using JPV4PC_HFT_2021221.Models;
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
        public IEnumerable<ReservationsServices> GetAllConnections()
        {
            return this._ReservationsServicesConnectionRepository.GetAll();
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
        public ReservationsServices GetConnection(int id)
        {
            ReservationsServices ReservationsServicesToReturn = this._ReservationsServicesConnectionRepository.GetOne(id);
            if (ReservationsServicesToReturn != null)
            {
                return ReservationsServicesToReturn;
            }
            else
            {
                throw new Exception("This ID can't be found on our ReservationsServicesDatabase.");
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
                throw new ArgumentException("This ID can't be found on our ArtistsDatabase.");
            }
        }
        public Services AddNewService(string name,int price,int rating)
        {
            if (name == null)
            {
                throw new ArgumentException("ERROR : Please provide a Name");
            }
            else
            {
                Services ServiceToAdd = new Services() { Name = name, Price = price, Rating = rating };
                this._ServicesRepository.Add(ServiceToAdd);
                return ServiceToAdd;
            }
            
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
        public KeyValuePair<string,int> MostPaidArtist()
        {
            var Mostpaidartist = ArtistEarnings().OrderByDescending(x => x.Value).FirstOrDefault();
            return Mostpaidartist;
            
        }
        public KeyValuePair<string,int> LessPaidArtist()
        {
            var Lesspaidartist = ArtistEarnings().OrderBy(x => x.Value).FirstOrDefault();
            return Lesspaidartist;
        }
        public KeyValuePair<string, int> BestFan()
        {
            var BestFan = from fan in this._FansRepository.GetAll()
                          join Reservations in this._ReservationsRepository.GetAll()
                          on fan.Id equals Reservations.FanId
                          group Reservations by Reservations.FanId.Value into gr
                          select new KeyValuePair<string, int>
                          (this._FansRepository.GetOne(gr.Key).Name,gr.Count());
            int maxNumOfReservations = BestFan.Max(x => x.Value);
            var bestfann = BestFan.Where(x => x.Value == maxNumOfReservations).FirstOrDefault();
            return bestfann;
        }
        public KeyValuePair<string, int> WorstFan()
        {
            var WorstFan = from fan in this._FansRepository.GetAll()
                           join Reservations in this._ReservationsRepository.GetAll()
                           on fan.Id equals Reservations.FanId
                           group Reservations by Reservations.FanId.Value into gr
                           select new KeyValuePair<string, int>
                           (this._FansRepository.GetOne(gr.Key).Name,gr.Count());  
            int minNumOfReservations = WorstFan.Min(x => x.Value);
            var Worstfann = WorstFan.Where(x => x.Value == minNumOfReservations).FirstOrDefault();
            return Worstfann;
        }
    }
}
