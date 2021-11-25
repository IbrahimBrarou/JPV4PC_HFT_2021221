using JPV4PC_HFT_2021221.Logic;
using JPV4PC_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReservationsServicesController : ControllerBase
    {
        IReservationsServicesLogic RSL;

        public ReservationsServicesController(IReservationsServicesLogic rSL)
        {
            RSL = rSL;
        }
        // GET: /ReservationsServices
        [HttpGet]
        public IEnumerable<ReservationsServices> Get()
        {
            return RSL.GetAllConnections();
        }


        // GET /ReservationsServices/5
        [HttpGet("{id}")]
        public ReservationsServices Get(int id)
        {
            return RSL.GetConnection(id); ;
        }



        // DELETE /ReservationsServices/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            RSL.DeleteConnection(id);
        }
    }
}
