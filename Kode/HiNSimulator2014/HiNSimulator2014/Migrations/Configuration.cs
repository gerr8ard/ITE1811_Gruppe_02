namespace HiNSimulator2014.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using HiNSimulator2014.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<HiNSimulator2014.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HiNSimulator2014.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            
            var loc1 = new Location{LocationType = "Datarom", ShortDescription = "D3330", LongDescription = "Linuxlabben"};
            var loc2 = new Location{LocationType = "Grupperom", ShortDescription = "C1070", LongDescription = "HiN IL kontor"};
            var loc3 = new Location{LocationType = "Auditorium", ShortDescription = "D1090", LongDescription = "Auditorium 2"};
            var loc4 = new Location{LocationType = "Grupperom", ShortDescription = "B3020", LongDescription = "Grupperom med balkong og telefon"};
            var loc5 = new Location{LocationType = "Kontor", ShortDescription = "C4230", LongDescription = "Arild Steen bor her :)"};
            
            context.Locations.AddOrUpdate(
                p => p.ShortDescription,
                loc1,
                loc2,
                loc3,
                loc4,
                loc5);

            
        }
    }
}
