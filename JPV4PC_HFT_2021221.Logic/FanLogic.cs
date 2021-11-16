using JPV4PC_HFT_2021221.Models;
using JPV4PC_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Logic
{
    public class FanLogic : IFanLogic
    {
        private IReservationsRepository ReservationsRepository;
        private IFansRepository FansRepository;
        private IReservationsServicesRepository ReservationsServicesConnectionRepository;
        public FanLogic(IReservationsRepository reservationsRepo, IFansRepository fansRepo, IReservationsServicesRepository reservationsServicesConnectionRepo)
        {
            ReservationsRepository = reservationsRepo;
            FansRepository = fansRepo;
            ReservationsServicesConnectionRepository = reservationsServicesConnectionRepo;
        }
        public void UpdateCity(int id, string newCity)
        {
            this.FansRepository.UpdateCity(id,newCity);
        }
        public void UpdateEmail(int id, string newEmail)
        {
            this.FansRepository.UpdateEmail(id, newEmail);
        }
        public void UpdatePhone(int id, int NewPhoneNumber)
        {
            this.FansRepository.UpdatePhone(id, NewPhoneNumber);
        }
        public void UpdateOrderDate(int id, DateTime newDate)
        {
            this.ReservationsRepository.UpdateDate(id, newDate);
        }
        public Fans AddNewFan(string city, string email, string name ,int phoneNumber)
        {
            Fans NewFan = new Fans() { City = city, Email = email, Name = name, PhoneNumber=phoneNumber};
            this.FansRepository.Add(NewFan);
            return NewFan;
        }
        public void DeleteFan(int id)
        {
            Fans FanToDelete = this.FansRepository.GetOne(id);
            if (FanToDelete!=null)
            {
                this.FansRepository.Delete(FanToDelete);
            }
        }
        public Reservations AddNewReservation(int fanId, int artistId, DateTime dateTime)
        {
            Reservations ReservationToAdd = new Reservations(){FanId = fanId,ArtistId = artistId,DateTime = dateTime};
            this.ReservationsRepository.Add(ReservationToAdd);
            return ReservationToAdd;
        }
        public void DeleteReservation(int id)
        {
            Reservations ReservationToDelete = this.ReservationsRepository.GetOne(id);
            if (ReservationToDelete!=null)
            {
                this.ReservationsRepository.Delete(ReservationToDelete);
            }
        }
        public ReservationsServices AddNewConnection(int reservationId,int serviceId)
        {
            ReservationsServices ConnectionToAdd = new ReservationsServices() { ReservationId = reservationId, ServiceId = serviceId };
            this.ReservationsServicesConnectionRepository.Add(ConnectionToAdd);
            return ConnectionToAdd;
        }
        public void DeleteConnection(int id)
        {
            ReservationsServices ConnectionToDelete = this.ReservationsServicesConnectionRepository.GetOne(id);
            if (ConnectionToDelete!=null)
            {
                this.ReservationsServicesConnectionRepository.Delete(ConnectionToDelete);
            }
        }



    }
}
