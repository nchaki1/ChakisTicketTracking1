using ChakisTicketTracking1.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ChakisTicketTracking1.DAL
{
    public class HelpDeskContext : DbContext
    {
        public HelpDeskContext() : base("HelpDeskContext")
        {
        }

        public DbSet<Tech> Techs { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}