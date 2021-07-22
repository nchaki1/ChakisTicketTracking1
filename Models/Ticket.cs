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
        [Display(Name ="Request Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime RequestDate { get; set; }

        // Description of user request
        [Display(Name ="Request Description")]
        [MinLength(10)]
        public string TicketDescription { get; set; }

        // Navigation property
        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}