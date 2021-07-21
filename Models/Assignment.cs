using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChakisTicketTracking1.Models
{
    public class Assignment
    {
        // Identifier for the assignment
        public int AssignmentID { get; set; }
        // From Tech class
        public int TicketID { get; set; }
        // From Ticket class
        public int TechID { get; set; }

        // Time and date of completion, if applicable
        [Display(Name ="Date Completed")]
        [DataType(DataType.DateTime)]
        public DateTime? CompletionDate { get; set; }
        // Tech notes on the service ticket (can be an empty string if no notes)
        public string Notes { get; set; }

        // Navigation properties
        public virtual Ticket Ticket { get; set; }
        public virtual Tech Tech { get; set; }
    }
}