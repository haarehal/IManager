namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaxProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Tax", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "Tax");
        }
    }
}
