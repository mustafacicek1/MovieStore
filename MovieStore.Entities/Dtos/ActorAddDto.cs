using MovieStore.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Entities.Dtos
{
    public class ActorAddDto:IDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
