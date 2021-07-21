using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChakisTicketTracking1.Models
{
    public class Tech
    {
        // Primary key for Tech table
        public int TechID { get; set; }

        // Last name of tech
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        // First name of tech
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstMidName { get; set; }

        // Navigation property
        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}