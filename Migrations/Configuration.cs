namespace ChakisTicketTracking1.Migrations
{
    using ChakisTicketTracking1.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ChakisTicketTracking1.DAL.HelpDeskContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ChakisTicketTracking1.DAL.HelpDeskContext context)
        {
            // Update techs in the system
            var techs = new List<Tech>
            {
                new Tech{FirstMidName = "Wesley", LastName = "Walker"},
                new Tech{FirstMidName = "Nick", LastName = "Mangold"},
                new Tech{FirstMidName = "Darrelle", LastName = "Revis"},
                new Tech{FirstMidName = "Kevin", LastName = "Mawae"},
                new Tech{FirstMidName = "Ryan", LastName = "Fitzpatrick"},
                new Tech{FirstMidName = "Curtis", LastName = "Martin"}
            };

            // Add to database and save
            techs.ForEach(t => context.Techs.AddOrUpdate(p => p.LastName, t));
            context.SaveChanges();

            // Update tickets in the system
            var tickets = new List<Ticket>
            {
                new Ticket{RequestDate = DateTime.Parse("2021-07-02 10:35 AM"), TicketDescription = "The copier is broken!"},
                new Ticket{RequestDate = DateTime.Parse("2021-07-05 05:00 PM"), TicketDescription = "Missing document from shared drive."},
                new Ticket{RequestDate = DateTime.Parse("2021-07-15 02:32 PM"), TicketDescription = "Phishing email?"},
                new Ticket{RequestDate = DateTime.Parse("2021-06-24 06:53 AM"), TicketDescription = "Link does not work."},
                new Ticket{RequestDate = DateTime.Parse("2021-07-06 03:23 PM"), TicketDescription = "Password reset."},
                new Ticket{RequestDate = DateTime.Parse("2021-07-11 12:57 PM"), TicketDescription = "We would like a new smart TV."},
                new Ticket{RequestDate = DateTime.Parse("2021-07-14 09:00 AM"), TicketDescription = "Please add Jane Smith to Cool People email group."},
                new Ticket{RequestDate = DateTime.Parse("2021-07-13 11:14 AM"), TicketDescription = "Create accounts for new employees starting Monday (names emailed to Curtis)."},
                new Ticket{RequestDate = DateTime.Parse("2021-07-10 09:03 AM"), TicketDescription = "Need access to MS Access."},
                new Ticket{RequestDate = DateTime.Parse("2021-05-28 12:11 PM"), TicketDescription = "Please collect unused cables from old phone system."},
                new Ticket{RequestDate = DateTime.Parse("2021-06-22 03:53 PM"), TicketDescription = "Internet seems slow in downstairs offices."},
                new Ticket{RequestDate = DateTime.Parse("2021-03-01 02:53 PM"), TicketDescription = "UPS battery replacement light is blinking in John's office."},
                new Ticket{RequestDate = DateTime.Parse("2021-06-29 07:08 AM"), TicketDescription = "Website is loading a blank screen?"}
            };

            // Add them to the database and save
            tickets.ForEach(t => context.Tickets.AddOrUpdate(p => p.TicketDescription, t));
            context.SaveChanges();

            // Initial values for which techs are assigned which tickets
            var assignments = new List<Assignment>
            {
                new Assignment{TechID = 1, TicketID = 13, CompletionDate = DateTime.Parse("2021-07-01 02:53 PM"), Notes = "Content filter was preventing loading. URL added to exclusion list."},
                new Assignment{TechID = 2, TicketID = 12, CompletionDate = null, Notes = "John's office is locked."},
                new Assignment{TechID = 1, TicketID = 11, CompletionDate = null, Notes = "Troubleshooting possible causes."},
                new Assignment{TechID = 2, TicketID = 10, CompletionDate = DateTime.Parse("2021-05-31 04:15 PM"), Notes = "Cables were discarded as both ends were frayed."},
                new Assignment{TechID = 6, TicketID = 9, CompletionDate = DateTime.Parse("2021-07-15 07:29 AM"), Notes = "Software installed and user account created."},
                new Assignment{TechID = 6, TicketID = 8, CompletionDate = null, Notes = "Email has not been sent."},
                new Assignment{TechID = 5, TicketID = 7, CompletionDate = DateTime.Parse("2021-07-14 03:05 PM"), Notes = "User added to email group."},
                new Assignment{TechID = 3, TicketID = 6, CompletionDate = null, Notes = "No TVs left in equipment room. Will have to reevaluate when next budget year starts."},
                new Assignment{TechID = 6, TicketID = 5, CompletionDate = DateTime.Parse("2021-07-08 12:45 PM"), Notes = "Walked user through process of resetting password."},
                new Assignment{TechID = 4, TicketID = 4, CompletionDate = DateTime.Parse("2021-06-28 04:57 PM"), Notes = "Reached out to SaaS provider and received an alternate link."},
                new Assignment{TechID = 5, TicketID = 3, CompletionDate = DateTime.Parse("2021-07-16 06:47 PM"), Notes = "Email confirmed to be legitimate."},
                new Assignment{TechID = 4, TicketID = 2, CompletionDate = DateTime.Parse("2021-07-15 01:11 PM"), Notes = "Was unable to recover document. Appears to have been deleted prior to our earliest available back up."},
                new Assignment{TechID = 3, TicketID = 1, CompletionDate = DateTime.Parse("2021-07-06 03:15 PM"), Notes = "Restarted copier and issue was resolved."}
            };

            foreach (Assignment a in assignments)
            {
                var assignmentInDataBase = context.Assignments.Where(
                    t =>
                        t.Tech.TechID == a.TechID &&
                        t.Ticket.TicketID == a.TicketID).SingleOrDefault();
                if (assignmentInDataBase == null)
                {
                    context.Assignments.Add(a);
                }
            }

            context.SaveChanges();
        }
    }
    
}
