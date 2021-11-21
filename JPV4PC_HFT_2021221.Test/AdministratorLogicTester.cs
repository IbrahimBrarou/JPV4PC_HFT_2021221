using JPV4PC_HFT_2021221.Logic;
using JPV4PC_HFT_2021221.Repository;
using JPV4PC_HFT_2021221.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Test
{
    [TestFixture]
    public class AdministratorLogicTester
    {

        AdministratorLogic AL;

        [SetUp]
        public void Init()
        {
            var MockArtistRepository = new Mock<IArtistsRepository>();
            var MockFanRepository = new Mock<IFansRepository>();
            var MockReservationsRepository = new Mock<IReservationsRepository>();
            var MockServicesRepository = new Mock<IServicesRepository>();
            var mockConRepository = new Mock<IReservationsServicesRepository>();

            var Artists = new List<Artists>()
            {
                new Artists(){Id=1,Name="artist1",Category="c1",Price=100,Duration=1 },
                new Artists(){Id=2,Name="artist2",Category="c2",Price=200,Duration=1 },
                new Artists(){Id=3,Name="artist3",Category="c3",Price=300,Duration=1 },
                new Artists(){Id=4,Name="artist4",Category="c4",Price=400,Duration=1 },
                new Artists(){Id=5,Name="artist5",Category="c5",Price=500,Duration=1 }
            }.AsQueryable();
            var Reservations = new List<Reservations>()
            {
                new Reservations(){Id = 1 , FanId=5,ArtistId=4,DateTime=new DateTime(2021,11,21) },
                new Reservations(){Id = 2 , FanId=2,ArtistId=5,DateTime=new DateTime(2021,11,22) },
                new Reservations(){Id = 3 , FanId=2,ArtistId=2,DateTime=new DateTime(2021,11,23) },
                new Reservations(){Id = 4 , FanId=1,ArtistId=3,DateTime=new DateTime(2021,11,29) },
                new Reservations(){Id = 5 , FanId=1,ArtistId=1,DateTime=new DateTime(2021,11,20) }
            }.AsQueryable();
            var fans = new List<Fans>()
            {
                new Fans(){ Id =1,City="Budapest1",Email="fan1@gmail.com",Name="fan1",PhoneNumber=11111111},
                new Fans(){Id =2,City="Budapest2",Email="fan2@gmail.com",Name="fan2",PhoneNumber=22222222},
                new Fans(){Id =3,City="Budapest3",Email="fan3@gmail.com",Name="fan3",PhoneNumber=33333333},
                new Fans(){Id =4,City="Budapest4",Email="fan4@gmail.com",Name="fan4",PhoneNumber=44444444},
                new Fans(){Id =5,City="Budapest5",Email="fan5@gmail.com",Name="fan5",PhoneNumber=55555555}
            }.AsQueryable();
            var Services = new List<Services>()
            {
                new Services(){Id=1,Name="service1",Price=1,Rating=2},
                new Services(){Id=2,Name="service2",Price=2,Rating=3},
                new Services(){Id=3,Name="service3",Price=3,Rating=4},
                new Services(){Id=4,Name="service4",Price=4,Rating=5},
                new Services(){Id=5,Name="service5",Price=5,Rating=6},
            }.AsQueryable();
            var Connection = new List<ReservationsServices>()
            {
                new ReservationsServices{Id= 1,ReservationId=1,ServiceId=1},
                new ReservationsServices{Id= 2,ReservationId=1,ServiceId=2},
                new ReservationsServices{Id= 3,ReservationId=1,ServiceId=3},
                new ReservationsServices{Id= 4,ReservationId=2,ServiceId=5},
                new ReservationsServices{Id= 5,ReservationId=2,ServiceId=4},
                new ReservationsServices{Id= 6,ReservationId=2,ServiceId=3},
                new ReservationsServices{Id= 7,ReservationId=3,ServiceId=1},
                new ReservationsServices{Id= 8,ReservationId=3,ServiceId=3},
                new ReservationsServices{Id= 9,ReservationId=3,ServiceId=2},
                new ReservationsServices{Id= 10,ReservationId=4,ServiceId=5},
                new ReservationsServices{Id= 11,ReservationId=4,ServiceId=2},
                new ReservationsServices{Id= 12,ReservationId=4,ServiceId=3},
                new ReservationsServices{Id= 13,ReservationId=5,ServiceId=1},
                new ReservationsServices{Id= 14,ReservationId=5,ServiceId=5},
                new ReservationsServices{Id= 15,ReservationId=5,ServiceId=3}
            }.AsQueryable();


            MockFanRepository.Setup((t) => t.GetAll()).Returns(fans);
            MockArtistRepository.Setup((t) => t.GetAll()).Returns(Artists);
            MockReservationsRepository.Setup((t) => t.GetAll()).Returns(Reservations);
            MockServicesRepository.Setup((t) => t.GetAll()).Returns(Services);
            mockConRepository.Setup((t) => t.GetAll()).Returns(Connection);
            for (int i = 0; i < 5; i++)
            {
                MockArtistRepository.Setup((t) => t.GetOne(i+1)).Returns(Artists.ToList()[i]);
            }
            for (int i = 0; i < 5; i++)
            {
                MockFanRepository.Setup((t) => t.GetOne(i + 1)).Returns(fans.ToList()[i]);
            }
            AL = new AdministratorLogic(MockServicesRepository.Object, MockReservationsRepository.Object, MockFanRepository.Object,
                MockArtistRepository.Object, mockConRepository.Object);
        }

        //Tests for crud functions
        [Test]
        public void AddServiceTest_Throws()
        {
            //Arrange
            Assert.Throws<ArgumentException>(() => AL.AddNewService(null, 100,2));
        }
        [Test]
        public void GetArtistTest()
        {
            
            //ACT
            var result = this.AL.GetArtist(1).Name;
            var expected = "artist1";
            //assert
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void DeleteArtistTest_Throws()
        {
            //Arrange
            Assert.Throws<ArgumentException>(() => AL.DeleteArtist(100));
        }
        [Test]
        public void DeleteServiceTest_Throws()
        {
            //Arrange
            Assert.Throws<Exception>(() => AL.DeleteService(100));
        }
        [Test]
        public void GetArtistTest_Throws()
        {
            //Arrange
            Assert.Throws<Exception>(() => AL.GetArtist(100));
        }

        //Tests for Non-crud functions
        [Test]
        public void MostPaidArtistTest()
        {
            //act
            var result = AL.MostPaidArtist();
            var expected = new KeyValuePair<string, int>("artist5", 500);
            //assert
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void LessPaidArtistTest()
        {
            //act
            var result = AL.LessPaidArtist();
            var expected = new KeyValuePair<string, int>("artist1", 100);
            //assert
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void BestFan()
        {
            //act 
            var result = AL.BestFan();
            var expected = new KeyValuePair<string, int>("fan1",2);

            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void WorstFan()
        {
            //act 
            var result = AL.WorstFan();
            var expected = new KeyValuePair<string, int>("fan5", 1);

            Assert.That(result, Is.EqualTo(expected));

        }


    }
}
