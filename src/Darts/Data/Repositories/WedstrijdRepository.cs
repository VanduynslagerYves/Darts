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

        ICollection<Wedstrijd> IWedstrijdRepository.GetAll()
        {
            return _wedstrijden
                //.Include(w => w.Speler1)
                //.Include(w => w.Speler2)
                .ToList();
        }

        ICollection<Wedstrijd> IWedstrijdRepository.GetById(int id)
        {
            return _wedstrijden
                //.Include(w => w.Speler1)
                //.Include(w => w.Speler2)
                .Where(w => w.Id == id)
                .ToList();
        }

        ICollection<Wedstrijd> IWedstrijdRepository.GetBySpeler(Speler speler)
        {
            return _wedstrijden
                //.Include(w => w.Speler1)
                //.Include(w => w.Speler2)
                //.Where(w => w.Speler1.Equals(speler))
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
