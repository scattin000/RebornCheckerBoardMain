namespace RebornCheckerBoardMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TokenCategories1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TokenCategories", "ValueMin", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TokenCategories", "ValueMin");
        }
    }
}
