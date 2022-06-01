﻿using MovieStore.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Entities.Dtos
{
    public class OrdersDto:IDto
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string Movie { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
