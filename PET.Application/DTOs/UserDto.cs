using System;
using System.Collections.Generic;
using PET.Domain.Models;

namespace PET.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public ICollection<Animal> Animals { get; set; }

    }
}