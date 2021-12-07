using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPV4PC_HFT_2021221.Models;
using JPV4PC_HFT_2021221.Data;
using Microsoft.EntityFrameworkCore;

namespace JPV4PC_HFT_2021221.Repository
{
    public class FansRepository : Repository<Fans>, IFansRepository
    {
        public FansRepository(TalkWithYourFavoriteArtistDbContext DbContext) : base(DbContext){ }
        public override Fans GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(fan => fan.Id == id);
                
        }
        public void UpdateCity(int id, string newcity)
        {
            var Fan = this.GetOne(id);
            if (Fan == null)
            {
                throw new Exception("This id doesn't match to any Fan in our Database");
            }
            else
            {
                Fan.City = newcity;
                this.context.SaveChanges();
            }
        }
    }
}
