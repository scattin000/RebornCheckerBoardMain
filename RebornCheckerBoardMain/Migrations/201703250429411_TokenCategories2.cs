namespace RebornCheckerBoardMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TokenCategories2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TokenCategories", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.TokenCategories", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.TokenCategories", "ValueLabel", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TokenCategories", "ValueLabel", c => c.String());
            AlterColumn("dbo.TokenCategories", "Content", c => c.String());
            AlterColumn("dbo.TokenCategories", "Type", c => c.String());
        }
    }
}
