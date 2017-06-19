using Darts.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Darts.Data.Repositories
{
    public class SpelerWedstrijdRepository : ISpelerWedstrijdRepository
    {
        private DbSet<SpelerWedstrijd> _spelerWedstrijden;
        private ApplicationDbContext _dbContext;
        public SpelerWedstrijdRepository(ApplicationDbContext context)
        {
            _dbContext = context;
            _spelerWedstrijden = _dbContext.SpelerWedstrijden;      
        }

        public int BerekenTotaalVoorSpeler(int spelerId)
        {
            throw new NotImplementedException();
        }

        public IDictionary<Speler, Wedstrijd> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SpelerWedstrijd> GetBySpelerId(int id)
        {
            return _spelerWedstrijden
                .Include(t => t.Speler)
                .Include(t => t.Wedstrijd)
                .Where(t => t.Speler.Id == id).ToList();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Add(SpelerWedstrijd sw)
        {
            _spelerWedstrijden.Add(sw);
        }

        public void Remove(SpelerWedstrijd sw)
        {
            _spelerWedstrijden.Remove(sw);
        }
    }
}
