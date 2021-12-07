using JPV4PC_HFT_2021221.Models;
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
        public Fans AddNewFan(Fans fan)
        {
            if (fan.Name== null)
            {
                throw new ArgumentException("ERROR : Please provide a Name");
            }
            else
            {
                
                this._FansRepository.Add(fan);
                return fan;
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
        public List<KeyValuePair<int, int>> BestFan()
        {


            var BestFan = from fan in this._FansRepository.GetAll().ToList()
                          join Reservations in this._ReservationsRepository.GetAll().ToList()
                          on fan.Id equals Reservations.FanId
                          group Reservations by Reservations.FanId.Value into gr
                          select new 
                          {
                              id = gr.Key,
                              c=gr.Count()
                          };
            int max = BestFan.AsEnumerable().Max(t=>t.c);
            
            int[] maxNumOfReservationss = BestFan.Where(x => x.c == max).Select(x => x.id).ToArray();
            List<KeyValuePair<int, int>> r = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < maxNumOfReservationss.Length; i++)
            {
                r.Add(new KeyValuePair<int, int>(maxNumOfReservationss[i], max));
            }
            return r;
        }
        public List<KeyValuePair<int, int>> WorstFan()
        {
            var WorstFan = from fan in this._FansRepository.GetAll().ToList()
                          join Reservations in this._ReservationsRepository.GetAll().ToList()
                          on fan.Id equals Reservations.FanId
                          group Reservations by Reservations.FanId.Value into gr
                          select new
                          {
                              id = gr.Key,
                              c = gr.Count()
                          };
            int min = WorstFan.AsEnumerable().Min(t => t.c);
            int[] maxNumOfReservationss = WorstFan.Where(x => x.c == min).Select(x => x.id).ToArray();
            List<KeyValuePair<int, int>> r = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < maxNumOfReservationss.Length; i++)
            {
                r.Add(new KeyValuePair<int, int>(maxNumOfReservationss[i],min));
            }
            
            return r;
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
