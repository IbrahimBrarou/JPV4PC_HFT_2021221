using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Logic
{
    public interface IFanLogic
    {
        void UpdateCity(int id, string newCity);
        void UpdatePhone(int id, int NewPhoneNumber);
        void UpdateEmail(int id, string newEmail);
        void UpdateOrderDate(int id, DateTime newDate);
    }
}
