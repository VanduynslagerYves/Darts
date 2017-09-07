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
        [Display(Name = "Punten van de speler")]
        [Range(0, 3, ErrorMessage = "{0} moeten tussen {1} en {2} liggen!")]
        public int PuntenGewonnen { get; set; }
        public int PuntenVerloren => 3 - PuntenGewonnen;
        [Display(Name ="Datum gespeeld")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DatumGespeeld { get; set; }
        [Required(ErrorMessage = "Je moet een speler selecteren!")]
        [Display(Name ="Speler")]
        public int IdSpeler1 { get; set; }
        [Required(ErrorMessage = "Je moet een tegenstander selecteren!")]
        [Display(Name ="Tegenstander")]
        public int IdSpeler2 { get; set; }
    }
}
