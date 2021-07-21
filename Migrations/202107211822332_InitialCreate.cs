namespace ChakisTicketTracking1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignment",
                c => new
                    {
                        AssignmentID = c.Int(nullable: false, identity: true),
                        TicketID = c.Int(nullable: false),
                        TechID = c.Int(nullable: false),
                        CompletionDate = c.DateTime(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.AssignmentID)
                .ForeignKey("dbo.Tech", t => t.TechID, cascadeDelete: true)
                .ForeignKey("dbo.Ticket", t => t.TicketID, cascadeDelete: true)
                .Index(t => t.TicketID)
                .Index(t => t.TechID);
            
            CreateTable(
                "dbo.Tech",
                c => new
                    {
                        TechID = c.Int(nullable: false, identity: true),
                        LastName = c.String(maxLength: 50),
                        FirstMidName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.TechID);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        TicketID = c.Int(nullable: false, identity: true),
                        RequestDate = c.DateTime(nullable: false),
                        TicketDescription = c.String(),
                    })
                .PrimaryKey(t => t.TicketID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignment", "TicketID", "dbo.Ticket");
            DropForeignKey("dbo.Assignment", "TechID", "dbo.Tech");
            DropIndex("dbo.Assignment", new[] { "TechID" });
            DropIndex("dbo.Assignment", new[] { "TicketID" });
            DropTable("dbo.Ticket");
            DropTable("dbo.Tech");
            DropTable("dbo.Assignment");
        }
    }
}
