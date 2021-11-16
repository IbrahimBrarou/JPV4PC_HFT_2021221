using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Logic
{
    public class ArtistEarnings
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public int FinishedJobs { get; set; }
        public int OverallEarnings { get; set; }
        public override string ToString()
        {
            return "Artist's name : " + this.ArtistName + ", ID : " + ArtistId + ",FinishedJobs : " + FinishedJobs + ",OverallEarnings : " + OverallEarnings;
        }




    }
}
