namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeInvoiceFKNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.Items", new[] { "InvoiceId" });
            AlterColumn("dbo.Items", "InvoiceId", c => c.Int());
            CreateIndex("dbo.Items", "InvoiceId");
            AddForeignKey("dbo.Items", "InvoiceId", "dbo.Invoices", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.Items", new[] { "InvoiceId" });
            AlterColumn("dbo.Items", "InvoiceId", c => c.Int(nullable: false));
            CreateIndex("dbo.Items", "InvoiceId");
            AddForeignKey("dbo.Items", "InvoiceId", "dbo.Invoices", "Id", cascadeDelete: true);
        }
    }
}
