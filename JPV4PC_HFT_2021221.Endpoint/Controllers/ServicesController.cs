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
    public class ServicesController :ControllerBase
    {
        IServicesLogic SL;
        public ServicesController(IServicesLogic sL)
        {
            SL = sL;
        }

        // GET: /services
        [HttpGet]
        public IEnumerable<Services> Get()
        {
            return SL.GetAllServices();
        }


        // GET /services/5
        [HttpGet("{id}")]
        public Services Get(int id)
        {
            return SL.GetService(id);
        }

        // POST /services
        [HttpPost]
        public void Post([FromBody] Services value)
        {
            SL.AddNewService(value.Name, value.Price, value.Rating);
        }


        // PUT /services
        [HttpPut]
        public void Put([FromBody] Services value)
        {
            SL.UpdateServiceCost(value.Id, value.Price);
        }


        // DELETE /services/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            SL.DeleteService(id);
        }
    }
}
