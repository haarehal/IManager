namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetDateFinalNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "DateFinal", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "DateFinal", c => c.DateTime(nullable: false));
        }
    }
}
