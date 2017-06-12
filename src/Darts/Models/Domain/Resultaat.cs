using System;
using System.Collections.Generic;
using System.Linq;

namespace Darts.Models.Domain
{
    public class Resultaat
    {
        #region Properties
        public DateTime? SpeelDatum { get; set; }
        public int Punten { get; set; }
        public int ResultaatId { get; set; }
        #endregion

        #region Constructors
        protected Resultaat()
        { }
        public Resultaat(DateTime _speelDatum, int _punten):this()
        {
            this.SpeelDatum = _speelDatum;
            this.Punten = _punten;
        }
        #endregion
    }
}
