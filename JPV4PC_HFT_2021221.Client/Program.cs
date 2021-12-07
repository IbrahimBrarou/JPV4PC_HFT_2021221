using ConsoleTools;
using System.Linq;
using System.Collections.Generic;
using System;
using JPV4PC_HFT_2021221.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
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
            
            var MenuForFansadmin = new ConsoleMenu()
                .Add(">> CREATE", () => AddNewFan(rest))
                .Add(">> READ By Id", () => ReadFanById(rest))
                .Add(">> READ All", () => ReadAllFans(rest))
                .Add(">> UpdateCity", () => UpdateFanCity(rest))
                .Add(">> DELETE", () => DeleteFan(rest))
                .Add(">> Best Fan (non-crud)", () => BestFan(rest))
                .Add(">> Worst Fan (non-crud)", () => WorstFan(rest))
                .Add(">> Reservations count (non-crud) ", () => CountResers(rest))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
            var MenuForFans = new ConsoleMenu()
                .Add(">> CREATE", () => AddNewFan(rest))
                .Add(">> Add Reservation", () => AddNewReservation(rest))
                .Add(">> Read all Services", () => ReadAllServices(rest))
                .Add(">> Read all Artists", () => ReadAllArtists(rest))
                .Add(">> UpdateCity", () => UpdateFanCity(rest))
                .Add(">> DELETE", () => DeleteFan(rest))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
            var MenuForArtists = new ConsoleMenu()
                .Add(">> CREATE", () => AddNewArtist(rest))
                .Add(">> READ By Id", () => ReadArtistById(rest))
                .Add(">> READ All", () => ReadAllArtists(rest))
                .Add(">> UpdateCost", () => UpdateArtistcost(rest))
                .Add(">> DELETE", () => DeleteArtist(rest))
                .Add(">> Artists Earnings (non-crud)", () => Artistearrings(rest))
                .Add(">> Most Paid Artist (non-crud)", () => MostPaidArt(rest))
                .Add(">> Less Paid Artist (non-crud)", () => LessPaidArt(rest))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
            var MenuForReservations = new ConsoleMenu()
                .Add(">> CREATE", () => AddNewReservation(rest))
                .Add(">> READ By Id", () => ReadReservationById(rest))
                .Add(">> READ All", () => ReadAllReservations(rest))
                .Add(">> UpdateDate", () => UpdateReservationdate(rest))
                .Add(">> DELETE", () => DeleteReservation(rest))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
            var MenuForServices = new ConsoleMenu()
                .Add(">> CREATE", () => AddNewService(rest))
                .Add(">> READ By Id", () => ReadServiceById(rest))
                .Add(">> READ All", () => ReadAllServices(rest))
                .Add(">> UpdateCost", () => UpdateServicecost(rest))
                .Add(">> DELETE", () => DeleteService(rest))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
            var MenuForReservationsServices = new ConsoleMenu()
                .Add(">> CREATE", () => AddNewConnection(rest))
                .Add(">> READ By Id", () => ReadConnectionById(rest))
                .Add(">> READ All", () => ReadAllConnections(rest))
                .Add(">> DELETE", () => DeleteConnection(rest))
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
        #region fansMethods
        private static void AddNewFan(RestService rest)
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

                Fans fan = new Fans() { City = city, Email = email, Name = name, PhoneNumber = phoneNumber };
                rest.Post<Fans>(fan ,"fans");
                
                Console.WriteLine("\n A fan with name "+ name + " has been added to the Database\n");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
        private static void ReadFanById(RestService rest)
        {
            Console.Write("\n ID of Fan :  ");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n{"Id",3} | {"Name",-20} {"Email",-28} {"PhoneNumber",10}  {"City",5}");
                Console.ResetColor();
                Console.WriteLine(rest.Get<Fans>(id,"fans").ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void CountResers(RestService rest)
        {
            Console.Write("Fan's ID : ");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                int coun = rest.Get<int>(id, "Noncrudfan/ReservationNUM");
                Console.WriteLine("This fan has : " + coun + " reservations.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            
            Console.ReadLine();
        }
        private static void ReadAllFans(RestService rest)
        {
            Console.WriteLine("\n   ALL Fans :  \n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n{"Id",3} | {"Name",-20} {"Email",-28} {"PhoneNumber",10} {"City",5}");
            Console.ResetColor();
            var fans =  rest.Get<Fans>("fans");
            fans.ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void UpdateFanCity(RestService rest)
        {
            Console.WriteLine("\n  Fan's ID : \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                
                Console.WriteLine("\n New City : ");
                string city = Console.ReadLine();
                Fans f1 = rest.Get<Fans>(id, "fans");
                f1.City = city;

                rest.Put<Fans>(f1, "fans");


                Console.WriteLine("City Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void DeleteFan(RestService rest)
        {
            Console.WriteLine("\n Fan's ID :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("\n  Fan who will be deleted  has ID : " + id );
                rest.Delete(id, "fans");
                Console.WriteLine("  Fan deleted! ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void BestFan(RestService rest)
        {
            var bestfans = rest.Get<KeyValuePair<int, int>>("Noncrudfan/BestFans");
            foreach (var item in bestfans)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Best Fan Id : " + item.Key + ", Reservations number : " + item.Value);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        private static void WorstFan(RestService rest)
        {
            var worstfans = rest.Get<KeyValuePair<int, int>>("Noncrudfan/WorstFans");
            foreach (var item in worstfans)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Worst Fan Id : " + item.Key + ", Reservations number : " + item.Value);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        #endregion

        #region ArtistsMethods
        private static void AddNewArtist(RestService rest)
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

            rest.Post<Artists>(new Artists() { Name = name, Duration = duration, Price = price, Category = category }, "artists");

            Console.WriteLine("\n An artist with the name  " + name + " has been added to the Database\n");

            Console.ReadLine();
        }
        private static void ReadArtistById(RestService rest)
        {
            Console.WriteLine("\n ID of Artist :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n{"Id",3} |  {"Duration"}  {"Price",10}  {"Category",10} {"Name",15}");
                Console.ResetColor();
                Console.WriteLine(rest.Get<Artists>(id,"artists").ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ReadAllArtists(RestService rest)
        {
            Console.WriteLine("\n   ALL Artists :  \n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n{"Id",3} |  {"Duration"}  {"Price",10}  {"Category",10} {"Name",15}");
            Console.ResetColor();
            var artists = rest.Get<Artists>("artists");
            artists.ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void UpdateArtistcost(RestService rest)
        {
            Console.WriteLine("\n  Artist's ID : \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                
                Console.WriteLine("\n New Cost : ");
                int cost = int.Parse(Console.ReadLine());
                
                Artists art = rest.Get<Artists>(id, "artists");
                art.Price = cost;
                
                rest.Put<Artists>(art,"artists");
                
                
                Console.WriteLine("Cost Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void DeleteArtist(RestService rest)
        {
            Console.WriteLine("\n Artist's ID :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("\n  Artist who will be deleted has ID :  " + id);
                rest.Delete(id, "artists");
                Console.WriteLine("  Artist deleted! ");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void Artistearrings(RestService rest)
        {
            var artistsearnings = rest.Get<KeyValuePair<string, int>>("Noncrudartist/ArtistsEarnings");
            foreach (var item in artistsearnings)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("ARTIST NAME  : " + item.Key + ", OVERALL EARNINGS : " + item.Value);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        private static void MostPaidArt(RestService rest)
        {
            var Mostpaidarti = rest.Get<KeyValuePair<string, int>>("Noncrudartist/Mostpaidart");
            foreach (var item in Mostpaidarti)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("ARTIST NAME  : " + item.Key + ", OVERALL EARNINGS : " + item.Value);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        private static void LessPaidArt(RestService rest)
        {
            var Lesspaidarti = rest.Get<KeyValuePair<string, int>>("Noncrudartist/Lesspaidart");
            foreach (var item in Lesspaidarti)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("ARTIST NAME  : " + item.Key + ", OVERALL EARNINGS : " + item.Value);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        #endregion

        #region ReservationsMethods
        private static void AddNewReservation(RestService rest)
        {
            Console.WriteLine("\n:: New Reservation ::\n");
            

            Console.WriteLine("Fan ID  : ");
            int fanId = int.Parse(Console.ReadLine());

            Console.WriteLine("Artist ID : : ");
            int artistId = int.Parse(Console.ReadLine());

            Console.WriteLine(" Date [yyyy-MM-dd HH:mm] : ");
            DateTime dateTime = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm",null);
            try
            {
                rest.Post<Reservations>(new Reservations() { FanId=fanId,ArtistId=artistId,DateTime=dateTime},"reservations");
                Console.WriteLine("\n A Reservation with For fan with ID " + fanId + " has been added to the Database\n");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ReadReservationById(RestService rest)
        {
            Console.WriteLine("\n ID of Reservation :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n{"Id",3} | {"Fan Id ",-20} {"DateTime",10} {"Artist Id",25}");
                Console.ResetColor();
                var re=rest.Get<Reservations>(id, "reservations");
                Console.WriteLine(re.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ReadAllReservations(RestService rest)
        {
            Console.WriteLine("\n   ALL Reservations :  \n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n{"Id",3} | {"Fan Id ",-20} {"DateTime",10} {"Artist Id",25}");
            Console.ResetColor();
            var reservations = rest.Get<Reservations>("reservations");
            reservations.ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void UpdateReservationdate(RestService rest)
        {
            Console.WriteLine("\n  Reservation's ID : \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                
                Console.WriteLine("\n New Date [yyyy - MM - dd HH: mm] :  ");
                DateTime date = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm", null);
                Reservations r1 = rest.Get<Reservations>(id, "reservations");
                r1.DateTime = date;

                rest.Put<Reservations>(r1, "reservations");
                

                Console.WriteLine("Date Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void DeleteReservation(RestService rest)
        {
            Console.WriteLine("Reservation's ID which will be deleted ");

            int id = int.Parse(Console.ReadLine());
            rest.Delete(id,"reservations");
            Console.WriteLine("  Reservation deleted! ");

            Console.ReadLine();
        }
        #endregion

        #region ServicesMethods
        private static void AddNewService(RestService rest)
        {
            Console.WriteLine("\n:: New Service ::\n");

            Console.WriteLine("Service's name :  : ");
            string name = Console.ReadLine();

            Console.WriteLine("Service's price  : ");
            int price = int.Parse(Console.ReadLine());

            Console.WriteLine("Service's rating : : ");
            int rating = int.Parse(Console.ReadLine());

            rest.Post<Services>(new Services() {Name=name,Price=price,Rating=rating }, "Services");
           

            Console.WriteLine("\n A Service with name  " + name + " has been added to the Database\n");

            Console.ReadLine();
        }
        private static void ReadServiceById(RestService rest)
        {
            Console.WriteLine("\n ID of Service :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n{"Id",3} | {"Rating",2}/10 {"Price",7}  {"Name",10}");
                Console.ResetColor();
                Console.WriteLine(rest.Get<Services>(id,"services").ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ReadAllServices(RestService rest)
        {
            Console.WriteLine("\n   ALL Services :  \n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n{"Id",3} | {"Rating",2}/10 {"Price",7}  {"Name",10}");
            Console.ResetColor();
            var services = rest.Get<Services>("services");
            services.ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void UpdateServicecost(RestService rest)
        {
            Console.WriteLine("\n  Service's ID : \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                
                Console.WriteLine("\n New Cost :  ");
                int cost = int.Parse(Console.ReadLine());
                Services s1 = rest.Get<Services>(id, "Services");
                s1.Price = cost;

                rest.Put<Services>(s1, "Services");
                
                Console.WriteLine("Cost Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void DeleteService(RestService rest)
        {
            Console.WriteLine("Service's ID which will be deleted : ");
            int id = int.Parse(Console.ReadLine());

            rest.Delete(id, "services");
            Console.WriteLine("  Reservation deleted! ");

            Console.ReadLine();
        }
        #endregion

        #region ConnectionsMethods
        private static void AddNewConnection(RestService rest)
        {
            Console.WriteLine("\n:: New Connection ::\n");

            Console.WriteLine("Reservation's ID  : ");
            int reservationId = int.Parse(Console.ReadLine());

            Console.WriteLine("Service's ID: : ");
            int serviceid = int.Parse(Console.ReadLine());

            rest.Post<ReservationsServices>(new ReservationsServices() { ReservationId = reservationId, ServiceId = serviceid }, "Reservationsservices");

            Console.WriteLine("\n A Connection has been added to the Database\n");

            Console.ReadLine();
        }
        private static void ReadConnectionById(RestService rest)
        {
            Console.WriteLine("\n ID of Connection :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{"Id",3} | {"ReservationId",5} {"ServiceId",10}");
                Console.ResetColor();
                Console.WriteLine(rest.Get<ReservationsServices>(id,"reservationsservices").ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ReadAllConnections(RestService rest)
        {
            Console.WriteLine("\n   ALL Connections :  \n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{"Id",3} | {"ReservationId",5}\t {"ServiceId",10}");
            Console.ResetColor();
            var reservationssers = rest.Get<ReservationsServices>("reservationsservices");
            reservationssers.ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        private static void DeleteConnection(RestService rest)
        {
            Console.WriteLine("Connection's ID which will be deleted ");
            int id = int.Parse(Console.ReadLine());

            rest.Delete(id, "reservationsservices");
            Console.WriteLine("  Connection deleted! ");

            Console.ReadLine();
        }
        #endregion



    }
}
