using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPV4PC_HFT_2021221.Models;

namespace JPV4PC_HFT_2021221.Data
{
    class TalkWithYourFavoriteArtistDatabaseContext : DbContext
    {
        public TalkWithYourFavoriteArtistDatabaseContext()
        {
            this.Database.EnsureCreated();
        }

        public TalkWithYourFavoriteArtistDatabaseContext(DbContextOptions<TalkWithYourFavoriteArtistDatabaseContext> options)
            : base(options) { }
        public virtual DbSet<Fans> Fans { get; set; }
        public virtual DbSet<Artists> Artists { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<Reservations> Reservations { get; set; }
        public virtual DbSet<ConnectorReservationsServices> ConnectorTable { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder != null && !optionsBuilder.IsConfigured)
            {
                optionsBuilder.
                    UseLazyLoadingProxies().
                    UseSqlServer(@"data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\TalkWithYourFavoriteArtistDB.mdf; Integrated Security=True; MultipleActiveResultSets=True ");
            }
        }
        
    }
}
