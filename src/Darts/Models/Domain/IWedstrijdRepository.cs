using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Darts.Models.Domain
{
    public interface IWedstrijdRepository
    {
        ICollection<Wedstrijd> GetAll();
        Wedstrijd GetById(int id);
        ICollection<Wedstrijd> GetBySpeler(Speler speler);

        void Add(Wedstrijd wedstrijd);
        void Remove(Wedstrijd wedstrijd);
        void SaveChanges();
    }
}
