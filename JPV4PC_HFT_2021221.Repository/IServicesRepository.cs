﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPV4PC_HFT_2021221.Models;

namespace JPV4PC_HFT_2021221.Repository
{
    public interface IServicesRepository : IRepository<Services>
    {
        void UpdatePrice(int id, int newPrice);
        void UpdateName(int id, string newName);
        void UpdateRating(int id, int newRating);


    }
}