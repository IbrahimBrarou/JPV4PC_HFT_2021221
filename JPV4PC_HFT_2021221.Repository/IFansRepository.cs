using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPV4PC_HFT_2021221.Models;

namespace JPV4PC_HFT_2021221.Repository
{
    public interface IFansRepository : IRepository<Fans>
    {
        void UpdateCity(int id, string newcity);
        void UpdatePhone(int id, int NewPhoneNumber);
        void UpdateEmail(int id, string newEmail);
    }
}
