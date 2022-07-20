﻿using MovieStore.Core.Entities.Abstract;

namespace MovieStore.Entities.Dtos
{
    public class CustomerRegisterDto:IDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
}
