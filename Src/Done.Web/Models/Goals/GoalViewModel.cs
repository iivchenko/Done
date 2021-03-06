﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Done.Domain;
using System;

namespace Done.Web.Models.Goals
{
    public sealed class GoalViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        // TODO: Extract view state and Use automapper
        public State State { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}