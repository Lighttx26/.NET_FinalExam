namespace NET_Final.Migrations
{
    using NET_Final.DTO;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NET_Final.Model>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NET_Final.Model context)
        {
            context.Positions.AddRange(new Position[]
            {
                new Position{ PositionName = "Quan ly", PositionRate = 2 },
                new Position{ PositionName = "Ke toan", PositionRate = 1.2 },
                new Position{ PositionName = "Truong phong", PositionRate = 1.5 },
                new Position{ PositionName = "Nhan vien", PositionRate = 1 },
            });
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
