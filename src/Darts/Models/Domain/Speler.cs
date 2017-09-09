using System;
using System.Collections.Generic;

namespace Darts.Models.Domain
{
    public class Speler
    {
        #region Properties
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string VolledigeNaam => Naam + " " + Voornaam;
        public int AantalGespeeld => SpelerWedstrijd.Count;
        public virtual ICollection<SpelerWedstrijd> SpelerWedstrijd { get; set; }
        public int TotaalPunten => BerekenTotaal();
        public int VerlorenPunten => BerekenVerlorenPunten();
        public DateTime DatumToegevoegd { get; set; }
        #endregion

        #region Constructors
        public Speler(string voornaam, string naam) : this()
        {
            Naam = naam;
            Voornaam = voornaam;
        }

        public Speler()
        {
            //Wedstrijden = new List<Wedstrijd>();
            SpelerWedstrijd = new List<SpelerWedstrijd>();
            DatumToegevoegd = DateTime.Now;
        }
        #endregion
        #region Methods
        private int BerekenTotaal()
        {
            int totaal = 0;
            foreach(SpelerWedstrijd sw in SpelerWedstrijd)
            {
                if(sw.SpelerId == this.Id)
                {
                    totaal += sw.PuntenGewonnen;
                }
            }
            return totaal;
        }

        private int BerekenVerlorenPunten()
        {
            int totaal = 0;
            foreach (SpelerWedstrijd sw in SpelerWedstrijd)
            {
                if (sw.SpelerId == this.Id)
                {
                    totaal += sw.PuntenVerloren;
                }
            }
            return totaal;
        }

        //public void AddWedstrijd(Wedstrijd w)
        //{ 
        //    Wedstrijden.Add(w);
        //}
        #endregion
    }
}