﻿using BuyandRentHomeWebAPI.Data.Entities;
using System;

namespace BuyandRentHomeWebAPI.Dtos
{
    public class VisitingRequestCreateDto
    {

        public int PropertyId { get; set; }

        public DateTime DateOn { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string ContactNumber { get; set; }
    }
}