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
    public class ServicesController: ControllerBase
    {
        IServicesLogic SL;
        public ServicesController(IServicesLogic sL)
        {
            SL = sL;
        }
        // GET: /Services
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



        // DELETE /Services/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            SL.DeleteService(id);
        }
    }
}
