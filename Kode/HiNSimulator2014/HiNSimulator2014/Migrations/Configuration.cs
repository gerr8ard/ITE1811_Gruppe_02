namespace HiNSimulator2014.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using HiNSimulator2014.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    /// <summary>
    /// Skrevet av: Andreas Jansson og Pål Skogsrud
    /// 
    /// 
    /// </summary>
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

            var loc1 = new Location { LocationType = "Korridor", LocationName = "D3310", ShortDescription = "Gang", LongDescription = "Gang ved i tredje etasje" };
            var loc2 = new Location { LocationType = "Klasserom", LocationName = "D3320", ShortDescription = "Klasserom for elektronikkstudenter", LongDescription = "Klasserom for studenter som går Digital teknikk", AcessTypeRole = "EL" };
            var loc3 = new Location { LocationType = "Datarom", LocationName = "D3330", ShortDescription = "Linuxlabben", LongDescription = "Linuxlabben er det mest brukte rommet for studenter som går datateknikk ved høgskolen", AcessTypeRole = "DT" };
            var loc4 = new Location { LocationType = "Klasserom", LocationName = "D3340", ShortDescription = "Grunnlagslab", LongDescription = "Klasserom for elektronikkstudenter", AcessTypeRole = "EL" };
            var loc5 = new Location { LocationType = "Klasserom", LocationName = "D3350", ShortDescription = "Elektronikk produksjon", LongDescription = "Klasserom for elektronikkstudenter", AcessTypeRole = "EL" };
            var loc6 = new Location { LocationType = "Klasserom", LocationName = "D3360", ShortDescription = "Verksted ELK", LongDescription = "Verksted for elektronikkstudenter", AcessTypeRole = "ELK" };
            var loc7 = new Location { LocationType = "Korridor", LocationName = "C3020", ShortDescription = "Gang", LongDescription = "Gang ved siden av toalettene" };
            var loc8 = new Location { LocationType = "Toalett", LocationName = "C3040", ShortDescription = "Herretoalett", LongDescription = "Her kan det gå strålende eller bare dritt :)" };
            var loc9 = new Location { LocationType = "Toalett", LocationName ="C3050", ShortDescription = "Dametoalett", LongDescription = "Her kan det gå strålende eller bare dritt :)" };
            var loc10 = new Location { LocationType = "Kontor", LocationName = "C2100", ShortDescription = "Einars kontor", LongDescription = "Einar er en hardtarbeidende student og har alltid tid til en prat"};
            var loc11 = new Location { LocationType = "Kontor", LocationName = "C2100", ShortDescription = "Einars kontor", LongDescription = "Einar er en hardtarbeidende student og har alltid tid til en prat" };
            var loc12 = new Location { LocationType = "Gang", LocationName = "C2000", ShortDescription = "Gang", LongDescription = "Just a ordinary corridor" };
            

            
            context.Locations.AddOrUpdate(
                p => p.ShortDescription,
                loc1,
                loc2,
                loc3,
                loc4,
                loc5,
                loc6,
                loc7,
                loc8,
                loc9,
                loc10,
                loc11,
                loc12);

            
        }
    }
}
