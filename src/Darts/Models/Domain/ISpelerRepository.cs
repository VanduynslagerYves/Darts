using System.Collections.Generic;

namespace Darts.Models.Domain
{
    public interface ISpelerRepository
    {
        ICollection<Speler> GetAll();
        Speler GetById(int id);
        void Remove(Speler speler);
        void Add(Speler speler);
        void SaveChanges();
    }
}
