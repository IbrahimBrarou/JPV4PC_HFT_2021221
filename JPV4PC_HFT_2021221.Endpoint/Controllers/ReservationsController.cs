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
    public class ReservationsController : ControllerBase
    {
        IReservationsLogic RL;
        public ReservationsController(IReservationsLogic rL)
        {
            RL = rL;
        }


        // GET: /reservations
        [HttpGet]
        public IEnumerable<Reservations> Get()
        {
            return RL.GetAllReservations();
        }


        // GET /resevations/5
        [HttpGet("{id}")]
        public Reservations Get(int id)
        {
            return RL.GetReservation(id);
        }

        // POST /reservations
        [HttpPost]
        public void Post([FromBody] Reservations value)
        {
            RL.AddNewReservation(value);
        }


        // PUT /reservations
        [HttpPut]
        public void Put([FromBody] Reservations value)
        {
            RL.UpdateReservationDate(value);
        }


        // DELETE /reservations/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            RL.DeleteReservation(id);
        }
    }
}
