namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookstoreUpd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "Amount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "Amount");
        }
    }
}
