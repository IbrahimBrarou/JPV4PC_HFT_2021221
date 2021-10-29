using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPV4PC_HFT_2021221.Models;

namespace JPV4PC_HFT_2021221.Repository
{
    public interface IArtistsRepository : IRepository<Artists>
    {
        void UpdatePrice(int id, string newprice);
        void UpdateDuration(int id, string newduration);

    }
}
