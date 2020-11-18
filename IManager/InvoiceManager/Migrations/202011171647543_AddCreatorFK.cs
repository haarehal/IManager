namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatorFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "CreatorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Invoices", "CreatorId");
            AddForeignKey("dbo.Invoices", "CreatorId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "CreatorId", "dbo.AspNetUsers");
            DropIndex("dbo.Invoices", new[] { "CreatorId" });
            DropColumn("dbo.Invoices", "CreatorId");
        }
    }
}
