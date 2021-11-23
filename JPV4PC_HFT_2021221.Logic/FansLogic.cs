﻿using JPV4PC_HFT_2021221.Models;
using JPV4PC_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Logic
{
    public class FansLogic : IFansLogic
    {
        protected  IReservationsRepository _ReservationsRepository;
        protected  IFansRepository _FansRepository;
        public FansLogic(IReservationsRepository reservationsRepo, IFansRepository fansRepo)
        {
            _ReservationsRepository = reservationsRepo;
            _FansRepository = fansRepo;
        }
        public void UpdateCity(int id, string newCity)
        {
            this._FansRepository.UpdateCity(id,newCity);
        }
        public void UpdateEmail(int id, string newEmail)
        {
            this._FansRepository.UpdateEmail(id, newEmail);
        }
        public void UpdatePhone(int id, int NewPhoneNumber)
        {
            this._FansRepository.UpdatePhone(id, NewPhoneNumber);
        }
        public Fans AddNewFan(string city, string email, string name ,int phoneNumber)
        {
            if (name== null)
            {
                throw new ArgumentException("ERROR : Please provide a Name");
            }
            else
            {
                Fans NewFan = new Fans() { City = city, Email = email, Name = name, PhoneNumber = phoneNumber };
                this._FansRepository.Add(NewFan);
                return NewFan;
            }
            
        }
        public void DeleteFan(int id)
        {
            Fans FanToDelete = this._FansRepository.GetOne(id);
            if (FanToDelete!=null)
            {
                this._FansRepository.Delete(FanToDelete);
            }
            else
            {
                throw new ArgumentException("Error : No FAN with this Id is found.");
            }
        }
        public IEnumerable<Fans> GetAllFans()
        {
            return this._FansRepository.GetAll();
        }
        public Fans GetFan(int id)
        {
            Fans fanToReturn = this._FansRepository.GetOne(id);
            if (fanToReturn != null)
            {
                return fanToReturn;
            }
            else
            {
                throw new Exception("This ID can't be found on our FansDatabase.");
            }
        }
        // 2 non-crud methods
        public KeyValuePair<string, int> BestFan()
        {


            var BestFan = from fan in this._FansRepository.GetAll()
                          join Reservations in this._ReservationsRepository.GetAll()
                          on fan.Id equals Reservations.FanId
                          group Reservations by Reservations.FanId.Value into gr
                          select new KeyValuePair<string, int>
                          (this._FansRepository.GetOne(gr.Key).Name, gr.Count());
            var maxNumOfReservations = BestFan.OrderBy(x => x.Value).FirstOrDefault();
            return maxNumOfReservations;
        }
        public KeyValuePair<string, int> WorstFan()
        {
            var WorstFan = from fan in this._FansRepository.GetAll()
                           join Reservations in this._ReservationsRepository.GetAll()
                           on fan.Id equals Reservations.FanId
                           group Reservations by Reservations.FanId.Value into gr
                           select new KeyValuePair<string, int>
                           (this._FansRepository.GetOne(gr.Key).Name, gr.Count());
            int minNumOfReservations = WorstFan.Min(x => x.Value);
            var Worstfann = WorstFan.Where(x => x.Value == minNumOfReservations).FirstOrDefault();
            return Worstfann;
        }
        public int ReservationsNumber(int id)
        {
            if (GetFan(id)==null)
            {
                throw new Exception("This Fan id does not exist in our Database.");
            }
            else
            {
                var res = this._ReservationsRepository.GetAll().Count(x => x.FanId == id);
                return res;
            }
            
            
        }



    }
}
