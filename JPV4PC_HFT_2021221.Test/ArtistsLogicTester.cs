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
    public class ArtistsLogicTester
    {
        ArtistsLogic AL;
        [SetUp]
        public void Init()
        {
            var MockArtistRepository = new Mock<IArtistsRepository>();
            var MockReservationsRepository = new Mock<IReservationsRepository>();
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
            MockArtistRepository.Setup((t) => t.GetAll()).Returns(Artists);
            MockReservationsRepository.Setup((t) => t.GetAll()).Returns(Reservations);
            for (int i = 0; i < 5; i++)
            {
                MockArtistRepository.Setup((t) => t.GetOne(i + 1)).Returns(Artists.ToList()[i]);
            }
            AL = new ArtistsLogic(MockArtistRepository.Object, MockReservationsRepository.Object);
        }
        [Test]
        public void AddNewArtistTest()
        {
            //Act
            Artists artist6 = AL.AddNewArtist("artist6", 1, 600, "c6");
            //Arrange
            Assert.That(artist6.Name, Is.EqualTo("artist6"));
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
            var expected = new List<KeyValuePair<string, int>>() { new KeyValuePair<string, int>("artist5", 500)};
            //assert
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void LessPaidArtistTest()
        {
            //act
            var result = AL.LessPaidArtist();
            var expected = new List<KeyValuePair<string, int>>() { new KeyValuePair<string, int>("artist1", 100) };
            //assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
