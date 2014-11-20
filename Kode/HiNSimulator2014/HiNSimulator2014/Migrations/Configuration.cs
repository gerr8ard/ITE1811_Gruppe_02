#region Fil
namespace HiNSimulator2014.Migrations
{
    #region referanser
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using HiNSimulator2014.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    #endregion

    #region Klasse
    /// <summary>
    /// Skrevet av: Andreas Jansson og Pål Skogsrud
    /// 
    /// 
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<HiNSimulator2014.Models.ApplicationDbContext>
    {
        #region Medlemsvariabler
        private UserManager<ApplicationUser> userManager;
        private UserStore<ApplicationUser> userStore;
        #endregion

        #region Konstruktør
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }
        #endregion

       

        #region Opprett bruker metode
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
        #endregion

        #region Opprett locationConnection
        public void CreateLocationConnection(LocationConnection lc, HiNSimulator2014.Models.ApplicationDbContext context)
        {
            var locationConnection = context.LocationConnections.Where(u => u.LocationOne_LocationID == lc.LocationOne_LocationID && u.LocationTwo_LocationID == lc.LocationTwo_LocationID).FirstOrDefault();
            if (locationConnection != null)
            {
                locationConnection.LocationOne_LocationID = lc.LocationOne_LocationID;
                locationConnection.LocationTwo_LocationID = lc.LocationTwo_LocationID;
            }
            else
            {
                context.LocationConnections.Add(lc);
            }

            context.SaveChanges();
        }
        #endregion

        #region Opprett gyldige kommandoer for kunstige aktører
        public void CreateValidCommandsForAI(ValidCommandsForArtificialPlayers vcfaip, HiNSimulator2014.Models.ApplicationDbContext context)
        {
            var valCom = context.ValidCommandsForArtificialPlayers.Where(u => u.CommandID == vcfaip.CommandID && u.ArtificialPlayerID == vcfaip.ArtificialPlayerID).FirstOrDefault();
            if (valCom != null)
            {
                valCom.ArtificialPlayerID = vcfaip.ArtificialPlayerID;
                valCom.CommandID = vcfaip.CommandID;
            }
            else
            {
                context.ValidCommandsForArtificialPlayers.Add(vcfaip);
            }

            context.SaveChanges();
        }
        #endregion

        #region Opprett gyldige kommandoer for ting
        public void CreateValidCommandsForThings(ValidCommandsForThings vcft, HiNSimulator2014.Models.ApplicationDbContext context)
        {
            var valCom = context.ValidCommandsForThings.Where(u => u.CommandID == vcft.CommandID && u.ThingID== vcft.ThingID).FirstOrDefault();
            if (valCom != null)
            {
                valCom.ThingID= vcft.ThingID;
                valCom.CommandID = vcft.CommandID;
            }
            else
            {
                context.ValidCommandsForThings.Add(vcft);
            }

            context.SaveChanges();
        }
        #endregion

