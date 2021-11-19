﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPV4PC_HFT_2021221.Logic
{
    public class FanRating
    {
        public int FanID { get; set; }
        public string FanName { get; set; }
        public int NumberOfReservations { get; set; }
        public override string ToString()
        {
            return "Fan's name : "+FanName+", ID : "+FanID+", Number of Reservations : "+NumberOfReservations;
        }



    }
}