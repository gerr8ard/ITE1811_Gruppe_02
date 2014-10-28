namespace HiNSimulator2014.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using HiNSimulator2014.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    /// <summary>
    /// Skrevet av: Andreas Jansson og Pål Skogsrud
    /// 
    /// 
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<HiNSimulator2014.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
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

            var locations = new List<Location>{
                new Location{
                    LocationType = "Korridor", LocationName = "D3310", ShortDescription = "Gang", LongDescription = "Gang ved i tredje etasje"
                },
                 new Location{
                    LocationType = "Klasserom", LocationName = "D3320", ShortDescription = "Klasserom for elektronikkstudenter", LongDescription = "Klasserom for studenter som går Digital teknikk", AcessTypeRole = "EL"
                },
                 new Location{
                    LocationType = "Datarom", LocationName = "D3330", ShortDescription = "Linuxlabben", LongDescription = "Linuxlabben er det mest brukte rommet for studenter som går datateknikk ved høgskolen", AcessTypeRole = "DT"
                },
                 new Location{
                    LocationType = "Klasserom", LocationName = "D3340", ShortDescription = "Grunnlagslab", LongDescription = "Klasserom for elektronikkstudenter", AcessTypeRole = "EL"
                },
                 new Location{
                    LocationType = "Klasserom", LocationName = "D3350", ShortDescription = "Elektronikk produksjon", LongDescription = "Klasserom for elektronikkstudenter", AcessTypeRole = "EL"
                },
                new Location{
                    LocationType = "Klasserom", LocationName = "D3360", ShortDescription = "Verksted ELK", LongDescription = "Verksted for elektronikkstudenter", AcessTypeRole = "ELK"
                },
                new Location{
                    LocationType = "Korridor", LocationName = "C3020", ShortDescription = "Gang", LongDescription = "Gang ved siden av toalettene"
                },
                new Location{
                    LocationType = "Toalett", LocationName = "C3040", ShortDescription = "Herretoalett", LongDescription = "Her kan det gå strålende eller bare dritt :)"
                },
                new Location{
                    LocationType = "Toalett", LocationName ="C3050", ShortDescription = "Dametoalett", LongDescription = "Her kan det gå strålende eller bare dritt :)"
                },
                new Location{
                    LocationType = "Kontor", LocationName = "C2100", ShortDescription = "Einars kontor", LongDescription = "Einar er en hardtarbeidende student og har alltid tid til en prat"
                },
                new Location{
                    LocationType = "Gang", LocationName = "C2000", ShortDescription = "Gang", LongDescription = "Just a ordinary corridor"
                },
                new Location{
                    LocationType = "Gang", LocationName = "D2500-C", ShortDescription = "Galleri"
                },
                new Location{
                    LocationType = "Gang", LocationName = "Glassgata", ShortDescription = "Glassgata er en gate laget av glass"
                },
                new Location{
                    LocationType = "Grupperom", LocationName = "C1001", ShortDescription = "--"
                },
                new Location{
                    LocationType = "Kontor", LocationName = "C1070", ShortDescription = "HiN IL's kontor"
                },
                new Location{
                    LocationType = "Bru", LocationName = "BRU-2C", LongDescription = "Under broen bor det et troll"
                },
                new Location{
                    LocationType = "Bru", LocationName = "BRU-3C", LongDescription = "En fin bro"
                },
                new Location{
                    LocationType = "Gang", LocationName = "D2360", ShortDescription = "Dean & Project Area", LongDescription = "In this part is the Dean's office, but also offices to those that work with projects and simulations."
                },
                new Location{
                    LocationType = "Kontor", LocationName = "D2210", ShortDescription = "Hans Olofsen's office", LongDescription = "You are amazed by how tidy this office is."
                },
                new Location{
                    LocationType = "Kontor", LocationName = "D2280", ShortDescription = "The Dean's office", LongDescription = "What are you doing in the Dean's office without asking for permission!?!"
                },
                new Location{
                    LocationType = "Kontor", LocationName = "D2310", LongDescription = "Jostein er en veldig hyggelig stipendiat. Han hjelper gjerne."
                },
                new Location{
                    LocationType = "Gang", LocationName = "C3191", ShortDescription = "Engineering Design Area", LongDescription = ""
                },
                new Location{
                    LocationType = "Kontor", LocationName = "D3440", ShortDescription = "Guy's office", LongDescription = "Guy is a nice and funny guy."
                },
                new Location{
                    LocationType = "Kontor", LocationName = "D3400C", ShortDescription = "Prof. Per-Arne Sundsbø's office", LongDescription = "Prof. Sundsbø is a role-model professor"
                },
                new Location{
                    LocationType = "Kontor", LocationName = "D3480", ShortDescription = "Prof. Annette Meidell's office", LongDescription = "Prof. Meidell was the first female professor at Narvik Univeristy College"
                },
                new Location{
                    LocationType = "Gang", LocationName = "C4330", ShortDescription = "C4-area", LongDescription = "In the part of the building are the offices of the other computer engineering teachers. You decide not to disturb them."
                },
                new Location{
                    LocationType = "Gang", LocationName = "C5460", ShortDescription = "C5-area", LongDescription = "This floor belong to the Health Department. You have no business here."
                },
                new Location{
                    LocationType = "Gang", LocationName = "C6001", ShortDescription = "Parking area C", LongDescription = "Outside is parking for employees. You cannot open the doors."
                }
            };

            locations.ForEach(element => context.Locations.AddOrUpdate(locationName => locationName.LocationName, element));
            context.SaveChanges();

        }
    }
}
