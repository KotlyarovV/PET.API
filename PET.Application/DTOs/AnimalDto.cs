﻿using System;
using System.Collections.Generic;
using System.Text;
using PET.Domain.Models;

namespace PET.Application.DTOs
{
    public class AnimalDto
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Kind { get; set; }

        public bool Sterilization { get; set; }

        public bool Vaccination { get; set; }

        public bool Passport { get; set; }

        public string Name { get; set; }

        public DateTime BDate { get; set; }

        public string AnimalType { get; set; }

        public SexM Sex { get; set; }

        public IEnumerable<string> WayToFiles { get; set; }

        public Guid OwnerId { get; set; }
    }
}
