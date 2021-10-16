using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPV4PC_HFT_2021221.Models;

namespace JPV4PC_HFT_2021221.Data
{
    public class TalkWithYourFavoriteArtistDbContext : DbContext
    {
        public TalkWithYourFavoriteArtistDbContext()
        {
            this.Database.EnsureCreated();
        }

        public TalkWithYourFavoriteArtistDbContext(DbContextOptions<TalkWithYourFavoriteArtistDbContext> options)
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
                    UseSqlServer(@"data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\TalkWithYourFavoriteArtistDB.mdf; Integrated Security=True; MultipleActiveResultSets=True ");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedData();
        }

    }
    public static class DbSeed
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            Services service1 = new Services() { Id = 1, Name = "Video talk ", Price = 500, Rating = 8 };
            Services service2 = new Services() { Id = 2, Name = "Phone call ", Price = 250, Rating = 5 };
            Services service3 = new Services() { Id = 3, Name = "Book for an event ", Price = 5000, Rating = 10 };
            Services service4 = new Services() { Id = 4, Name = "Video Shout-out", Price = 200, Rating = 10 };
            Services service5 = new Services() { Id = 5, Name = "Meet your artist ", Price = 1000, Rating = 9 };
            Services service6 = new Services() { Id = 6, Name = "Audio shout-out", Price = 100, Rating = 4 };

            Artists artist1 = new Artists() { Id = 1, Name = "Stormy", Category = "Singer", Duration = 1, Price = 2500 };
            Artists artist2 = new Artists() { Id = 2, Name = "Rachid El Wali", Category = "Actor", Duration = 2, Price = 10000 };
            Artists artist3 = new Artists() { Id = 3, Name = "Redouane", Category = "Singer", Duration = 1, Price = 20000 };
            Artists artist4 = new Artists() { Id = 4, Name = "Don big", Category = "Singer", Duration = 3, Price = 10000 };
            Artists artist5 = new Artists() { Id = 5, Name = "Ahmed Cherkaoui", Category = "Painter", Duration = 1, Price = 20000 };
            Artists artist6 = new Artists() { Id = 6, Name = "Tahar Ben Jelloun", Category = "Writer", Duration = 1, Price = 30000 };

            Fans fan1 = new Fans() { Id = 1, Name = "ibrahim ", PhoneNumber = 0610203050, City = "Rabat", Email = "ibrahim@gmail.com" };
            Fans fan2 = new Fans() { Id = 2, Name = "kamal", PhoneNumber = 0610203750, City = "Casablanca", Email = "Kamal@gmail.com" };
            Fans fan3 = new Fans() { Id = 3, Name = "Rachid", PhoneNumber = 0610403050, City = "Rabat", Email = "Rachid@gmail.com" };
            Fans fan4 = new Fans() { Id = 4, Name = "Amin", PhoneNumber = 0620203050, City = "Tanger", Email = "Amin@gmail.com" };
            Fans fan5 = new Fans() { Id = 5, Name = "Mohamed", PhoneNumber = 0630203050, City = "Marrakesh", Email = "Mohamed@gmail.com" };
            Fans fan6 = new Fans() { Id = 6, Name = "Nabil", PhoneNumber = 0640203050, City = "Paris", Email = "Nabil@gmail.com" };
            Fans fan7 = new Fans() { Id = 7, Name = "Khalid", PhoneNumber = 0650203050, City = "Agadir", Email = "Khalid@gmail.com" };
            Fans fan8 = new Fans() { Id = 8, Name = "Karim", PhoneNumber = 0660203050, City = "Rabat", Email = "Karim@gmail.com" };
            Fans fan9 = new Fans() { Id = 9, Name = "Sofia", PhoneNumber = 0670203050, City = "Casablance", Email = "Sofia@gmail.com" };
            Fans fan10 = new Fans() { Id = 10, Name = "Meryem", PhoneNumber = 0680203050, City = "Tanger", Email = "Meryem@gmail.com" };
            Fans fan11 = new Fans() { Id = 11, Name = "Youssef", PhoneNumber = 0690203050, City = "Rabat", Email = "Youssef@gmail.com" };

            Reservations reservation1 = new Reservations() { Id = 1, FanId = fan1.Id, ArtistId = artist1.Id, DateTime = new DateTime(2021, 09, 08) };
            Reservations reservation2 = new Reservations() { Id = 2, FanId = fan2.Id, ArtistId = artist3.Id, DateTime = new DateTime(2021, 09, 09) };
            Reservations reservation3 = new Reservations() { Id = 3, FanId = fan5.Id, ArtistId = artist2.Id, DateTime = new DateTime(2021, 09, 10) };
            Reservations reservation4 = new Reservations() { Id = 4, FanId = fan10.Id, ArtistId = artist1.Id, DateTime = new DateTime(2021, 09, 11) };
            Reservations reservation5 = new Reservations() { Id = 5, FanId = fan4.Id, ArtistId = artist6.Id, DateTime = new DateTime(2021, 09, 12) };
            Reservations reservation6 = new Reservations() { Id = 6, FanId = fan11.Id, ArtistId = artist2.Id, DateTime = new DateTime(2021, 09, 13) };
            Reservations reservation7 = new Reservations() { Id = 7, FanId = fan6.Id, ArtistId = artist5.Id, DateTime = new DateTime(2021, 09, 14) };
            Reservations reservation8 = new Reservations() { Id = 8, FanId = fan8.Id, ArtistId = artist6.Id, DateTime = new DateTime(2021, 09, 15) };
            Reservations reservation9 = new Reservations() { Id = 9, FanId = fan3.Id, ArtistId = artist4.Id, DateTime = new DateTime(2021, 09, 16) };
            Reservations reservation10 = new Reservations() { Id = 10, FanId = fan7.Id, ArtistId = artist1.Id, DateTime = new DateTime(2021, 09, 17) };
            Reservations reservation11 = new Reservations() { Id = 11, FanId = fan9.Id, ArtistId = artist2.Id, DateTime = new DateTime(2021, 09, 18) };

            ConnectorReservationsServices connection1 = new ConnectorReservationsServices() { Id = 1, ReservationId = reservation1.Id, ServiceId = service1.Id };
            ConnectorReservationsServices connection2 = new ConnectorReservationsServices() { Id = 2, ReservationId = reservation1.Id, ServiceId = service2.Id };
            ConnectorReservationsServices connection3 = new ConnectorReservationsServices() { Id = 3, ReservationId = reservation2.Id, ServiceId = service3.Id };
            ConnectorReservationsServices connection4 = new ConnectorReservationsServices() { Id = 4, ReservationId = reservation2.Id, ServiceId = service4.Id };
            ConnectorReservationsServices connection5 = new ConnectorReservationsServices() { Id = 5, ReservationId = reservation3.Id, ServiceId = service5.Id };
            ConnectorReservationsServices connection6 = new ConnectorReservationsServices() { Id = 6, ReservationId = reservation4.Id, ServiceId = service6.Id };
            ConnectorReservationsServices connection7 = new ConnectorReservationsServices() { Id = 7, ReservationId = reservation5.Id, ServiceId = service1.Id };
            ConnectorReservationsServices connection8 = new ConnectorReservationsServices() { Id = 8, ReservationId = reservation5.Id, ServiceId = service2.Id };
            ConnectorReservationsServices connection9 = new ConnectorReservationsServices() { Id = 9, ReservationId = reservation6.Id, ServiceId = service3.Id };
            ConnectorReservationsServices connection10 = new ConnectorReservationsServices() { Id = 10, ReservationId = reservation7.Id, ServiceId = service4.Id };
            ConnectorReservationsServices connection11 = new ConnectorReservationsServices() { Id = 11, ReservationId = reservation8.Id, ServiceId = service5.Id };
            ConnectorReservationsServices connection12 = new ConnectorReservationsServices() { Id = 12, ReservationId = reservation9.Id, ServiceId = service6.Id };
            ConnectorReservationsServices connection13 = new ConnectorReservationsServices() { Id = 13, ReservationId = reservation10.Id, ServiceId = service1.Id };
            ConnectorReservationsServices connection14 = new ConnectorReservationsServices() { Id = 14, ReservationId = reservation11.Id, ServiceId = service2.Id };
            ConnectorReservationsServices connection15 = new ConnectorReservationsServices() { Id = 15, ReservationId = reservation2.Id, ServiceId = service3.Id };
            ConnectorReservationsServices connection16 = new ConnectorReservationsServices() { Id = 16, ReservationId = reservation4.Id, ServiceId = service4.Id };
            ConnectorReservationsServices connection17 = new ConnectorReservationsServices() { Id = 17, ReservationId = reservation5.Id, ServiceId = service5.Id };
            ConnectorReservationsServices connection18 = new ConnectorReservationsServices() { Id = 18, ReservationId = reservation8.Id, ServiceId = service6.Id };
            ConnectorReservationsServices connection19 = new ConnectorReservationsServices() { Id = 19, ReservationId = reservation7.Id, ServiceId = service5.Id };
            ConnectorReservationsServices connection20 = new ConnectorReservationsServices() { Id = 20, ReservationId = reservation10.Id, ServiceId = service1.Id };
            
            modelBuilder.Entity<Fans>().HasData(fan1, fan2, fan3, fan4, fan5, fan6, fan7, fan8, fan9, fan10, fan11);
            modelBuilder.Entity<Artists>().HasData(artist1, artist2, artist3, artist4, artist5, artist6);
            modelBuilder.Entity<Services>().HasData(service1, service2, service3, service4, service5, service6);
            modelBuilder.Entity<Reservations>().HasData(reservation1, reservation2, reservation3, reservation4, reservation5, reservation6, reservation7, reservation8, reservation9, reservation10, reservation11);
            modelBuilder.Entity<ConnectorReservationsServices>().HasData(connection1, connection2, connection3, connection4, connection5, connection6, connection7, connection8, connection9, connection10, connection11, connection12, connection13, connection14, connection15, connection16, connection17, connection18, connection19, connection20);
            
            modelBuilder.Entity<Reservations>(entity =>
            {
                entity.HasOne(reservation => reservation.Artist)
                      .WithMany(artist => artist.Reservations)
                      .HasForeignKey(reservation => reservation.ArtistId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Reservations>(entity =>
            {
                entity.HasOne(reservation => reservation.Fan)
                      .WithMany(fan => fan.Reservations)
                      .HasForeignKey(reservation => reservation.FanId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<ConnectorReservationsServices>(entity =>
            {
                entity.HasOne(connection => connection.Reservations)
                      .WithMany(reservation => reservation.ConnectorReservationsServices)
                      .HasForeignKey(connection => connection.ReservationId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<ConnectorReservationsServices>(entity =>
            {
                entity.HasOne(connection => connection.Services)
                      .WithMany(service => service.ConnectorReservationsServices)
                      .HasForeignKey(connection => connection.ServiceId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

        }
    }
}
