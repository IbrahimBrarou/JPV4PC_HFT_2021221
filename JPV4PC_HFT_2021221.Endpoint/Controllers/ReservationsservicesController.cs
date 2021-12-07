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
    public class ReservationsservicesController: ControllerBase
    {
        IReservationsServicesLogic RSL;

        public ReservationsservicesController(IReservationsServicesLogic rSL)
        {
            RSL = rSL;
        }
        // GET: /reservationsservices
        [HttpGet]
        public IEnumerable<ReservationsServices> Get()
        {
            return RSL.GetAllConnections();
        }


        // GET /reservationsservices/5
        [HttpGet("{id}")]
        public ReservationsServices Get(int id)
        {
            return RSL.GetConnection(id);
        }

        // POST /reservationsservices
        [HttpPost]
        public void Post([FromBody] ReservationsServices value)
        {
            RSL.AddNewConnection((int)value.ReservationId, (int)value.ServiceId);
        }
        


        // DELETE /reservationsservices/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            RSL.DeleteConnection(id);
        }
    }
}
