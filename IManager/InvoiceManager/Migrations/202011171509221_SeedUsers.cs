namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7b241eb0-9bac-409c-8735-9a40f75e6a58', N'ivonamarkovic@imanager.com', 0, N'AAEUUFYUmIYVYt1flgd2hOuUNqr0JDE5hX8LwY9wGcTOYhG9TkCQ5azGPPstRjYhMQ==', N'72eb5795-1d80-47d0-9948-37918530fb97', NULL, 0, 0, NULL, 1, 0, N'ivonamarkovic@imanager.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'859fe17b-db1a-424c-8835-17be126c9bbf', N'harisalikadic@imanager.com', 0, N'AAT7ImEp08DA5CG2SJfr4M65lYZfiyodnWrLLCf694N1ToOa09QmX1PEvPcXOm3f/A==', N'5e54b275-3c3e-40eb-806a-2436900b800a', NULL, 0, 0, NULL, 1, 0, N'harisalikadic@imanager.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ae7086f0-fceb-450e-aa95-a135a8d1d7cd', N'guest@imanager.com', 0, N'AD7jvkyGuCI4HDqLZ5+gQNnpp+mW7/yaK2nYuquQiceWEechyawIlioDc7p6bWgjow==', N'b972cb24-1dbe-4476-a72f-8c7dbead3dca', NULL, 0, 0, NULL, 1, 0, N'guest@imanager.com')
                
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'f9e3c674-f137-435f-8c74-d3884cb814fa', N'CanManageInvoices')
                
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7b241eb0-9bac-409c-8735-9a40f75e6a58', N'f9e3c674-f137-435f-8c74-d3884cb814fa')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'859fe17b-db1a-424c-8835-17be126c9bbf', N'f9e3c674-f137-435f-8c74-d3884cb814fa')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
