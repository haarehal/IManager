namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResetIdCounterInTables : DbMigration
    {
        public override void Up()
        {
            Sql(@"DBCC CHECKIDENT ('Invoices', RESEED, 0);
                GO ");

            Sql(@"DBCC CHECKIDENT ('Items', RESEED, 0);
                GO ");
        }
        
        public override void Down()
        {
        }
    }
}
