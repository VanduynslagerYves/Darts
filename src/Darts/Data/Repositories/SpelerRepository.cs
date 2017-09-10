using System;
using System.Collections.Generic;
using System.Linq;
using Darts.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Darts.Data.Repositories
{
    public class SpelerRepository : ISpelerRepository
    {
        private DbSet<Speler> _spelers;
        private ApplicationDbContext _dbContext;

        public SpelerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _spelers = _dbContext.Spelers;
        }
        public Speler GetById(int id)
        {
            return _spelers.Include(s => s.SpelerWedstrijd).SingleOrDefault(s => s.Id == id);
        }
        public void Add(Speler speler)
        {
            _spelers.Add(speler);
        }

        public void Remove(Speler speler)
        {
            _spelers.Remove(speler);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public ICollection<Speler> GetAll()
        {
            return _spelers
                .Include(s => s.SpelerWedstrijd)
                .OrderByDescending(s => s.TotaalPunten)
                .ToList();

        }
    }
}
