using System;
using System.Collections.Generic;
using Darts.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Darts.Models.ViewModels.SpelerViewModels;

namespace Darts.Controllers
{
    [Authorize(Policy = "Speler")]
    public class DartsController : Controller
    {
        private readonly ISpelerRepository _spelerRepository;
        private readonly IWedstrijdRepository _wedstrijdRepository;
        private readonly ISpelerWedstrijdRepository _spelerWedstrijdRepository;

        public DartsController(ISpelerRepository spelerRepository,
            IWedstrijdRepository wedstrijdRepository,
            ISpelerWedstrijdRepository spelerWedstrijdRepository)
        {
            _spelerRepository = spelerRepository;
            _wedstrijdRepository = wedstrijdRepository;
            _spelerWedstrijdRepository = spelerWedstrijdRepository;
        }

        //volgende overschrijven auth van class level
        public IActionResult Index()
        {
            IEnumerable<Speler> spelers = _spelerRepository.GetAll();
            return View(spelers);
        }

        public IActionResult Detail(int id)
        {
            Speler s = _spelerRepository.GetById(id);
            ViewData["Speler"] = s.Voornaam + " " + s.Naam;
            IEnumerable<SpelerWedstrijd> wedstrijden = _spelerWedstrijdRepository.GetBySpelerId(id);
            return View(wedstrijden);
        }
        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Edit(int id)
        {
            Speler speler = _spelerRepository.GetById(id);
            if (speler == null)
                return NotFound();
            //ViewData["Locations"] = GetLocationsAsSelectList(brewer.Location?.PostalCode);
            return View(new EditViewModel(speler));
        }

        [HttpGet]
        [Authorize(Policy ="AdminOnly")]
        public IActionResult NieuweWedstrijd()
        {
            ViewData["Spelers"] = GetSpelersAsSelectList();
            return View(new WedstrijdViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy ="AdminOnly")]
        public IActionResult NieuweWedstrijd(WedstrijdViewModel spelerwedstrijd)
        {
            if (ModelState.IsValid && (spelerwedstrijd.IdSpeler1 != spelerwedstrijd.IdSpeler2))
            {
                try
                {
                    Speler speler1 = _spelerRepository.GetById(spelerwedstrijd.IdSpeler1);
                    Speler speler2 = _spelerRepository.GetById(spelerwedstrijd.IdSpeler2);
                    Wedstrijd w = new Wedstrijd(spelerwedstrijd.DatumGespeeld);

                    SpelerWedstrijd sw1 = new SpelerWedstrijd(speler1, w, spelerwedstrijd.PuntenGewonnen, speler2.Voornaam + " " + speler2.Naam);
                    SpelerWedstrijd sw2 = new SpelerWedstrijd(speler2, w, spelerwedstrijd.PuntenVerloren, speler1.Voornaam + " " + speler1.Naam);

                    _spelerWedstrijdRepository.Add(sw1);
                    _spelerWedstrijdRepository.Add(sw2);
                    _spelerWedstrijdRepository.SaveChanges();

                    TempData["message"] = $"Nieuwe wedstrijd tussen {speler1.VolledigeNaam} en {speler2.VolledigeNaam} werd met succes toegevoegd!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            ViewData["Spelers"] = GetSpelersAsSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Edit(EditViewModel spelerEditViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Speler speler = _spelerRepository.GetById(spelerEditViewModel.SpelerId);
                    MapSpelerEditViewModelToSpeler(spelerEditViewModel, speler);
                    _spelerRepository.SaveChanges();
                    TempData["message"] = $"{speler.Naam} {speler.Voornaam} werd succesvol gewijzigd!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            //ViewData["Locations"] = GetLocationsAsSelectList(brewerEditViewModel?.PostalCode);
            return View(spelerEditViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Create()
        {
            //ViewData["Locations"] = GetLocationsAsSelectList(null);
            return View(nameof(Edit), new EditViewModel(new Speler()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Create(EditViewModel spelerEditViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Speler speler = new Speler();
                    MapSpelerEditViewModelToSpeler(spelerEditViewModel, speler);
                    _spelerRepository.Add(speler);
                    _spelerRepository.SaveChanges();
                    TempData["message"] = $"{speler.Naam} {speler.Voornaam} werd succesvol toegevoegd!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            //ViewData["Locations"] = GetLocationsAsSelectList(brewerEditViewModel?.PostalCode);
            return View(nameof(Edit), spelerEditViewModel);
        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult Delete(int id)
        {
            Speler speler = _spelerRepository.GetById(id);
            if (speler == null)
                return NotFound();
            ViewData[nameof(Speler.Naam)] = speler.Naam;
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult DeleteConfirmed(int id)
        {
            Speler speler = null;
            try
            {
                speler = _spelerRepository.GetById(id);
                _spelerRepository.Remove(speler);
                _spelerRepository.SaveChanges();
                TempData["message"] = $"{speler.Naam} {speler.Voornaam} werd succesvol verwijderd!";
            }
            catch
            {
                TempData["error"] = $"Er ging iets verkeerd, speler {speler?.Naam} {speler?.Voornaam} werd niet verwijderd...";
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Spelers()
        {
            return View(_spelerRepository.GetAll());
        }

        //private SelectList GetLocationsAsSelectList(string postalCode)
        //{
        //    return new SelectList(
        //        _locationRepository.GetAll().OrderBy(l => l.Name),
        //        nameof(Location.PostalCode),
        //        nameof(Location.Name),
        //        postalCode);
        //}
        private SelectList GetSpelersAsSelectList()
        {
            return new SelectList(_spelerRepository.GetAll().OrderBy(s => s.Naam).ThenBy(s => s.Voornaam),
                nameof(Speler.Id),
                nameof(Speler.VolledigeNaam));
        }

        private void MapSpelerEditViewModelToSpeler(EditViewModel spelerEditViewModel, Speler speler)
        {
            speler.Naam = spelerEditViewModel.Naam;
            speler.Email = spelerEditViewModel.Email;
            speler.Voornaam = spelerEditViewModel.Voornaam;
        }
    }
}