        #region Seed-metode
        protected override void Seed(HiNSimulator2014.Models.ApplicationDbContext context)
        {

            userStore = new UserStore<ApplicationUser>(context);
            userManager = new UserManager<ApplicationUser>(userStore);

            #region Slett innhold i database
            /*
            //Sletter innhold i databasen før seeding. http://stackoverflow.com/questions/25702693/how-do-i-delete-all-data-in-the-seed-method
            context.Database.ExecuteSqlCommand("sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'");
            context.Database.ExecuteSqlCommand("sp_MSForEachTable 'IF OBJECT_ID(''?'') NOT IN (ISNULL(OBJECT_ID(''[dbo].[__MigrationHistory]''),0)) DELETE FROM ?'");
            context.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'");
             * */
            #endregion

            #region Steder
            var locations = new List<Location>{
                new Location{
                    LocationType = "Corridor", 
                    LocationName = "D3310", 
                    ShortDescription = "in a hallway", 
                    LongDescription = "A hallway at the 2nd floor"
                },
                 new Location{
                    LocationType = "Classroom", 
                    LocationName = "D3320", 
                    ShortDescription = "in a classroom for electronic engineering students", 
                    LongDescription = "Classroom for students in Digital technics", 
                    AcessTypeRole = "EL"
                },
                 new Location{
                    LocationType = "Computerroom", 
                    LocationName = "D3330", 
                    ShortDescription = "in the Linux lab", 
                    LongDescription = "The Linux lab is the most commonly used room by the computer science students at HiN", 
                    AcessTypeRole = "DT"
                },
                 new Location{
                    LocationType = "Classroom", 
                    LocationName = "D3340", 
                    ShortDescription = "in the foundation lab", 
                    LongDescription = "Classroom for students of electronics", 
                    AcessTypeRole = "EL"
                },
                 new Location{
                    LocationType = "Classroom", 
                    LocationName = "D3350", 
                    ShortDescription = "in the electronics production room", 
                    LongDescription = "Classroom for students of electronic engineering", 
                    AcessTypeRole = "EL"
                },
                new Location{
                    LocationType = "Classroom", 
                    LocationName = "D3360", 
                    ShortDescription = "in the ELK workshop", 
                    LongDescription = "Workshop for students of electronic engineering", 
                    AcessTypeRole = "ELK"
                },
                new Location{
                    LocationType = "Corridor", 
                    LocationName = "C3020", 
                    ShortDescription = "in a hallway", 
                    LongDescription = "Hallway next to the toilets"
                },
                new Location{
                    LocationType = "Toilet", 
                    LocationName = "C3040", 
                    ShortDescription = "in the Gentlemen's toilet",
                },
                new Location{
                    LocationType = "Toilet", 
                    LocationName ="C3050", 
                    ShortDescription = "in the Ladies' toilet",
                },
                new Location{
                    LocationType = "Office", 
                    LocationName = "C2100", 
                    ShortDescription = "in Einars office", 
                    LongDescription = "Einar is a hard working student and always has time for a chat"
                },
                new Location{
                    LocationType = "Corridor", 
                    LocationName = "C2000", 
                    ShortDescription = "in a orridor", 
                    LongDescription = "Just an ordinary corridor"
                },
                new Location{
                    LocationType = "Corridor", 
                    LocationName = "D2500-C", 
                    ShortDescription = "at the Gallery"
                },
                new Location{
                    LocationType = "Corridor", 
                    LocationName = "Glassgata", 
                    ShortDescription = "in the Glassgata"
                },
                new Location{
                    LocationType = "Corridor", 
                    LocationName = "C1001", 
                    ShortDescription = "in a hallway next to HiN IL's office"
                },
                new Location{
                    LocationType = "Office", 
                    LocationName = "C1070", 
                    ShortDescription = "in HiN IL's office"
                },
                new Location{
                    LocationType = "Walkway", 
                    LocationName = "BRU-2C", 
                    LongDescription = "on the bridge to the E-block at the 1st floor"
                },
                new Location{
                    LocationType = "Walkway", 
                    LocationName = "BRU-3C", 
                    LongDescription = "on the bridge to the E-block at the 2nd floor"
                },
                new Location{
                    LocationType = "Corridor", 
                    LocationName = "D2360", 
                    ShortDescription = "in Dean & Project Area", 
                    LongDescription = "In this part is the Dean's office, but also offices to those that work with projects and simulations."
                },
                new Location{
                    LocationType = "Office", 
                    LocationName = "D2210", 
                    ShortDescription = "in Hans Olofsen's office", 
                    LongDescription = "You are amazed by how tidy this office is."
                },
                new Location{
                    LocationType = "Office", 
                    LocationName = "D2280", 
                    ShortDescription = "in The Dean's office", 
                    LongDescription = "What are you doing in the Dean's office without asking for permission!?!"
                },
                new Location{
                    LocationType = "Office", 
                    LocationName = "D2310", 
                    LongDescription = "at Jostein's. He is a very nice research fellow. He loves to help."
                },
                new Location{
                    LocationType = "Corridor", 
                    LocationName = "C3191", 
                    ShortDescription = "in the Engineering Design Area",
                },
                new Location{
                    LocationType = "Office", 
                    LocationName = "D3440", 
                    ShortDescription = "in Guy's office", 
                    LongDescription = "Guy is a nice and funny guy."
                },
                new Location{
                    LocationType = "Office", 
                    LocationName = "D3400C", 
                    ShortDescription = "in Prof. Per-Arne Sundsbø's office", 
                    LongDescription = "Prof. Sundsbø is a role-model professor"
                },
                new Location{
                    LocationType = "Office", 
                    LocationName = "D3480", 
                    ShortDescription = "in Prof. Annette Meidell's office", 
                    LongDescription = "Prof. Meidell was the first female professor at Narvik Univeristy College"
                },
                new Location{
                    LocationType = "Corridor", 
                    LocationName = "C4330", 
                    ShortDescription = "in the C4-area", 
                    LongDescription = "In the part of the building are the offices of the other computer engineering teachers. You decide not to disturb them."
                },
                new Location{
                    LocationType = "Corridor",
                    LocationName = "C5460", 
                    ShortDescription = "in the C5-area",
                    LongDescription = "This floor belong to the Health Department. You have no business here."
                },
                new Location{
                    LocationType = "Corridor", 
                    LocationName = "C6001", 
                    ShortDescription = "at Parking area C", 
                    LongDescription = "Outside is parking for employees. You cannot open the doors."
                }
            };
            locations.ForEach(element => context.Locations.AddOrUpdate(locationName => locationName.LocationName, element));
            context.SaveChanges();
            
#endregion

            #region Lokasjoner
            var location01 = context.Locations.Where(l => l.LocationName == "D3310").FirstOrDefault();
            var location02 = context.Locations.Where(l => l.LocationName == "D3320").FirstOrDefault();
            var location03 = context.Locations.Where(l => l.LocationName == "D3330").FirstOrDefault();
            var location04 = context.Locations.Where(l => l.LocationName == "D3340").FirstOrDefault();
            var location05 = context.Locations.Where(l => l.LocationName == "D3350").FirstOrDefault();
            var location06 = context.Locations.Where(l => l.LocationName == "D3360").FirstOrDefault();
            var location07 = context.Locations.Where(l => l.LocationName == "C3020").FirstOrDefault();
            var location08 = context.Locations.Where(l => l.LocationName == "C3040").FirstOrDefault();
            var location09 = context.Locations.Where(l => l.LocationName == "C3050").FirstOrDefault();
            var location10 = context.Locations.Where(l => l.LocationName == "C2100").FirstOrDefault();
            var location11 = context.Locations.Where(l => l.LocationName == "C2000").FirstOrDefault();
            var location12 = context.Locations.Where(l => l.LocationName == "D2500-C").FirstOrDefault();
            var location13 = context.Locations.Where(l => l.LocationName == "Glassgata").FirstOrDefault();
            var location14 = context.Locations.Where(l => l.LocationName == "C1001").FirstOrDefault();
            var location15 = context.Locations.Where(l => l.LocationName == "C1070").FirstOrDefault();
            var location16 = context.Locations.Where(l => l.LocationName == "BRU-2C").FirstOrDefault();
            var location17 = context.Locations.Where(l => l.LocationName == "BRU-3C").FirstOrDefault();
            var location18 = context.Locations.Where(l => l.LocationName == "D2360").FirstOrDefault();
            var location19 = context.Locations.Where(l => l.LocationName == "D2210").FirstOrDefault();
            var location20 = context.Locations.Where(l => l.LocationName == "D2280").FirstOrDefault();
            var location21 = context.Locations.Where(l => l.LocationName == "D2310").FirstOrDefault();
            var location22 = context.Locations.Where(l => l.LocationName == "C3191").FirstOrDefault();
            var location23 = context.Locations.Where(l => l.LocationName == "D3440").FirstOrDefault();
            var location24 = context.Locations.Where(l => l.LocationName == "D3400C").FirstOrDefault();
            var location25 = context.Locations.Where(l => l.LocationName == "D3480").FirstOrDefault();
            var location26 = context.Locations.Where(l => l.LocationName == "C4330").FirstOrDefault();
            var location27 = context.Locations.Where(l => l.LocationName == "C5460").FirstOrDefault();
            var location28 = context.Locations.Where(l => l.LocationName == "C6001").FirstOrDefault();
            #endregion

            #region Ting
            var things = new List<Thing>{
                new Thing{
                    Name = "Cola-boks",
                    Description = "En cola-boks med cola inni evt oppi. Den er full.",
                    IsStationary = false,
                    WrittenText = "Inneholder koockain og herpes.",
                    LocationID = context.Locations.Where(l => l.LocationName == "D3360").FirstOrDefault().LocationID,
                    PlayerWritable = false
                },
                new Thing{
                    Name = "Tavle",
                    Description = "En kul gul tavle som henger på veggen.",
                    IsStationary = true,
                    WrittenText = "Whaaaaaaaat?",
                    LocationID = context.Locations.Where(l => l.LocationName == "D3330").FirstOrDefault().LocationID,
                    PlayerWritable = true
                },
                new Thing{
                    Name = "Bazooka",
                    Description = "En sovjetisk bazooka fra den kalde krigen.",
                    IsStationary = false,
                    WrittenText = "что-то смешное на русском.",
                    LocationID = context.Locations.Where(l => l.LocationName == "D2210").FirstOrDefault().LocationID,
                    PlayerWritable = false
                },
            };

            things.ForEach(element => context.Things.AddOrUpdate(u => u.Name, element));
            context.SaveChanges();
            #endregion  

            #region AI
            var artificialPlayer = new List<ArtificialPlayer>{
                new ArtificialPlayer{
                    Name = "Hans Olofsen",
                    Description = "Hans er en trivelig dude som jobber på skolen",
                    IsStationary = false,
                    Type = "Førstelektor",
                    LocationID = location19.LocationID,
                    AccessLevel = "Universal"
                },
                new ArtificialPlayer{
                    Name = "Knut Collin",
                    Description = "Høgskolelektor med avansert kunnskap innen forskjellige programmeringsspråk",
                    IsStationary = false,
                    Type = "Høgskolelektor",
                    LocationID = location03.LocationID,
                    AccessLevel = "Universal"
                },
                new ArtificialPlayer{
                    Name = "Arvid Urke",
                    Description = "Snasn kis med mye på hjertet",
                    IsStationary = false,
                    Type = "Rådgiver",
                    LocationID = location13.LocationID,
                    AccessLevel = "Universal"
                },
                new ArtificialPlayer{
                    Name = "Dracula",
                    Description = "Skummel kar som biter",
                    IsStationary = false,
                    Type = "Vampyr",
                    LocationID = location09.LocationID,
                    AccessLevel = "Universal"
                }

            };

            artificialPlayer.ForEach(element => context.ArtificialPlayers.AddOrUpdate(u => u.Name, element));
            context.SaveChanges();
            #endregion

            #region Brukere
            var userPaal = createUser("pskogsru88@hotmail.com", "appelsinFarge5", "Gerrard", "Datateknikk", false, location03);
            var userTina = createUser("tinahotty64@hotmail.com", "appelsinFarge5", "Tina", "Datateknikk", false, location03);
            var userKristina = createUser("kristinamyrligundersen@gmail.com", "appelsinFarge5", "Kristina", "Datateknikk", false, location03);
            var userAndreas = createUser("drknert@gmail.com", "appelsinFarge5", "Andreas", "Datateknikk", false, location03);
            var userAlexander = createUser("alec90@gmail.com", "appelsinFarge5", "Alexander", "Datateknikk", false, location03);
            var userMarius = createUser("skaterase@gmail.com", "appelsinFarge5", "Marius", "Datateknikk", false, location03);
            #endregion

            #region Kommandoer
            var commands = new List<Command>
            {
                new Command {
                    Name = "Take",
                    Description = "Put object in inventory"
                },
                new Command {
                    Name = "Open",
                    Description = "Open door, cupboard etc."
                },
                new Command {
                    Name = "Use",
                    Description = "Activate function on object"
                },
                new Command {
                    Name = "Drop",
                    Description = "Leave object in room"
                },
                new Command {
                    Name = "Turn on",
                    Description = "Turn on a device"
                },
                new Command {
                    Name = "Turn off",
                    Description = "Turn off a device"
                },
                new Command {
                    Name = "Talk",
                    Description = "Request a response from AI"
                },
                new Command {
                    Name = "Kick",
                    Description = "Kick an object"
                },
                new Command {
                    Name = "Close",
                    Description = "Close door, cupboard etc."
                },
                new Command {
                    Name = "Enter",
                    Description = "Enter door, room"
                },
                new Command {
                    Name = "Write on",
                    Description = "Open editor to write on object"
                },
                new Command {
                    Name = "Look at",
                    Description = "Show details about object"
                },
                new Command {
                    Name = "Punch",
                    Description = "Punch an object"
                },
            };
            commands.ForEach(element => context.Commands.AddOrUpdate(u => u.Name, element));
            context.SaveChanges();

            var take = context.Commands.Where(u => u.Name == "Take").FirstOrDefault().CommandID;
            var open = context.Commands.Where(u => u.Name == "Open").FirstOrDefault().CommandID;
            var use = context.Commands.Where(u => u.Name == "Use").FirstOrDefault().CommandID;
            var drop = context.Commands.Where(u => u.Name == "Drop").FirstOrDefault().CommandID;
            var turnOn = context.Commands.Where(u => u.Name == "Turn on").FirstOrDefault().CommandID;
            var turnOff = context.Commands.Where(u => u.Name == "Turn off").FirstOrDefault().CommandID;
            var talk = context.Commands.Where(u => u.Name == "Talk").FirstOrDefault().CommandID;
            var kick = context.Commands.Where(u => u.Name == "Kick").FirstOrDefault().CommandID;
            var close = context.Commands.Where(u => u.Name == "Close").FirstOrDefault().CommandID;
            var enter = context.Commands.Where(u => u.Name == "Enter").FirstOrDefault().CommandID;
            var writeOn = context.Commands.Where(u => u.Name == "Write on").FirstOrDefault().CommandID;
            var lookAt = context.Commands.Where(u => u.Name == "Look at").FirstOrDefault().CommandID;
            var punch = context.Commands.Where(u => u.Name == "Punch").FirstOrDefault().CommandID;
            #endregion

            #region Kunstige spillere
            var artificialPlayerResponses = new List<ArtificialPlayerResponse>
            {
            #region Hans 
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Hans Olofsen").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "God dag!"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Hans Olofsen").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Jeg har et veldig ryddig kontor"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Hans Olofsen").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Fint vær i dag"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Hans Olofsen").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Jeg lærer meg asp.NET"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Hans Olofsen").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Skolen er full av hemmeligheter!"
                },
            #endregion
            #region kc
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Knut Collin").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "..."
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Knut Collin").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Jeg har masse å gjøre"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Knut Collin").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Jeg har rettet oblig'en din, det ser meget bra ut"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Knut Collin").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Jeg skal holde et foredrag om java i morgen"
                },
            #endregion
            #region urke
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Arvid Urke").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Stå på! Tenk på dopaminet!"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Arvid Urke").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Du kan klare alt! Fortsett sånn."
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Arvid Urke").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Jeg skal personlig sørge for at du får A+ på eksamen"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Arvid Urke").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Høgskolen i Narvik er den beste i Norge, nei, hele verden!"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Arvid Urke").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Alle husker dagen Urke mistet buksene"
                },
            #endregion
            #region dracula
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Dracula").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Ha, ha! Jeg er Dracula!"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Dracula").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "I natt skal det skje."
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Dracula").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Du kan ikke rømme fra Dracula!"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Dracula").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "Hvem våger å forstyrre Dracula?"
                },
                new ArtificialPlayerResponse 
                {
                    ArtificialPlayerID = context.ArtificialPlayers.Where(l => l.Name == "Dracula").FirstOrDefault().ArtificialPlayerID,
                    ResponseText = "It's idnight and the oon is up"
                }
            #endregion  

            };

            artificialPlayerResponses.ForEach(element => context.ArtificialPlayerResponses.AddOrUpdate(u => u.ResponseText, element));
            context.SaveChanges();
            #endregion

            #region Variabler

            var Hans = context.ArtificialPlayers.Where(u => u.Name == "Hans Olofsen").FirstOrDefault().ArtificialPlayerID;
            var Knut = context.ArtificialPlayers.Where(u => u.Name == "Knut Collin").FirstOrDefault().ArtificialPlayerID;
            var Arvid = context.ArtificialPlayers.Where(u => u.Name == "Arvid Urke").FirstOrDefault().ArtificialPlayerID;
            var Dracula = context.ArtificialPlayers.Where(u => u.Name == "Dracula").FirstOrDefault().ArtificialPlayerID;

            var cola = context.Things.Where(u => u.Name == "Cola-boks").FirstOrDefault().ThingID;
            var tavle = context.Things.Where(u => u.Name == "Tavle").FirstOrDefault().ThingID;
            var bazooka = context.Things.Where(u => u.Name == "Bazooka").FirstOrDefault().ThingID;



            #endregion

            #region Stedsforbindelse
            var locationConnections = new List<LocationConnection>
            {
#region D3310
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D3310").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D3320").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D3310").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D3330").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D3310").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D3340").FirstOrDefault().LocationID,
                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D3310").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D3350").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D3310").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D3360").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D3310").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C3020").FirstOrDefault().LocationID,

                },
