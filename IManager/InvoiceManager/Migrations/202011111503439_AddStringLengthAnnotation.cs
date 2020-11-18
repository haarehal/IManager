namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStringLengthAnnotation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "RecipientName", c => c.String(maxLength: 20));
            AlterColumn("dbo.Items", "Description", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Items", "Description", c => c.String());
            AlterColumn("dbo.Invoices", "RecipientName", c => c.String(maxLength: 50));
        }
    }
}
