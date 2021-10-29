using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPV4PC_HFT_2021221.Models;
using JPV4PC_HFT_2021221.Data;

namespace JPV4PC_HFT_2021221.Repository
{
    public class ServicesRepository : Repository<Services>, IServicesRepository
    {
        public ServicesRepository(TalkWithYourFavoriteArtistDbContext DbContext) : base(DbContext) { }
        public override Services GetOne(int id)
        {
            return context
                .Services
                .SingleOrDefault(service => service.Id == id);
        }
        public void UpdateName(int id, string newName)
        {
            var service = this.GetOne(id);
            if (service == null)
            {
                throw new Exception("This id doesn't match to any service in our Database");
            }
            else
            {
                service.Name = newName;
                this.context.SaveChanges();
            }
        }
        public void UpdatePrice(int id, int newPrice)
        {
            var service = this.GetOne(id);
            if (service == null)
            {
                throw new Exception("This id doesn't match to any service in our Database");
            }
            else
            {
                service.Price = newPrice;
                this.context.SaveChanges();
            }
        }
        public void UpdateRating(int id, int newRating)
        {
            var service = this.GetOne(id);
            if (service == null)
            {
                throw new Exception("This id doesn't match to any service in our Database");
            }
            else
            {
                service.Rating = newRating;
                this.context.SaveChanges();
            }
        }
    }
}