#endregion
#region C3020
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3020").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C3191").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3020").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "BRU-3C").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3020").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C4330").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3020").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C5460").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3020").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C2000").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3020").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D2500-C").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3020").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C3040").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3020").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D3350").FirstOrDefault().LocationID,

                },

#endregion

#region C2000
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C2000").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C2100").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C2000").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D2500-C").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C2000").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C1001").FirstOrDefault().LocationID,

                },
#endregion
#region D2500
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D2500-C").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D2360").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D2500-C").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "BRU-2C").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D2500-C").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "Glassgata").FirstOrDefault().LocationID,

                },
#endregion
#region Glassgata
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "Glassgata").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C1001").FirstOrDefault().LocationID,

                },
#endregion
#region C1001
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C1001").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C1070").FirstOrDefault().LocationID,

                },
#endregion
#region D2360
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D2360").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D2280").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D2360").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D2310").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "D2360").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D2210").FirstOrDefault().LocationID,

                },
#endregion
#region C3191
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3191").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D3440").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3191").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D3400C").FirstOrDefault().LocationID,

                },
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C3191").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "D3480").FirstOrDefault().LocationID,

                },
#endregion
#region C4330
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C4330").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C5460").FirstOrDefault().LocationID,

                },
#endregion
#region C5460
                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C5460").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C6001").FirstOrDefault().LocationID,

                },

                new LocationConnection {
                    LocationOne_LocationID = context.Locations.Where(l => l.LocationName == "C5460").FirstOrDefault().LocationID,
                    LocationTwo_LocationID = context.Locations.Where(l => l.LocationName == "C3191").FirstOrDefault().LocationID,

                }
