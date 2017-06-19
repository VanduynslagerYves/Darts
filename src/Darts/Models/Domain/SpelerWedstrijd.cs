using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Darts.Models.Domain
{
    public class SpelerWedstrijd
    {
        public int SpelerId { get; set; }
        public int WedstrijdId { get; set; }
        public int PuntenGewonnen { get; set; }
        public int PuntenVerloren => (3 - PuntenGewonnen);
        public string Tegenstander { get; set; }
        public Speler Speler { get; set; }
        public Wedstrijd Wedstrijd { get; set; }

        protected SpelerWedstrijd() { }
        public SpelerWedstrijd(Speler speler, Wedstrijd wedstrijd, int puntenGewonnen, string tegenstander):this()
        {
            Speler = speler;
            Wedstrijd = wedstrijd;

            SpelerId = speler.Id;
            WedstrijdId = wedstrijd.Id;
            PuntenGewonnen = puntenGewonnen;
            Tegenstander = tegenstander;
        }
    }
}
