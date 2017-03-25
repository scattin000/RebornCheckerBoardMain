namespace RebornCheckerBoardMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TokenCategories : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TokenCategories");
            AddColumn("dbo.TokenCategories", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.TokenCategories", "Type", c => c.String());
            AddPrimaryKey("dbo.TokenCategories", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.TokenCategories");
            AlterColumn("dbo.TokenCategories", "Type", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.TokenCategories", "ID");
            AddPrimaryKey("dbo.TokenCategories", "Type");
        }
    }
}
