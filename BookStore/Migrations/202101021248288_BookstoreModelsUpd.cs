namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookstoreModelsUpd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Purchases", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Purchases", "Address", c => c.String());
        }
    }
}
