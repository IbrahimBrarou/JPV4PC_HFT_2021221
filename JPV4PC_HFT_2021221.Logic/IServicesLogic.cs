using JPV4PC_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Logic
{
    public interface IServicesLogic
    {
        Services GetService(int id);
        IEnumerable<Services> GetAllServices();
        void UpdateServiceCost(Services serv);
        public Services AddNewService(Services serv);
        public void DeleteService(int id);

    }
}
