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



            var loc17 = new Location { LocationType = "Gang", ShortDescription = "D2500-C", LongDescription = "Galleri" };
            var loc18 = new Location { LocationType = "Gang", ShortDescription = "Glassgata", LongDescription = "Glassgata er en gate leget av glass" };
            var loc19 = new Location { LocationType = "Grupperom", ShortDescription = "C1001", LongDescription = "Et rom med tak og fire vegger" };
            var loc20 = new Location { LocationType = "Kontor", ShortDescription = "C1070", LongDescription = "HiN IL's kontor" };
            var loc21 = new Location { LocationType = "Bru", ShortDescription = "BRU-2C", LongDescription = "Under broen bor det et troll" };
            var loc22 = new Location { LocationType = "Bru", ShortDescription = "BRU-3C", LongDescription = "En fin bro" };
            var loc23 = new Location { LocationType = "Gang", ShortDescription = "D2360", LongDescription = "In this part is the Dean's office, but also offices to those that work with projects and simulations.'" };
            var loc24 = new Location { LocationType = "Kontor", ShortDescription = "D2210", LongDescription = "Her bor Hans Olofsen" };
            var loc25 = new Location { LocationType = "Kontor", ShortDescription = "D2280", LongDescription = "What are youdoing in the Dean's office without asking for permission!?!" };
            var loc26 = new Location { LocationType = "Gang", ShortDescription = "D2500-C", LongDescription = "Galleri" };
            var loc27 = new Location { LocationType = "Gang", ShortDescription = "D2500-C", LongDescription = "Galleri" };
            var loc28 = new Location { LocationType = "Gang", ShortDescription = "D2500-C", LongDescription = "Galleri" };
            var loc29 = new Location { LocationType = "Gang", ShortDescription = "D2500-C", LongDescription = "Galleri" };
            var loc30 = new Location { LocationType = "Gang", ShortDescription = "D2500-C", LongDescription = "Galleri" };
            var loc31 = new Location { LocationType = "Gang", ShortDescription = "D2500-C", LongDescription = "Galleri" };
            var loc32 = new Location { LocationType = "Gang", ShortDescription = "D2500-C", LongDescription = "Galleri" };
            var loc33 = new Location { LocationType = "Gang", ShortDescription = "D2500-C", LongDescription = "Galleri" };

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
