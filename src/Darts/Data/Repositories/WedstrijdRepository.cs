using Darts.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Darts.Data.Repositories
{
    public class WedstrijdRepository : IWedstrijdRepository
    {
        private DbSet<Wedstrijd> _wedstrijden;
        private ApplicationDbContext _dbContext;

        public WedstrijdRepository(ApplicationDbContext context)
        {
            _dbContext = context;
            _wedstrijden = _dbContext.Wedstrijden;
        }

        public ICollection<Wedstrijd> GetAll()
        {
            return _wedstrijden
                .ToList();
        }

       public Wedstrijd GetById(int id)
        {
            return _wedstrijden
                .SingleOrDefault(w => w.Id == id);
        }

        public ICollection<Wedstrijd> GetBySpeler(Speler speler)
        {
            return _wedstrijden
                .ToList();
        }

        public void Add(Wedstrijd wedstrijd)
        {
            _wedstrijden.Add(wedstrijd);
        }

        public void Remove(Wedstrijd wedstrijd)
        {
            _wedstrijden.Remove(wedstrijd);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
