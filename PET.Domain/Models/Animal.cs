using System;
using System.Collections.Generic;

namespace PET.Domain.Models
{
    public class Animal
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public DateTime BDate { get; set; }

        public string AnimalType { get; set; }

        public Sex Sex { get; set; }

        public ICollection<File> Files { get; set; }
    }
}
