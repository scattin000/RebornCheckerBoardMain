namespace RebornCheckerBoardMain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TokenCategories",
                c => new
                    {
                        Type = c.String(nullable: false, maxLength: 128),
                        Content = c.String(),
                        Value = c.Int(nullable: false),
                        ValueLabel = c.String(),
                        ValueRange = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Type);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TokenCategories");
        }
    }
}
