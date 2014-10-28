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
            
            var loc1 = new Location{LocationType = "Korridor", ShortDescription = "D3310", LongDescription = "Gang ved i tredje etasje"};
            var loc2 = new Location { LocationType = "Klasserom", ShortDescription = "D3320", LongDescription = "Digital teknikk", AcessTypeRole = "EL" };
            var loc3 = new Location { LocationType = "Datarom", ShortDescription = "D3330", LongDescription = "Linuxlabben", AcessTypeRole = "DT" };
            var loc4 = new Location { LocationType = "Klasserom", ShortDescription = "D3340", LongDescription = "Grunnlagslab", AcessTypeRole = "EL" };
            var loc5 = new Location {LocationType = "Klasserom", ShortDescription = "D3350", LongDescription = "Elektronikk produksjon", AcessTypeRole = "EL"};
            var loc6 = new Location { LocationType = "Klasserom", ShortDescription = "D3360", LongDescription = "Verkstes ELK", AcessTypeRole = "ELK" };

            var loc7 = new Location { LocationType = "Korridor", ShortDescription = "C3020", LongDescription = "Gang med toaletter"};
            var loc8 = new Location { LocationType = "Toalett", ShortDescription = "C3040", LongDescription = "Herretoalett"};
            var loc9 = new Location { LocationType = "Toalett", ShortDescription = "C3050", LongDescription = "Dametoalett"};
            var loc10 = new Location { LocationType = "Kontor", ShortDescription = "C2100", LongDescription = "Einars kontor", AcessTypeRole = "EL" };
            var loc11 = new Location { LocationType = "Klasserom", ShortDescription = "D3350", LongDescription = "Elektronikk produksjon", AcessTypeRole = "EL" };
            var loc12 = new Location { LocationType = "Klasserom", ShortDescription = "D3350", LongDescription = "Elektronikk produksjon", AcessTypeRole = "EL" };
            var loc13 = new Location { LocationType = "Klasserom", ShortDescription = "D3350", LongDescription = "Elektronikk produksjon", AcessTypeRole = "EL" };
            var loc14 = new Location { LocationType = "Klasserom", ShortDescription = "D3350", LongDescription = "Elektronikk produksjon", AcessTypeRole = "EL" };

            
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
