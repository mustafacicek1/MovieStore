using MovieStore.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Entities.Dtos
{
    public class CustomerLoginDto:IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
