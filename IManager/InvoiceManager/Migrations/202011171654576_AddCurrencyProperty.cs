namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCurrencyProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Currency", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "Currency");
        }
    }
}
