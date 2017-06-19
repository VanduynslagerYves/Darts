using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Darts.Models.Domain
{
    public class Wedstrijd
    {
        public int Id { get; set; }
        public DateTime DatumGespeeld { get; set; }

        public virtual ICollection<SpelerWedstrijd> SpelerWedstrijd { get; set; }
        protected Wedstrijd()
        {
            SpelerWedstrijd = new List<SpelerWedstrijd>();
        }
        public Wedstrijd(DateTime datumGespeeld):this()
        {
            DatumGespeeld = datumGespeeld;
        }
    }
}
