using Darts.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Darts.Models.ViewModels.SpelerViewModels
{
    public class WedstrijdViewModel
    {
        public WedstrijdViewModel()
        {
            DatumGespeeld = DateTime.Now;
        }
        [Display(Name = "Punten van Speler1")]
        [Range(0, 3, ErrorMessage = "{0} moeten tussen {1} en {2} liggen!")]
        public int PuntenGewonnen { get; set; }
        public int PuntenVerloren => 3 - PuntenGewonnen;
        [Display(Name ="Tijdstip gespeeld")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DatumGespeeld { get; set; }
        [Required]
        [Display(Name ="Speler 1")]
        public int IdSpeler1 { get; set; }
        [Required]
        [Display(Name ="Speler 2")]
        public int IdSpeler2 { get; set; }
    }
}
