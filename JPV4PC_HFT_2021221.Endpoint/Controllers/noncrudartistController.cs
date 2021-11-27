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
    public class noncrudartistController: ControllerBase
    {
        IArtistsLogic Al;

        public noncrudartistController(IArtistsLogic al)
        {
            Al = al;
        }


        // GET: Noncrudartist/ArtistsEarnings
        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> ArtistsEarnings()
        {
            return Al.ArtistEarnings();
        }


        // GET: Noncrudartist/Mostpaidart
        [HttpGet]
        public List<KeyValuePair<string, int>> Mostpaidart()
        {
            return Al.MostPaidArtist();
        }


        // GET: Noncrudartist/Lesspaidart
        [HttpGet]
        public List<KeyValuePair<string, int>> Lesspaidart()
        {
            return Al.LessPaidArtist();
        }
    }
}
