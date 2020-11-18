namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyAnnotationToRecipientName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "RecipientName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "RecipientName", c => c.String());
        }
    }
}
