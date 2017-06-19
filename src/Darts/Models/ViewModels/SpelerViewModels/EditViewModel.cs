using System;
using System.ComponentModel.DataAnnotations;
using Darts.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Darts.Models.ViewModels.SpelerViewModels
{
    public class EditViewModel
    {
        [HiddenInput]
        public int SpelerId
        {
            get; set;
        }
        [Required]
        [StringLength(50, ErrorMessage = "{0} may not contain more than 50 characters")]
        public string Naam
        {
            get; set;
        }
        [Required]
        [StringLength(20, ErrorMessage ="{0} mag niet langer zijn dan 20 tekens")]
        public string Voornaam
        { get; set; }
        //public string Street
        //{
        //    get; set;
        //}
        //[Display(Name = "Postal code")]
        //public string PostalCode
        //{
        //    get; set;
        //}
        //[DataType(DataType.Currency)]
        //[Range(0, int.MaxValue, ErrorMessage = "{0} may not be a negative value.")]
        //public int? Turnover
        //{
        //    get; set;
        //}
        //public string Description
        //{
        //    get; set;
        //}
        [Display(Name = "E-mailadres")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Dit is geen geldig e-mailadres")]
        public string Email
        {
            get; set;
        }
        //[Display(Name = "Date established")]
        //[DataType(DataType.Date)]
        //public DateTime? DateEstablished
        //{
        //    get; set;
        //}

        public EditViewModel()
        {
        }

        public EditViewModel(Speler speler) : this()
        {
            SpelerId = speler.Id;
            Voornaam = speler.Voornaam;
            Naam = speler.Naam;
            Email = speler.Email;
        }
    }
}