#endregion

            };
            foreach (LocationConnection lc in locationConnections) { CreateLocationConnection(lc, context); }
            //context.SaveChanges();
            #endregion

            #region Gyldige kommandoer for AI

            var validCommandForAi = new List<ValidCommandsForArtificialPlayers>{
#region Hans Olofsen 
                
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = Hans,
                    CommandID = talk,
                },
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = Hans,
                    CommandID = lookAt,
                },
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = Hans,
                    CommandID = punch,
                },
#endregion
#region Knut Collin    
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = Knut,
                    CommandID = talk,
                },
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = Knut,
                    CommandID = lookAt,
                },
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = Knut,
                    CommandID = punch,
                },
#endregion
#region Arvid Urke   
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = Arvid,
                    CommandID = talk,
                },
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = Arvid,
                    CommandID = lookAt,
                },
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = Arvid,
                    CommandID = punch,
                },
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = Arvid,
                    CommandID = turnOff,
                },
#endregion
#region Dracula   
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = Dracula,
                    CommandID = talk,
                },
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = Dracula,
                    CommandID = lookAt,
                },
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = Dracula,
                    CommandID = punch,
                },
                new ValidCommandsForArtificialPlayers{
                    ArtificialPlayerID = Dracula,
                    CommandID = kick,
                }
