namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameTaxProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "TaxPercentage", c => c.Double(nullable: false));
            DropColumn("dbo.Invoices", "Tax");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "Tax", c => c.Double(nullable: false));
            DropColumn("dbo.Invoices", "TaxPercentage");
        }
    }
}
