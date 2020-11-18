namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInvoiceInItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "Invoice_Id", "dbo.Invoices");
            DropIndex("dbo.Items", new[] { "Invoice_Id" });
            RenameColumn(table: "dbo.Items", name: "Invoice_Id", newName: "InvoiceId");
            AlterColumn("dbo.Items", "InvoiceId", c => c.Int(nullable: false));
            CreateIndex("dbo.Items", "InvoiceId");
            AddForeignKey("dbo.Items", "InvoiceId", "dbo.Invoices", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.Items", new[] { "InvoiceId" });
            AlterColumn("dbo.Items", "InvoiceId", c => c.Int());
            RenameColumn(table: "dbo.Items", name: "InvoiceId", newName: "Invoice_Id");
            CreateIndex("dbo.Items", "Invoice_Id");
            AddForeignKey("dbo.Items", "Invoice_Id", "dbo.Invoices", "Id");
        }
    }
}
