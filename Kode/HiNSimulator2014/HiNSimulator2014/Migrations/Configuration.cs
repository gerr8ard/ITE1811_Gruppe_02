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

            var loc1 = new Location { LocationType = "Korridor", LocationName = "D3310", LongDescription = "Gang ved i tredje etasje" };
            var loc2 = new Location { LocationType = "Klasserom", LocationName = "D3320", LongDescription = "Digital teknikk", AcessTypeRole = "EL" };
            var loc3 = new Location { LocationType = "Datarom", LocationName = "D3330", LongDescription = "Linuxlabben", AcessTypeRole = "DT" };
            var loc4 = new Location { LocationType = "Klasserom", LocationName = "D3340", LongDescription = "Grunnlagslab", AcessTypeRole = "EL" };
            var loc5 = new Location { LocationType = "Klasserom", LocationName = "D3350", LongDescription = "Elektronikk produksjon", AcessTypeRole = "EL" };
            var loc6 = new Location { LocationType = "Klasserom", LocationName = "D3360", LongDescription = "Verkstes ELK", AcessTypeRole = "ELK" };

            var loc7 = new Location { LocationType = "Korridor", LocationName = "C3020", LongDescription = "Gang med toaletter" };
            var loc8 = new Location { LocationType = "Toalett", LocationName = "C3040", LongDescription = "Herretoalett" };
            var loc9 = new Location { LocationType = "Toalett", LocationName = "C3050", LongDescription = "Dametoalett" };
            var loc10 = new Location { LocationType = "Kontor", LocationName = "C2100", LongDescription = "Einars kontor", AcessTypeRole = "EL" };
            var loc11 = new Location { LocationType = "Klasserom", LocationName = "D3350", LongDescription = "Elektronikk produksjon", AcessTypeRole = "EL" };
            var loc12 = new Location { LocationType = "Klasserom", LocationName = "D3350", LongDescription = "Elektronikk produksjon", AcessTypeRole = "EL" };
            var loc13 = new Location { LocationType = "Klasserom", LocationName = "D3350", LongDescription = "Elektronikk produksjon", AcessTypeRole = "EL" };
            var loc14 = new Location { LocationType = "Klasserom", LocationName = "D3350", LongDescription = "Elektronikk produksjon", AcessTypeRole = "EL" };
            
            var loc17 = new Location { LocationType = "Gang", LocationName = "D2500-C", ShortDescription = "Galleri" };
            var loc18 = new Location { LocationType = "Gang", LocationName = "Glassgata", ShortDescription = "Glassgata er en gate laget av glass" };
            var loc19 = new Location { LocationType = "Grupperom", LocationName = "C1001", ShortDescription = "--" };
            var loc20 = new Location { LocationType = "Kontor", LocationName = "C1070", ShortDescription = "HiN IL's kontor" };
            var loc21 = new Location { LocationType = "Bru", LocationName = "BRU-2C", LongDescription = "Under broen bor det et troll" };
            var loc22 = new Location { LocationType = "Bru", LocationName = "BRU-3C", LongDescription = "En fin bro" };
            var loc23 = new Location { LocationType = "Gang", LocationName = "D2360", ShortDescription = "Dean & Project Area", LongDescription = "In this part is the Dean's office, but also offices to those that work with projects and simulations." };
            var loc24 = new Location { LocationType = "Kontor", LocationName = "D2210", ShortDescription = "Hans Olofsen's office", LongDescription = "You are amazed by how tidy this office is." };
            var loc25 = new Location { LocationType = "Kontor", LocationName = "D2280", ShortDescription = "The Dean's office", LongDescription = "What are you doing in the Dean's office without asking for permission!?!" };
            var loc26 = new Location { LocationType = "Kontor", LocationName = "D2310", LongDescription = "Jostein er en veldig hyggelig stipendiat. Han hjelper gjerne." };
            var loc27 = new Location { LocationType = "Gang", LocationName = "C3191", ShortDescription = "Engineering Design Area", LongDescription = "" };
            var loc28 = new Location { LocationType = "Kontor", LocationName = "D3440", ShortDescription = "Guy's office", LongDescription = "Guy is a nice and funny guy." };
            var loc29 = new Location { LocationType = "Kontor", LocationName = "D3400C", ShortDescription = "Prof. Per-Arne Sundsbø's office", LongDescription = "Prof. Sundsbø is a role-model professor" };
            var loc30 = new Location { LocationType = "Kontor", LocationName = "D3480", ShortDescription = "Prof. Annette Meidell's office", LongDescription = "Prof. Meidell was the first female professor at Narvik Univeristy College" };
            var loc31 = new Location { LocationType = "Gang", LocationName = "C4330", ShortDescription = "C4-area", LongDescription = "In the part of the building are the offices of the other computer engineering teachers. You decide not to disturb them." };
            var loc32 = new Location { LocationType = "Gang", LocationName = "C5460", ShortDescription = "C5-area", LongDescription = "This floor belong to the Health Department. You have no business here." };
            var loc33 = new Location { LocationType = "Gang", LocationName = "C6001", ShortDescription = "Parking area C", LongDescription = "Outside is parking for employees. You cannot open the doors." };
            
            context.Locations.AddOrUpdate(
                p => p.LocationName,
                loc1,
                loc2,
                loc3,
                loc4,
                loc5,
                loc17,
                loc18,
                loc19,
                loc20,
                loc21,
                loc22,
                loc23,
                loc24,
                loc25,
                loc26,
                loc27,
                loc28,
                loc29,
                loc30,
                loc31,
                loc32,
                loc33);

        }
    }
}