#endregion
            };
            foreach (ValidCommandsForArtificialPlayers valCom in validCommandForAi) { CreateValidCommandsForAI(valCom, context); }
            //context.SaveChanges();
            #endregion

            #region Gyldige kommandoer for ting

            var validCommandForThings = new List<ValidCommandsForThings>{
#region Cola-boks   
                new ValidCommandsForThings{
                    ThingID = cola,
                    CommandID = take,
                },
                new ValidCommandsForThings{
                    ThingID = cola,
                    CommandID = use,
                },
                new ValidCommandsForThings{
                    ThingID = cola,
                    CommandID = drop,
                },
                new ValidCommandsForThings{
                    ThingID = cola,
                    CommandID = lookAt,
                },

#endregion
#region Tavle  
                new ValidCommandsForThings{
                    ThingID = tavle,
                    CommandID = writeOn,
                },
                new ValidCommandsForThings{
                    ThingID = tavle,
                    CommandID = lookAt,
                },
#endregion
#region Bazooka
                new ValidCommandsForThings{
                    ThingID = bazooka,
                    CommandID = take,
                },
                new ValidCommandsForThings{
                    ThingID = bazooka,
                    CommandID = use,
                },
                new ValidCommandsForThings{
                    ThingID = bazooka,
                    CommandID = drop,
                },
                new ValidCommandsForThings{
                   ThingID = bazooka,
                    CommandID = lookAt,
                }
#endregion

            };
            foreach (ValidCommandsForThings valTin in validCommandForThings) { CreateValidCommandsForThings(valTin, context); }
            //context.SaveChanges();
            #endregion
        }
        #endregion

    }
    #endregion
}
#endregion
