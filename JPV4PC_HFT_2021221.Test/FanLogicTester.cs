using JPV4PC_HFT_2021221.Logic;
using JPV4PC_HFT_2021221.Models;
using JPV4PC_HFT_2021221.Repository;
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
    public class FanLogicTester
    {
        FanLogic FL;
        [SetUp]
        public void Init()
        {
            var MockFanRepository = new Mock<IFansRepository>();
            var fans = new List<Fans>()
            {
                new Fans(){ Id =1,City="Budapest1",Email="fan1@gmail.com",Name="fan1",PhoneNumber=11111111},
                new Fans(){Id =2,City="Budapest2",Email="fan2@gmail.com",Name="fan2",PhoneNumber=22222222},
                new Fans(){Id =3,City="Budapest3",Email="fan3@gmail.com",Name="fan3",PhoneNumber=33333333},
                new Fans(){Id =4,City="Budapest4",Email="fan4@gmail.com",Name="fan4",PhoneNumber=44444444},
                new Fans(){Id =5,City="Budapest5",Email="fan5@gmail.com",Name="fan5",PhoneNumber=55555555}
            }.AsQueryable();
            MockFanRepository.Setup((t) => t.GetAll()).Returns(fans);
            FL = new FanLogic(MockFanRepository.Object);
        }
        [Test]
        public void AddNewFanTest_Throws()
        {
            //Arrange
            Assert.Throws<ArgumentException>(() => FL.AddNewFan("budapest6", "fan6@gmail.com", null, 66666666));
        }
        [Test]
        public void DeleteFanTest_Throws()
        {
            //Arrange
            Assert.Throws<ArgumentException>(() => FL.DeleteFan(100));
        }

    }
}
