using JPV4PC_HFT_2021221.Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NoncrudforfanController : ControllerBase
    {
        IFansLogic FL;

        public NoncrudforfanController(IFansLogic fL)
        {
            FL = fL;
        }
        // GET: Noncrudforfan/Best
        [HttpGet]
        public List<KeyValuePair<int, int>> Best()
        {
            return FL.BestFan();
        }

        // GET: Noncrudforfan/worse
        [HttpGet]
        public List<KeyValuePair<int, int>> Worse()
        {
            return FL.WorstFan();
        }

        // GET: Noncrudforfan/Reservationsum
        [HttpGet("{id}")]
        public int Reservationsum(int id)
        {
            return FL.ReservationsNumber(id);
        }
    }
}
