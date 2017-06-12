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
            return _spelers.Include(s => s.Resultaten).SingleOrDefault(s => s.SpelerId == id);
        }
        public Speler GetBy(string email)
        {
            return _spelers.Include(s => s.Resultaten).SingleOrDefault(s => s.Email == email);
        }
        //public Speler GetBy(string email)
        //{
        //    return _spelers.Include(c => c.Location).SingleOrDefault(c => c.Email == email);
        //}
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
            return _spelers.Include(s => s.Resultaten).OrderByDescending(s => s.TotaalPunten).ToList();
        }
    }
}
