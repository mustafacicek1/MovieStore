using MovieStore.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Entities.Dtos
{
    public class MovieUpdateDto:IDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
    }
}
