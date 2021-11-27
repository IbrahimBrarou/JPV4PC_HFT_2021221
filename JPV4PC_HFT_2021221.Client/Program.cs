using JPV4PC_HFT_2021221.Logic;
using JPV4PC_HFT_2021221.Data;
using JPV4PC_HFT_2021221.Repository;
using ConsoleTools;
using System.Linq;
using System.Collections.Generic;
using System;
using JPV4PC_HFT_2021221.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using JPV4PC_HFT_2021221.Client;

namespace Client
{
    
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(8000);
            RestService rest = new RestService("http://localhost:37793");
            var fans = rest.Get<Fans>("fans");


            using IHost host = CreateHostBuilder(args).Build();

            IFansLogic fanLogic = host.Services.GetRequiredService<IFansLogic>();
            IArtistsLogic artistLogic = host.Services.GetRequiredService<IArtistsLogic>();
            IReservationsLogic reservationsLogic = host.Services.GetRequiredService<IReservationsLogic>();
            IServicesLogic servicesLogic = host.Services.GetRequiredService<IServicesLogic>();
            IReservationsServicesLogic reservationsservicesLogic = host.Services.GetRequiredService<IReservationsServicesLogic>();
            

            var MenuForFansadmin = new ConsoleMenu()
                .Add(">> CREATE", () => AddNewFan(fanLogic))
                .Add(">> READ By Id", () => ReadFanById(fanLogic))
                .Add(">> READ All", () => ReadAllFans(fanLogic))
                .Add(">> UpdateCity", () => UpdateFanCity(fanLogic))
                .Add(">> UpdateEmail", () => UpdateFanEmail(fanLogic))
                .Add(">> UpdatePhone", () => UpdateFanPhone(fanLogic))
                .Add(">> DELETE", () => DeleteFan(fanLogic))
                .Add(">> Best Fan (non-crud)", () => BestFan(fanLogic))
                .Add(">> Worst Fan (non-crud)", () => WorstFan(fanLogic))
                .Add(">> Reservations count (non-crud) ", () => CountResers(fanLogic))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
            var MenuForFans = new ConsoleMenu()
                .Add(">> CREATE", () => AddNewFan(fanLogic))
                .Add(">> Add Reservation", () => AddNewReservation(reservationsLogic))
                .Add(">> Read all Services", () => ReadAllServices(servicesLogic))
                .Add(">> Read all Artists", () => ReadAllArtists(artistLogic))
                .Add(">> UpdateCity", () => UpdateFanCity(fanLogic))
                .Add(">> UpdateEmail", () => UpdateFanEmail(fanLogic))
                .Add(">> UpdatePhone", () => UpdateFanPhone(fanLogic))
                .Add(">> DELETE", () => DeleteFan(fanLogic))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
            var MenuForArtists = new ConsoleMenu()
                .Add(">> CREATE", () => AddNewArtist(artistLogic))
                .Add(">> READ By Id", () => ReadArtistById(artistLogic))
                .Add(">> READ All", () => ReadAllArtists(artistLogic))
                .Add(">> UpdateCost", () => UpdateArtistcost(artistLogic))
                .Add(">> DELETE", () => DeleteArtist(artistLogic))
                .Add(">> Artists Earnings (non-crud)", () => Artistearrings(artistLogic))
                .Add(">> Most Paid Artist (non-crud)", () => MostPaidArt(artistLogic))
                .Add(">> Less Paid Artist (non-crud)", () => LessPaidArt(artistLogic))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
            var MenuForReservations = new ConsoleMenu()
                .Add(">> CREATE", () => AddNewReservation(reservationsLogic))
                .Add(">> READ By Id", () => ReadReservationById(reservationsLogic))
                .Add(">> READ All", () => ReadAllReservations(reservationsLogic))
                .Add(">> UpdateDate", () => UpdateReservationdate(reservationsLogic))
                .Add(">> DELETE", () => DeleteReservation(reservationsLogic))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
            var MenuForServices = new ConsoleMenu()
                .Add(">> CREATE", () => AddNewService(servicesLogic))
                .Add(">> READ By Id", () => ReadServiceById(servicesLogic))
                .Add(">> READ All", () => ReadAllServices(servicesLogic))
                .Add(">> UpdateCost", () => UpdateServicecost(servicesLogic))
                .Add(">> DELETE", () => DeleteService(servicesLogic))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
            var MenuForReservationsServices = new ConsoleMenu()
                .Add(">> CREATE", () => AddNewConnection(reservationsservicesLogic))
                .Add(">> READ By Id", () => ReadConnectionById(reservationsservicesLogic))
                .Add(">> READ All", () => ReadAllConnections(reservationsservicesLogic))
                .Add(">> DELETE", () => DeleteConnection(reservationsservicesLogic))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });

            var menuForAdministrator = new ConsoleMenu(args, level: 0)
                .Add(">> Fans", () => MenuForFansadmin.Show())
                .Add(">> Artists ", () => MenuForArtists.Show())
                .Add(">> Reservations ", () => MenuForReservations.Show())
                .Add(">> Services ", () => MenuForServices.Show())
                .Add(">> ReservationsServicesConnections ", () => MenuForReservationsServices.Show())
                .Add(">> Exit", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Blue;
                });
            var MainMenu = new ConsoleMenu(args, level: 0)
                .Add(">> Fan", () => MenuForFans.Show())
                .Add(">> Administrator ", () => menuForAdministrator.Show())
                .Add(">> Exit", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Blue;
                });

            MainMenu.Show();

        }
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\TalkWithYourFavoriteArtistDB.mdf;Integrated Security=True;MultipleActiveResultSets=True";

            return Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                })
                .ConfigureServices((_, services) =>
                    services.AddDbContext<TalkWithYourFavoriteArtistDbContext>(options => options.UseSqlServer(connectionString))
                            .AddTransient<IFansRepository, FansRepository>()
                            .AddTransient<IArtistsRepository, ArtistsRepository>()
                            .AddTransient<IReservationsRepository, ReservationsRepository>()
                            .AddTransient<IServicesRepository, ServicesRepository>()
                            .AddTransient<IReservationsServicesRepository, ReservationsServicesRepository>()
                            .AddTransient<IFansLogic, FansLogic>()
                            .AddTransient<IArtistsLogic, ArtistsLogic>()
                            .AddTransient<IReservationsLogic, ReservationsLogic>()
                            .AddTransient<IServicesLogic, ServicesLogic>()
                            .AddTransient<IReservationsServicesLogic, ReservationsServicesLogic>());
        }


        #region fansMethods
        private static void AddNewFan(IFansLogic fanLogic)
        {
            try
            {
                Console.WriteLine("\n:: New Fan ::\n");
                Console.WriteLine("Fan's Name : ");
                string name = Console.ReadLine();

                Console.WriteLine("Fan's City : ");
                string city = Console.ReadLine();
                
                Console.WriteLine("Fan's Email : ");
                string email = Console.ReadLine();

                Console.WriteLine("Fan's Phone number : ");
                int phoneNumber = int.Parse(Console.ReadLine());
               
                Fans fanToAdd = fanLogic.AddNewFan(city, email,name, phoneNumber);
                
                Console.WriteLine("\n A fan with ID "+ fanToAdd.Id.ToString() + " has been added to the Database\n");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
        private static void ReadFanById(IFansLogic fanLogic)
        {
            Console.Write("\n ID of Fan :  ");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n{"Id",3} | {"Name",-20} {"Email",-28} {"PhoneNumber",10}  {"City",5}");
                Console.ResetColor();
                Console.WriteLine(fanLogic.GetFan(id).ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void CountResers(IFansLogic fanLogic)
        {
            Console.Write("Fan's ID : ");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("This fan has : " + fanLogic.ReservationsNumber(id) + " reservations.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            
            Console.ReadLine();
        }
        private static void ReadAllFans(IFansLogic fanLogic)
        {
            Console.WriteLine("\n   ALL Fans :  \n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n{"Id",3} | {"Name",-20} {"Email",-28} {"PhoneNumber",10} {"City",5}");
            Console.ResetColor();
            fanLogic.GetAllFans().ToList().ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void UpdateFanEmail(IFansLogic fanLogic)
        {
            Console.WriteLine("\n  Fan's ID : \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Fan Email : " + fanLogic.GetFan(id).Email);
                Console.WriteLine("\n New Email : ");
                string email = Console.ReadLine();
                fanLogic.UpdateEmail(id, email);
                Console.WriteLine("Email Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void UpdateFanCity(IFansLogic fanLogic)
        {
            Console.WriteLine("\n  Fan's ID : \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Fan City : " + fanLogic.GetFan(id).City);
                Console.WriteLine("\n New City : ");
                string city = Console.ReadLine();
                fanLogic.UpdateCity(id, city);
                Console.WriteLine("City Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void UpdateFanPhone(IFansLogic fanLogic)
        {
            Console.WriteLine("\n  Fan's ID : \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Fan Phone Number  : " + fanLogic.GetFan(id).PhoneNumber);
                Console.WriteLine("\n New Phone Number : ");
                int PhoneNum = int.Parse(Console.ReadLine());
                fanLogic.UpdatePhone(id, PhoneNum);
                Console.WriteLine("phone number Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void DeleteFan(IFansLogic fanLogic)
        {
            Console.WriteLine("\n Fan's ID :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("\n  Fan who will be deleted  " + fanLogic.GetFan(id).Name );
                fanLogic.DeleteFan(id);
                Console.WriteLine("  Fan deleted! ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void BestFan(IFansLogic fanLogic)
        {
            foreach (var item in fanLogic.BestFan())
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Best Fan Id : " + item.Key + ", Reservations number : " + item.Value);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        private static void WorstFan(IFansLogic fanLogic)
        {
            foreach (var item in fanLogic.WorstFan())
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Worst Fan Id : " + item.Key + ", Reservations number : " + item.Value);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        #endregion

        #region ArtistsMethods
        private static void AddNewArtist(IArtistsLogic artistLogic)
        {
            Console.WriteLine("\n:: New Artist ::\n");
            Console.WriteLine("Artit's Name : ");
            string name = Console.ReadLine();

            Console.WriteLine("Artist's Duration : ");
            int duration = int.Parse(Console.ReadLine());

            Console.WriteLine("Artist's price : ");
            int price = int.Parse(Console.ReadLine());

            Console.WriteLine("Artist's category  : ");
            string category = Console.ReadLine();

            Artists artistToAdd = artistLogic.AddNewArtist(name, duration, price, category);

            Console.WriteLine("\n An artist with ID " + artistToAdd.Id.ToString() + " has been added to the Database\n");

            Console.ReadLine();
        }
        private static void ReadArtistById(IArtistsLogic artistLogic)
        {
            Console.WriteLine("\n ID of Artist :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n{"Id",3} |  {"Duration"}  {"Price",10}  {"Category",10} {"Name",15}");
                Console.ResetColor();
                Console.WriteLine(artistLogic.GetArtist(id).ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ReadAllArtists(IArtistsLogic artistLogic)
        {
            Console.WriteLine("\n   ALL Artists :  \n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n{"Id",3} |  {"Duration"}  {"Price",10}  {"Category",10} {"Name",15}");
            Console.ResetColor();
            artistLogic.GetAllArtists().ToList().ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void UpdateArtistcost(IArtistsLogic artistLogic)
        {
            Console.WriteLine("\n  Artist's ID : \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Artist Cost : " + artistLogic.GetArtist(id).Price);
                Console.WriteLine("\n New Cost : ");
                int cost = int.Parse(Console.ReadLine());
                artistLogic.UpdateArtistCost(id, cost);
                Console.WriteLine("Cost Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void DeleteArtist(IArtistsLogic artistLogic)
        {
            Console.WriteLine("\n Artist's ID :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("\n  Artist who will be deleted  " + artistLogic.GetArtist(id).Name);
                artistLogic.DeleteArtist(id);
                Console.WriteLine("  Artist deleted! ");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void Artistearrings(IArtistsLogic artistLogic)
        {
            foreach (var item in artistLogic.ArtistEarnings())
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("ARTIST NAME  : " + item.Key + ", OVERALL EARNINGS : " + item.Value);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        private static void MostPaidArt(IArtistsLogic artistLogic)
        {
            foreach (var item in artistLogic.MostPaidArtist())
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("ARTIST NAME  : " + item.Key + ", OVERALL EARNINGS : " + item.Value);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        private static void LessPaidArt(IArtistsLogic artistLogic)
        {
            foreach (var item in artistLogic.LessPaidArtist())
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("ARTIST NAME  : " + item.Key + ", OVERALL EARNINGS : " + item.Value);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        #endregion

        #region ReservationsMethods
        private static void AddNewReservation(IReservationsLogic reservationsLogic)
        {
            Console.WriteLine("\n:: New Artist ::\n");
            

            Console.WriteLine("Fan ID  : ");
            int fanId = int.Parse(Console.ReadLine());

            Console.WriteLine("Artist ID : : ");
            int artistId = int.Parse(Console.ReadLine());

            Console.WriteLine(" Date [yyyy-MM-dd HH:mm] : ");
            DateTime dateTime = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm",null);
            try
            {
                Reservations reservationToAdd = reservationsLogic.AddNewReservation(fanId, artistId, dateTime);
                Console.WriteLine("\n A Reservation with ID " + reservationToAdd.Id.ToString() + " has been added to the Database\n");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ReadReservationById(IReservationsLogic reservationsLogic)
        {
            Console.WriteLine("\n ID of Reservation :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n{"Id",3} | {"Fan Id ",-20} {"DateTime",10} {"Artist Id",25}");
                Console.ResetColor();
                Console.WriteLine(reservationsLogic.GetReservation(id).ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ReadAllReservations(IReservationsLogic reservationsLogic)
        {
            Console.WriteLine("\n   ALL Reservations :  \n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n{"Id",3} | {"Fan Id ",-20} {"DateTime",10} {"Artist Id",25}");
            Console.ResetColor();
            reservationsLogic.GetAllReservations().ToList().ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void UpdateReservationdate(IReservationsLogic reservationsLogic)
        {
            Console.WriteLine("\n  Reservation's ID : \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Reservation's Date : " + reservationsLogic.GetReservation(id).DateTime);
                Console.WriteLine("\n New Date [yyyy - MM - dd HH: mm] :  ");
                DateTime date = DateTime.ParseExact(Console.ReadLine(),"yyyy-MM-dd HH:mm", null);
                reservationsLogic.UpdateReservationDate(id, date);
                Console.WriteLine("Date Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void DeleteReservation(IReservationsLogic reservationsLogic)
        {
            Console.WriteLine("Reservation's ID which will be deleted ");

            int id = int.Parse(Console.ReadLine());

            reservationsLogic.DeleteReservation(id);
            Console.WriteLine("  Reservation deleted! ");

            Console.ReadLine();
        }
        #endregion

        #region ServicesMethods
        private static void AddNewService(IServicesLogic servicesLogic)
        {
            Console.WriteLine("\n:: New Service ::\n");

            Console.WriteLine("Service's name :  : ");
            string name = Console.ReadLine();

            Console.WriteLine("Service's price  : ");
            int price = int.Parse(Console.ReadLine());

            Console.WriteLine("Service's rating : : ");
            int rating = int.Parse(Console.ReadLine());

            Services serviceToAdd = servicesLogic.AddNewService(name,price,rating);

            Console.WriteLine("\n A Service with ID " + serviceToAdd.Id.ToString() + " has been added to the Database\n");

            Console.ReadLine();
        }
        private static void ReadServiceById(IServicesLogic servicesLogic)
        {
            Console.WriteLine("\n ID of Service :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n{"Id",3} | {"Rating",2}/10 {"Price",7}  {"Name",10}");
                Console.ResetColor();
                Console.WriteLine(servicesLogic.GetService(id).ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ReadAllServices(IServicesLogic servicesLogic)
        {
            Console.WriteLine("\n   ALL Services :  \n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n{"Id",3} | {"Rating",2}/10 {"Price",7}  {"Name",10}");
            Console.ResetColor();
            servicesLogic.GetAllServices().ToList().ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void UpdateServicecost(IServicesLogic servicesLogic)
        {
            Console.WriteLine("\n  Service's ID : \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Service's Cost : " + servicesLogic.GetService(id).Price);
                Console.WriteLine("\n New Cost :  ");
                int cost = int.Parse(Console.ReadLine());
                servicesLogic.UpdateServiceCost(id, cost);
                Console.WriteLine("Cost Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void DeleteService(IServicesLogic servicesLogic)
        {
            Console.WriteLine("Service's ID which will be deleted ");
            int id = int.Parse(Console.ReadLine());

            servicesLogic.DeleteService(id);
            Console.WriteLine("  Reservation deleted! ");

            Console.ReadLine();
        }
        #endregion

        #region ConnectionsMethods
        private static void AddNewConnection(IReservationsServicesLogic reservationsServicesLogic)
        {
            Console.WriteLine("\n:: New Connection ::\n");

            Console.WriteLine("Reservation's ID  : ");
            int reservationId = int.Parse(Console.ReadLine());

            Console.WriteLine("Service's ID: : ");
            int serviceid = int.Parse(Console.ReadLine());

            ReservationsServices ConnectionToAdd = reservationsServicesLogic.AddNewConnection(reservationId,serviceid);

            Console.WriteLine("\n A Connection with ID " + ConnectionToAdd.Id.ToString() + " has been added to the Database\n");

            Console.ReadLine();
        }
        private static void ReadConnectionById(IReservationsServicesLogic reservationsServicesLogic)
        {
            Console.WriteLine("\n ID of Connection :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{"Id",3} | {"ReservationId",5} {"ServiceId",10}");
                Console.ResetColor();
                Console.WriteLine(reservationsServicesLogic.GetConnection(id).ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ReadAllConnections(IReservationsServicesLogic reservationsServicesLogic)
        {
            Console.WriteLine("\n   ALL Connections :  \n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{"Id",3} | {"ReservationId",5}\t {"ServiceId",10}");
            Console.ResetColor();
            reservationsServicesLogic.GetAllConnections().ToList().ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void DeleteConnection(IReservationsServicesLogic reservationsServicesLogic)
        {
            Console.WriteLine("Connection's ID which will be deleted ");
            int id = int.Parse(Console.ReadLine());

            reservationsServicesLogic.DeleteConnection(id);
            Console.WriteLine("  Connection deleted! ");

            Console.ReadLine();
        }
        #endregion



    }
}
