using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Darts.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Darts.Data
{
    public class DartsDataInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public DartsDataInitializer(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                string eMailAddress = "dorine.warnez@gmail.be";
                ApplicationUser user = new ApplicationUser { UserName = eMailAddress, Email = eMailAddress };
                await _userManager.CreateAsync(user, "YvesNick63142");
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "admin"));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "speler"));

                eMailAddress = "speler@darts.be";
                user = new ApplicationUser { UserName = eMailAddress, Email = eMailAddress };
                await _userManager.CreateAsync(user, "Darts123");
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "speler"));

                //SpelerWedstrijden adden aan context, met speler en wedstrijd
                //SpelerWedstrijd(speler 1 dan 2, zelfde wedstrijd)
                //var speler1 = new Speler("jan.deman@telenet.be", "Jan", "De man");
                //var speler2 = new Speler("dartsmaster@telenet.be", "Bert", "Vanachterdehoek");
                //var speler3 = new Speler("reetman@telenet.be", "Jantje", "Smit");

                //Wedstrijd w = new Wedstrijd(DateTime.Now);
                //SpelerWedstrijd sw1 = new SpelerWedstrijd(speler1, w, 3, speler2.Voornaam + " " + speler2.Naam);
                //SpelerWedstrijd sw2 = new SpelerWedstrijd(speler2, w, 0, speler1.Voornaam + " " + speler1.Naam);

                //w = new Wedstrijd(new DateTime(2010,1,12));
                //SpelerWedstrijd sw3 = new SpelerWedstrijd(speler2, w, 2, speler3.Voornaam + " " + speler3.Naam);
                //SpelerWedstrijd sw4 = new SpelerWedstrijd(speler3, w, 1, speler2.Voornaam + " " + speler2.Naam);

                //SpelerWedstrijd[] sws = { sw1, sw2, sw3, sw4 };
                //_dbContext.SpelerWedstrijden.AddRange(sws);
                //_dbContext.SaveChanges();

                //speler1.AddWedstrijd(new Wedstrijd(DateTime.Now, speler1, speler2, 3));
                //speler2.AddWedstrijd(new Wedstrijd(DateTime.Now, speler2, speler1, 1));
                //speler3.AddWedstrijd(new Wedstrijd(DateTime.Now, speler3, speler2, 1));

                //Speler[] spelers = { speler1, speler2, speler3 };

                //_dbContext.Spelers.AddRange(spelers);
                //_dbContext.SaveChanges();

                //await InitializeUsersAndCustomers();
            }
        }

        //private async Task InitializeUsersAndCustomers()
        //{
        //    string eMailAddress = "beermaster@hogent.be";
        //    ApplicationUser user = new ApplicationUser { UserName = eMailAddress, Email = eMailAddress };
        //    await _userManager.CreateAsync(user, "P@ssword1");
        //    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "admin"));

        //    eMailAddress = "jan@hogent.be";
        //    user = new ApplicationUser { UserName = eMailAddress, Email = eMailAddress };
        //    await _userManager.CreateAsync(user, "P@ssword1");
        //    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "customer"));

        //    var customer = new Speler
        //    {
        //        Email = eMailAddress,
        //        FirstName = "Jan",
        //        Name = "De man",
        //        Location = _dbContext.Locations.SingleOrDefault(l => l.PostalCode == "9700"),
        //        Street = "Nederstraat 5"
        //    };

        //    _dbContext.Customers.Add(customer);
        //    _dbContext.SaveChanges();
        //}
    }
}

