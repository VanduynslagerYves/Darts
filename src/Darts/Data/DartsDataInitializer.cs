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
                string eMailAddress = "yves.vanduynslager@telenet.be";
                ApplicationUser user = new ApplicationUser { UserName = eMailAddress, Email = eMailAddress };
                await _userManager.CreateAsync(user, "P@ssword1");
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "admin"));

                eMailAddress = "jan@hogent.be";
                user = new ApplicationUser { UserName = eMailAddress, Email = eMailAddress };
                await _userManager.CreateAsync(user, "P@ssword1");
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "speler"));

                eMailAddress = "dartsmaster@telenet.be";
                user = new ApplicationUser { UserName = eMailAddress, Email = eMailAddress };
                await _userManager.CreateAsync(user, "P@ssword1");
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "speler"));

                var speler1 = new Speler("jan.deman@telenet.be", "Jan", "De man");
                speler1.AddResultaat(DateTime.Now, 50);
                var speler2 = new Speler("dartsmaster@telenet.be", "Bert", "Vanachterdehoek");
                speler2.AddResultaat(DateTime.Now, 20);
                //{
                //    Email = eMailAddress,
                //    Voornaam = "Jan",
                //    Naam = "De man"
                //};
                Speler[] spelers = { speler1, speler2 };
                _dbContext.Spelers.AddRange(spelers);
                _dbContext.SaveChanges();

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

