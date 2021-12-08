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
    public class ArtistsController : ControllerBase
    {
        IArtistsLogic AL;
        public ArtistsController(IArtistsLogic aL)
        {
            AL = aL;
        }

        // GET: /artists
        [HttpGet]
        public IEnumerable<Artists> Get()
        {
            return AL.GetAllArtists();
        }


        // GET /artists/5
        [HttpGet("{id}")]
        public Artists Get(int id)
        {
            return AL.GetArtist(id);
        }

        // POST /artists
        [HttpPost]
        public void Post([FromBody] Artists value)
        {
            AL.AddNewArtist(value);
        }


        // PUT /artists
        [HttpPut]
        public void Put([FromBody] Artists value)
        {
            AL.UpdateArtistCost(value);
        }


        // DELETE /artists/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            AL.DeleteArtist(id);
        }

    }
}
