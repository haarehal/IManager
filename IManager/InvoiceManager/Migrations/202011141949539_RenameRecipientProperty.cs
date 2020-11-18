namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameRecipientProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Recipient", c => c.String(maxLength: 20));
            DropColumn("dbo.Invoices", "RecipientName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "RecipientName", c => c.String(maxLength: 20));
            DropColumn("dbo.Invoices", "Recipient");
        }
    }
}
