namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePropertyNames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Subtotal", c => c.Double(nullable: false));
            AddColumn("dbo.Invoices", "Total", c => c.Double(nullable: false));
            AddColumn("dbo.Items", "Amount", c => c.Double(nullable: false));
            DropColumn("dbo.Invoices", "TotalPrice");
            DropColumn("dbo.Invoices", "TotalPriceWithTax");
            DropColumn("dbo.Items", "TotalPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "TotalPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Invoices", "TotalPriceWithTax", c => c.Double(nullable: false));
            AddColumn("dbo.Invoices", "TotalPrice", c => c.Double(nullable: false));
            DropColumn("dbo.Items", "Amount");
            DropColumn("dbo.Invoices", "Total");
            DropColumn("dbo.Invoices", "Subtotal");
        }
    }
}
