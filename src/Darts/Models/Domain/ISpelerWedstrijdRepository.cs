using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Darts.Models.Domain
{
    public interface ISpelerWedstrijdRepository
    {
        int BerekenTotaalVoorSpeler(int spelerId);
        IEnumerable<SpelerWedstrijd> GetBySpelerId(int id);
        void SaveChanges();
        void Add(SpelerWedstrijd sw);
        void Remove(SpelerWedstrijd sw);
    }
}
