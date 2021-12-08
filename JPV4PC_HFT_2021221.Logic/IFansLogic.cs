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
        void UpdateCity(Fans fan);
        
        public Fans AddNewFan(Fans fan);
        public void DeleteFan(int id);
        Fans GetFan(int id);
        IEnumerable<Fans> GetAllFans();

        List<KeyValuePair<int, int>> BestFan();
        List<KeyValuePair<int, int>> WorstFan();
        int ReservationsNumber(int id)
;

    }
}
