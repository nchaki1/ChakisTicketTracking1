using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace ChakisTicketTracking1.DAL
{
    public class HelpDeskConfiguration : DbConfiguration
    {
        public HelpDeskConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}