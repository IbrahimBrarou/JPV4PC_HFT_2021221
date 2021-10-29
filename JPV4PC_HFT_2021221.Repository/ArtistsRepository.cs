﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPV4PC_HFT_2021221.Models;
using JPV4PC_HFT_2021221.Data;

namespace JPV4PC_HFT_2021221.Repository
{
    public class ArtistsRepository : Repository<Artists>, IArtistsRepository
    {
        public ArtistsRepository(TalkWithYourFavoriteArtistDbContext DbContext ): base(DbContext) { }
        public override Artists GetOne(int id)
        {
            return context
                   .Artists
                   .SingleOrDefault(artist => artist.Id == id);
        }
        public void UpdateDuration(int id, int newduration)
        {
            var artist = this.GetOne(id);
            if (artist == null)
            {
                throw new Exception("This id doesn't match to any artist in our Database");
            }
            else
            {
                artist.Duration = newduration;
                this.context.SaveChanges();
            }
        }
        public void UpdatePrice(int id, int newprice)
        {
            var artist = this.GetOne(id);
            if (artist == null)
            {
                throw new Exception("This id doesn't match to any artist in our Database");
            }
            else
            {
                artist.Price = newprice;
                this.context.SaveChanges();
            }
        }
    }
}
