using System;
using System.Collections.Generic;

namespace PET.Application.DTOs
{
    public class AnimalSpecDto
    {
        public IEnumerable<string> Kinds { get; set; }

        public DateTime? BDateFrom { get; set; }

        public DateTime? BDateTo { get; set; }

        public SexM? Sex { get; set; }
    }
}