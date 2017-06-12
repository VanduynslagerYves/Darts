using System;
using System.Collections.Generic;

namespace Darts.Models.Domain
{
    public class Speler
    {
        #region Properties
        public int SpelerId { get; set; }
        public string Email { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public int AantalGespeeld => Resultaten.Count;
        public ICollection<Resultaat> Resultaten { get; set; }
        public int TotaalPunten => BerekenTotaal();
        public DateTime DatumToegevoegd { get; set; }
        #endregion

        #region Constructors
        public Speler(string email, string naam, string voornaam):this()
        {
            Email = email;
            Naam = naam;
            Voornaam = voornaam;
        }

        public Speler()
        {
            Resultaten = new List<Resultaat>();
            DatumToegevoegd = DateTime.Now;
        }
        #endregion
        #region Methods
        private int BerekenTotaal()
        {
            int totaal = 0;
            foreach(Resultaat r in Resultaten)
            {
                totaal += r.Punten;
            }
            return totaal;
        }

        public void AddResultaat(DateTime speelDatum, int punten)
        {
            Resultaten.Add(new Resultaat(speelDatum, punten));
        }
        #endregion
    }
}