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
    public class NoncrudfanController : ControllerBase
    {
        IFansLogic FL;

        public NoncrudfanController(IFansLogic fL)
        {
            FL = fL;
        }

        // GET: Noncrudfan/ReservationNUM/id
        [HttpGet("{id}")]
        public int ReservationNUM(int id)
        {
            return FL.ReservationsNumber(id);
        }

        // GET: Noncrudfan/BestFans
        [HttpGet]
        public List<KeyValuePair<int, int>> BestFans()
        {
            return FL.BestFan();
        }

        // GET: Noncrudfan/WorstFans
        [HttpGet]
        public List<KeyValuePair<int, int>> WorstFans()
        {
            return FL.WorstFan();
        }
        
    }
}
