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

        private UserManager<ApplicationUser> userManager;
        private UserStore<ApplicationUser> userStore;

        /// <summary>
        /// Metode som lager en ny bruker
        /// </summary>
        /// <param name="_userName">Brukernavn</param>
        /// <param name="password">Passord</param>
        /// <param name="_playerName">Navn på spilleren</param>
        /// <param name="_accessLevel">Tilgangsrettigheter</param>
        /// <param name="_writePermission">Skriverettigheter</param>
        /// <param name="_currentLocation">Spillerens posisjon</param>
        /// <returns>Ny bruker</returns>
        private ApplicationUser createUser(string _userName, string password, string _playerName, string _accessLevel, bool _writePermission, Location _currentLocation)
        {
            var user = userManager.FindByName(_userName);
            

            if (user == null)
            {
                user = new ApplicationUser { UserName = _userName, PlayerName = _playerName, Email = _userName, AccessLevel = _accessLevel, WritePermission = _writePermission, CurrentLocation = _currentLocation };
                var result = userManager.Create(user, password);
            }
            else
            {
                string newPassword = password;
                string newHashPassword = userManager.PasswordHasher.HashPassword(newPassword);

                user.AccessLevel = _accessLevel;
                user.PlayerName = _playerName;
                user.UserName = _userName;
                user.WritePermission = _writePermission;
                user.CurrentLocation = _currentLocation;
                user.PasswordHash = newHashPassword;                

                var result = userManager.Update(user);
            }
            return user;
        }

        protected override void Seed(HiNSimulator2014.Models.ApplicationDbContext context)
        {

            userStore = new UserStore<ApplicationUser>(context);
            userManager = new UserManager<ApplicationUser>(userStore);

            var locations = new List<Location>{
                new Location{
                    LocationType = "Korridor", 
                    LocationName = "D3310", 
                    ShortDescription = "Gang", 
                    LongDescription = "Gang ved i tredje etasje"
                },
                 new Location{
                    LocationType = "Klasserom", 
                    LocationName = "D3320", 
                    ShortDescription = "Klasserom for elektronikkstudenter", 
                    LongDescription = "Klasserom for studenter som går Digital teknikk", 
                    AcessTypeRole = "EL"
                },
                 new Location{
                    LocationType = "Datarom", 
                    LocationName = "D3330", 
                    ShortDescription = "Linuxlabben", 
                    LongDescription = "Linuxlabben er det mest brukte rommet for studenter som går datateknikk ved høgskolen", 
                    AcessTypeRole = "DT"
                },
                 new Location{
                    LocationType = "Klasserom", 
                    LocationName = "D3340", 
                    ShortDescription = "Grunnlagslab", 
                    LongDescription = "Klasserom for elektronikkstudenter", 
                    AcessTypeRole = "EL"
                },
                 new Location{
                    LocationType = "Klasserom", 
                    LocationName = "D3350", 
                    ShortDescription = "Elektronikk produksjon", 
                    LongDescription = "Klasserom for elektronikkstudenter", 
                    AcessTypeRole = "EL"
                },
                new Location{
                    LocationType = "Klasserom", 
                    LocationName = "D3360", 
                    ShortDescription = "Verksted ELK", 
                    LongDescription = "Verksted for elektronikkstudenter", 
                    AcessTypeRole = "ELK"
                },
                new Location{
                    LocationType = "Korridor", 
                    LocationName = "C3020", 
                    ShortDescription = "Gang", 
                    LongDescription = "Gang ved siden av toalettene"
                },
                new Location{
                    LocationType = "Toalett", 
                    LocationName = "C3040", 
                    ShortDescription = "Herretoalett", 
                    LongDescription = "Her kan det gå strålende eller bare dritt :)"
                },
                new Location{
                    LocationType = "Toalett", 
                    LocationName ="C3050", 
                    ShortDescription = "Dametoalett", 
                    LongDescription = "Her kan det gå strålende eller bare dritt :)"
                },
                new Location{
                    LocationType = "Kontor", 
                    LocationName = "C2100", 
                    ShortDescription = "Einars kontor", 
                    LongDescription = "Einar er en hardtarbeidende student og har alltid tid til en prat"
                },
                new Location{
                    LocationType = "Gang", 
                    LocationName = "C2000", 
                    ShortDescription = "Gang", 
                    LongDescription = "Just an ordinary corridor"
                },
                new Location{
                    LocationType = "Gang", 
                    LocationName = "D2500-C", 
                    ShortDescription = "Galleri"
                },
                new Location{
                    LocationType = "Gang", 
                    LocationName = "Glassgata", 
                    ShortDescription = "Glassgata er en gate laget av glass"
                },
                new Location{
                    LocationType = "Grupperom", 
                    LocationName = "C1001", 
                    ShortDescription = "--"
                },
                new Location{
                    LocationType = "Kontor", 
                    LocationName = "C1070", 
                    ShortDescription = "HiN IL's kontor"
                },
                new Location{
                    LocationType = "Bru", 
                    LocationName = "BRU-2C", 
                    LongDescription = "Under broen bor det et troll"
                },
                new Location{
                    LocationType = "Bru", 
                    LocationName = "BRU-3C", 
                    LongDescription = "En fin bro"
                },
                new Location{
                    LocationType = "Gang", 
                    LocationName = "D2360", 
                    ShortDescription = "Dean & Project Area", 
                    LongDescription = "In this part is the Dean's office, but also offices to those that work with projects and simulations."
                },
                new Location{
                    LocationType = "Kontor", 
                    LocationName = "D2210", 
                    ShortDescription = "Hans Olofsen's office", 
                    LongDescription = "You are amazed by how tidy this office is."
                },
                new Location{
                    LocationType = "Kontor", 
                    LocationName = "D2280", 
                    ShortDescription = "The Dean's office", 
                    LongDescription = "What are you doing in the Dean's office without asking for permission!?!"
                },
                new Location{
                    LocationType = "Kontor", 
                    LocationName = "D2310", 
                    LongDescription = "Jostein er en veldig hyggelig stipendiat. Han hjelper gjerne."
                },
                new Location{
                    LocationType = "Gang", 
                    LocationName = "C3191", 
                    ShortDescription = "Engineering Design Area", 
                    LongDescription = ""
                },
                new Location{
                    LocationType = "Kontor", 
                    LocationName = "D3440", 
                    ShortDescription = "Guy's office", 
                    LongDescription = "Guy is a nice and funny guy."
                },
                new Location{
                    LocationType = "Kontor", 
                    LocationName = "D3400C", 
                    ShortDescription = "Prof. Per-Arne Sundsbø's office", 
                    LongDescription = "Prof. Sundsbø is a role-model professor"
                },
                new Location{
                    LocationType = "Kontor", 
                    LocationName = "D3480", 
                    ShortDescription = "Prof. Annette Meidell's office", 
                    LongDescription = "Prof. Meidell was the first female professor at Narvik Univeristy College"
                },
                new Location{
                    LocationType = "Gang", 
                    LocationName = "C4330", 
                    ShortDescription = "C4-area", 
                    LongDescription = "In the part of the building are the offices of the other computer engineering teachers. You decide not to disturb them."
                },
                new Location{
                    LocationType = "Gang",
                    LocationName = "C5460", 
                    ShortDescription = "C5-area",
                    LongDescription = "This floor belong to the Health Department. You have no business here."
                },
                new Location{
                    LocationType = "Gang", 
                    LocationName = "C6001", 
                    ShortDescription = "Parking area C", 
                    LongDescription = "Outside is parking for employees. You cannot open the doors."
                }
            };

            locations.ForEach(element => context.Locations.AddOrUpdate(locationName => locationName.LocationName, element));
            context.SaveChanges();

            var location01 = context.Locations.Where(l => l.LocationID == 1).FirstOrDefault();
            var location02 = context.Locations.Where(l => l.LocationID == 2).FirstOrDefault();
            var location03 = context.Locations.Where(l => l.LocationID == 3).FirstOrDefault();
            var location04 = context.Locations.Where(l => l.LocationID == 4).FirstOrDefault();
            var location05 = context.Locations.Where(l => l.LocationID == 5).FirstOrDefault();
            var location06 = context.Locations.Where(l => l.LocationID == 6).FirstOrDefault();
            var location07 = context.Locations.Where(l => l.LocationID == 7).FirstOrDefault();
            var location08 = context.Locations.Where(l => l.LocationID == 8).FirstOrDefault();
            var location09 = context.Locations.Where(l => l.LocationID == 9).FirstOrDefault();
            var location10 = context.Locations.Where(l => l.LocationID == 10).FirstOrDefault();
            var location11 = context.Locations.Where(l => l.LocationID == 11).FirstOrDefault();
            var location12 = context.Locations.Where(l => l.LocationID == 12).FirstOrDefault();
            var location13 = context.Locations.Where(l => l.LocationID == 13).FirstOrDefault();
            var location14 = context.Locations.Where(l => l.LocationID == 14).FirstOrDefault();
            var location15 = context.Locations.Where(l => l.LocationID == 15).FirstOrDefault();
            var location16 = context.Locations.Where(l => l.LocationID == 16).FirstOrDefault();
            var location17 = context.Locations.Where(l => l.LocationID == 17).FirstOrDefault();
            var location18 = context.Locations.Where(l => l.LocationID == 18).FirstOrDefault();
            var location19 = context.Locations.Where(l => l.LocationID == 19).FirstOrDefault();
            var location20 = context.Locations.Where(l => l.LocationID == 20).FirstOrDefault();
            var location21 = context.Locations.Where(l => l.LocationID == 21).FirstOrDefault();
            var location22 = context.Locations.Where(l => l.LocationID == 22).FirstOrDefault();
            var location23 = context.Locations.Where(l => l.LocationID == 23).FirstOrDefault();
            var location24 = context.Locations.Where(l => l.LocationID == 24).FirstOrDefault();
            var location25 = context.Locations.Where(l => l.LocationID == 25).FirstOrDefault();
            var location26 = context.Locations.Where(l => l.LocationID == 26).FirstOrDefault();
            var location27 = context.Locations.Where(l => l.LocationID == 27).FirstOrDefault();
            var location28 = context.Locations.Where(l => l.LocationID == 28).FirstOrDefault();

            var things = new List<Thing>{
                new Thing{
                    Name = "Cola-boks",
                    Description = "En cola-boks med cola inni evt oppi. Den er full.",
                    IsStationary = false,
                    WrittenText = "Inneholder koockain og herpes.",
                    LocationID = context.Locations.Single(l => l.LocationID == 6).LocationID,
                    PlayerWritable = false
                },
                new Thing{
                    Name = "Tavle",
                    Description = "En kul gul tavle som henger på veggen.",
                    IsStationary = true,
                    WrittenText = "Whaaaaaaaat?",
                    LocationID = context.Locations.Single(l => l.LocationID == 9).LocationID,
                    PlayerWritable = true
                },
                new Thing{
                    Name = "Bazooka",
                    Description = "En sovjetisk bazooka fra den kalde krigen.",
                    IsStationary = false,
                    WrittenText = "что-то смешное на русском.",
                    LocationID = context.Locations.Single(l => l.LocationID == 19).LocationID,
                    PlayerWritable = false
                },
            };

            things.ForEach(element => context.Things.AddOrUpdate(u => u.Name, element));
            context.SaveChanges();

            var artificialPlayer = new List<ArtificialPlayer>{
                new ArtificialPlayer{
                    Name = "Hans Olofsen",
                    Description = "Hans er en trivelig dude som jobber på skolen",
                    IsStationary = false,
                    Type = "Førstelektor",
                    LocationID = context.Locations.Single(l => l.LocationID == 19).LocationID,
                    AccessLevel = "Universal"
                },
                new ArtificialPlayer{
                    Name = "Knut Collin",
                    Description = "Høgskolelektor med avansert kunnskap innen forskjellige programmeringsspråk",
                    IsStationary = false,
                    Type = "Høgskolelektor",
                    LocationID = context.Locations.Single(l => l.LocationID == 8).LocationID,
                    AccessLevel = "Universal"
                },
                new ArtificialPlayer{
                    Name = "Arvid Urke",
                    Description = "Snasn kis med mye på hjertet",
                    IsStationary = false,
                    Type = "Rådgiver",
                    LocationID = context.Locations.Single(l => l.LocationID == 28).LocationID,
                    AccessLevel = "Universal"
                },
                new ArtificialPlayer{
                    Name = "Dracula",
                    Description = "Skummel kar som biter",
                    IsStationary = false,
                    Type = "Vampyr",
                    LocationID = context.Locations.Single(l => l.LocationID == 18).LocationID,
                    AccessLevel = "Universal"
                }

            };

            artificialPlayer.ForEach(element => context.ArtificialPlayers.AddOrUpdate(u => u.Name, element));
            context.SaveChanges();

            var userPaal = createUser("pskogsru88@hotmail.com", "appelsinFarge5", "Gerrard", "Datateknikk", false, location03);
            var userTina = createUser("tinahotty64@hotmail.com", "appelsinFarge5", "Tina", "Datateknikk", false, location03);
            var userKristina = createUser("kristinamyrligundersen@gmail.com", "appelsinFarge5", "Kristina", "Datateknikk", false, location03);
            var userAndreas = createUser("drknert@gmail.com", "appelsinFarge5", "Andreas", "Datateknikk", false, location03);
            var userAlexander = createUser("alec90@gmail.com", "appelsinFarge5", "Alexander", "Datateknikk", false, location03);
            var userMarius = createUser("skaterase@gmail.com", "appelsinFarge5", "Marius", "Datateknikk", false, location03);

            var commands = new List<Command>
            {
                new Command {
                    Name = "Take",
                    Description = "Plukker opp valgt gjenstand"
                },
                new Command {
                    Name = "Open",
                    Description = "Åpner dør, skap o.l"
                },
                new Command {
                    Name = "Use",
                    Description = "Aktiverer funksjon på objekt"
                },
                new Command {
                    Name = "Drop",
                    Description = "Legg fra deg valgt gjenstand"
                },
                new Command {
                    Name = "Turn on",
                    Description = "Slår på objekt"
                },
                new Command {
                    Name = "Turn off",
                    Description = "Slår av objekt"
                },
                new Command {
                    Name = "Talk",
                    Description = "Starter samtale i valgt rom"
                },
                new Command {
                    Name = "Kick",
                    Description = "Sparker et objekt"
                },
                new Command {
                    Name = "Close",
                    Description = "Lukker dør, skap ol."
                },
                new Command {
                    Name = "Enter",
                    Description = "Går inn dør, rom"
                },
                new Command {
                    Name = "Write on",
                    Description = "Åpner en editor for å endre tekst på et objekt"
                },
                new Command {
                    Name = "Look at",
                    Description = "Viser detaljert informasjon om objekt"
                },
                new Command {
                    Name = "Punch",
                    Description = "Slår et objekt"
                },
            };
            commands.ForEach(element => context.Commands.AddOrUpdate(u => u.Name, element));
            context.SaveChanges();

            var artificialPlayerResponses = new List<ArtificialPlayerResponse>
            {
            #region Hans 
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayer = context.ArtificialPlayers.Single(l => l.ArtificialPlayerID == 1),
                    ResponseText = "God dag!"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayer = context.ArtificialPlayers.Single(l => l.ArtificialPlayerID == 1),
                    ResponseText = "Jeg har et veldig ryddig kontor"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayer = context.ArtificialPlayers.Single(l => l.ArtificialPlayerID == 1),
                    ResponseText = "Fint vær i dag"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayer = context.ArtificialPlayers.Single(l => l.ArtificialPlayerID == 1),
                    ResponseText = "Jeg lærer meg asp.NET"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayer = context.ArtificialPlayers.Single(l => l.ArtificialPlayerID == 1),
                    ResponseText = "Skolen er full av hemmeligheter!"
                },
            #endregion
            #region kc
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayer = context.ArtificialPlayers.Single(l => l.ArtificialPlayerID == 2),
                    ResponseText = "..."
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayer = context.ArtificialPlayers.Single(l => l.ArtificialPlayerID == 2),
                    ResponseText = "Jeg har masse å gjøre"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayer = context.ArtificialPlayers.Single(l => l.ArtificialPlayerID == 2),
                    ResponseText = "Jeg har rettet oblig'en din, det ser meget bra ut"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayer = context.ArtificialPlayers.Single(l => l.ArtificialPlayerID == 2),
                    ResponseText = "Jeg skal holde et foredrag om java i morgen"
                },
            #endregion
            #region urke
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayer = context.ArtificialPlayers.Single(l => l.ArtificialPlayerID == 3),
                    ResponseText = "Stå på! Tenk på dopaminet!"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayer = context.ArtificialPlayers.Single(l => l.ArtificialPlayerID == 3),
                    ResponseText = "Du kan klare alt! Fortsett sånn."
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayer = context.ArtificialPlayers.Single(l => l.ArtificialPlayerID == 3),
                    ResponseText = "Jeg skal personlig sørge for at du får A+ på eksamen"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayer = context.ArtificialPlayers.Single(l => l.ArtificialPlayerID == 3),
                    ResponseText = "Høgskolen i Narvik er den beste i Norge, nei, hele verden!"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayer = context.ArtificialPlayers.Single(l => l.ArtificialPlayerID == 3),
                    ResponseText = "Alle husker dagen Urke mistet buksene"
                },
            #endregion
            #region dracula
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayer = context.ArtificialPlayers.Single(l => l.ArtificialPlayerID == 4),
                    ResponseText = "Ha, ha! Jeg er Dracula!"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayer = context.ArtificialPlayers.Single(l => l.ArtificialPlayerID == 4),
                    ResponseText = "I natt skal det skje."
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayer = context.ArtificialPlayers.Single(l => l.ArtificialPlayerID == 4),
                    ResponseText = "Du kan ikke rømme fra Dracula!"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayer = context.ArtificialPlayers.Single(l => l.ArtificialPlayerID == 4),
                    ResponseText = "Hvem våger å forstyrre Dracula?"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayer = context.ArtificialPlayers.Single(l => l.ArtificialPlayerID == 4),
                    ResponseText = "It's idnight and the oon is up"
                }
            #endregion  

            };

            artificialPlayerResponses.ForEach(element => context.ArtificialPlayerResponses.AddOrUpdate(u => u.ResponseText, element));
            context.SaveChanges();
        }

        
    }
}
