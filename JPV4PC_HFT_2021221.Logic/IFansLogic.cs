using JPV4PC_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Logic
{
    public interface IFansLogic
    {
        void UpdateCity(int id, string newCity);
        void UpdatePhone(int id, int NewPhoneNumber);
        void UpdateEmail(int id, string newEmail);
        
        public Fans AddNewFan(string city, string email, string name, int phoneNumber);
        public void DeleteFan(int id);
        Fans GetFan(int id);
        IEnumerable<Fans> GetAllFans();

        List<KeyValuePair<int, int>> BestFan();
        List<KeyValuePair<int, int>> WorstFan();
        int ReservationsNumber(int id)
;

    }
}
