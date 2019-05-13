using System;
using System.Collections.Generic;
using System.Text;

namespace PET.Application.DTOs
{
    public enum SexM
    {
        Male,
        Female
    }

    public class AnimalDto
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public DateTime BDate { get; set; }

        public string AnimalType { get; set; }

        public SexM Sex { get; set; }

        public IEnumerable<string> WayToFiles { get; set; }
    }
}
