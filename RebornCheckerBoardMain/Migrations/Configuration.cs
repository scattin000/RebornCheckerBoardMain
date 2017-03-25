namespace RebornCheckerBoardMain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using RebornCheckerBoardMain.Models;

    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RebornCheckerBoardMain.Models.TokenCategoryDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "RebornCheckerBoardMain.Models.TokenCategoryDBContext";
        }

        protected override void Seed(RebornCheckerBoardMain.Models.TokenCategoryDBContext context)
        {
            context.Tokens.AddOrUpdate (i => i.ID, 
                new TokenCategory
                {
                    Type = "Game",
                    Content ="Battle Of The Worlds - DLC",
                    Value = 1,
                    ValueLabel = "months",
                    ValueRange = 12,
                    ValueMin = 1
                },
                 new TokenCategory
                 {
                     Type = "Game",
                     Content = "Battle Of The Worlds - Standard Edition",
                     Value = 12,
                     ValueLabel = "months",
                     ValueRange = 12,
                     ValueMin = 1
                 },
                   new TokenCategory
                   {
                       Type = "Subscription",
                       Content = "EducationOnline",
                       Value = 12,
                       ValueLabel = "months",
                       ValueRange = 24,
                       ValueMin = 1
                   },
                   new TokenCategory
                   {
                       Type = "Subscription",
                       Content = "GamersPlus",
                       Value = 3,
                       ValueLabel = "months",
                       ValueRange = 24,
                       ValueMin = 1
                   }
            //  This method will be called aftermigrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
              ); 
        }
    }
}
