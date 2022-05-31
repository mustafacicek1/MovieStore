﻿using MovieStore.Core.Entities.Abstract;
using MovieStore.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Entities.Dtos
{
    public class MovieDetailDto:IDto
    {
        public MovieDetailDto()
        {
            Actors = new List<string>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public List<string> Actors { get; set; }
    }
}
