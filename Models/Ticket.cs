using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChakisTicketTracking1.Models
{
    public class Ticket
    {
        // Primary key for Ticket table
        public int TicketID { get; set; }

        // Date and time of service request
        [DataType(DataType.DateTime)]
        public DateTime RequestDate { get; set; }

        // Description of user request
        [MinLength(10)]
        public string TicketDescription { get; set; }

        // Navigation property
        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}