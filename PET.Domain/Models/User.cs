using System;
using System.Collections.Generic;

namespace PET.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public ICollection<Animal> Animals { get; set; }
    }
}